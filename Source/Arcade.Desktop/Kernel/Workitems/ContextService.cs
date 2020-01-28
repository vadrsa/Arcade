using Kernel.Prism;
using Kernel.Workitems.Strategies.Close;
using Kernel.Workitems.Strategies.Focus;
using Kernel.Workitems.Strategies.Launch;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Kernel.Managers;
using Kernel.Configuration;
using System.Collections.Generic;

namespace Kernel.Workitems
{
    public class ContextService : IContextService
    {

        private Project Project { get; set; }

        private IContainerExtension Container { get; set; }

        private ITaskManager TaskManager { get; set; }

        private IUIManager UIManager { get; set; }

        internal IRegionManagerExtension RegionManager { get; set; }

        public ContextService(IRegionManagerExtension regionManager, IContainerExtension container, ITaskManager taskManager, IUIManager uiManager, Project project)
        {
            Container = container;
            RegionManager = regionManager;
            TaskManager = taskManager;
            UIManager = uiManager;
            Collection = new WorkitemCollection();
            Project = project;
        }

        public IReadOnlyCollection<IWorkItem> Workitems => Collection;

        internal WorkitemCollection Collection;

        public event NotifyCollectionChangedEventHandler WorkitemCollectionChanged
        {
            add
            {
                Collection.CollectionChanged += value;
            }
            remove
            {
                Collection.CollectionChanged -= value;
            }
        }

        public async Task FocusWorkitem(IWorkItem workItem)
        {
            await WorkitemFocusStrategy.GetFocusStrategy(this, workItem).Focus().ConfigureAwait(false);
        }

        // TODO: localize messages and have the applciation provide messages
        public async Task CloseAllWorkitems()
        {
            await TaskManager.Run(async () =>
            {
                bool canCloseAll = Collection.All(w => !w.IsDirty);
                MessageBoxResult res = MessageBoxResult.Yes;
                if (!canCloseAll)
                    res = UIManager.ShowMessageBox("Some tabs have unsaved changes, do you want to close all tabs?", "Closing", System.Windows.MessageBoxButton.YesNoCancel);
                else if (!UIManager.AskForConfirmation("Do you confirm to close all tabs?"))
                    return;
                if (res == MessageBoxResult.Yes)
                {
                    while (Collection.Count != 0)
                        await ForceCloseWorkitem(Collection[0]).ConfigureAwait(false);
                }
                else if (res == MessageBoxResult.No)
                {
                    for (int i = 0; i < Collection.Count; i++)
                    {
                        if (!Collection[i].IsDirty)
                        {
                            await ForceCloseWorkitem(Collection[i]).ConfigureAwait(false);
                            i--;
                        }
                    }
                }
            }).ConfigureAwait(false);
        }

        public Task<IObservable<WorkitemEventArgs>> LaunchWorkItem<T>(object data = null, IWorkItem parent = null) where T : IWorkItem
        {
            return LaunchWorkItem(typeof(T), parent, data, false);
        }

        public Task<IObservable<WorkitemEventArgs>> LaunchWorkItem(Type type, object data = null, IWorkItem parent = null)
        {
            return LaunchWorkItem(type, parent, data, false);
        }

        public Task<IObservable<WorkitemEventArgs>> LaunchModalWorkItem<T>(ModalOptions metadata, object data = null, IWorkItem parent = null) where T : IWorkItem
        {
            return LaunchWorkItem(typeof(T), parent, data, true, metadata);
        }

        public Task<IObservable<WorkitemEventArgs>> LaunchModalWorkItem<T>(object data = null, IWorkItem parent = null) where T : IWorkItem
        {
            return LaunchWorkItem(typeof(T), parent, data, true);
        }

        async Task<IObservable<WorkitemEventArgs>> LaunchWorkItem(Type type, IWorkItem parent, object data, bool isModal, ModalOptions metadata = null)
        {
            if (Project.IsConfigured<WorkitemBehaviorOptions>())
            {
                WorkitemBehaviorOptions options = Project.GetOption<WorkitemBehaviorOptions>();
                if (!await options.Behaviour.OnLaunching(this, new Behaviors.WorkitemLaunchDescriptor(type, parent == null, isModal)))
                    return null;
                
            }
            if (!typeof(IWorkItem).IsAssignableFrom(type))
                throw new ArgumentException("Workitem to be launched must be of type IWorkItem");
            IWorkItem workitem = (IWorkItem)Container.Resolve(type);
            WorkitemLaunchStrategy strategy = WorkitemLaunchStrategy.GetLaunchStrategy(this, workitem, parent, data);
            if (isModal)
                return await strategy.LaunchModal(metadata).ConfigureAwait(false);
            else
                return await strategy.Launch().ConfigureAwait(false);
        }

        internal async Task ForceCloseWorkitem(IWorkItem workitem)
        {
            CloseChildren(workitem);
            await WorkitemCloseStrategy.GetCloseStrategy(this, workitem).Close().ConfigureAwait(false);
        }

        public async void CloseChildren(IWorkItem workitem)
        {
            foreach (var w in Workitems.Where(w => w.Parent == workitem).ToList())
                await ForceCloseWorkitem(w);
        }


        public async Task<bool> CloseWorkitem(IWorkItem workitem)
        {
            if (workitem.IsOpen)
            {
                bool canClose = true;
                if (workitem.IsDirty)
                {
                    canClose = UIManager.AskForConfirmation("Closing the workitem may result in unsaved changes. Do you want to close it?");
                }
                if (canClose)
                {
                    await ForceCloseWorkitem(workitem);
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}

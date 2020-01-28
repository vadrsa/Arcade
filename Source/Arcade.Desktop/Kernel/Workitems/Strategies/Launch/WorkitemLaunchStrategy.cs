//using Kernel.Abstractions;
using Kernel.Configuration;
using Kernel.Managers;
using Kernel.Utility;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Kernel.Workitems.Strategies.Launch
{
    /// <summary>
    /// The Launch strategy for a workitem
    /// Any workitem should be opened by calling 
    /// WorkitemLaunchStrategy.Launch or WorkitemLaunchStrategy.LaunchModal methods
    /// </summary>
    internal abstract class WorkitemLaunchStrategy
    {
        /// <summary>
        /// The communication chanel of the workitem
        /// </summary>
        protected IObservable<WorkitemEventArgs> Channel;
        /// <summary>
        /// The Current ContextService
        /// </summary>
        protected ContextService CurrentContextService { get; private set; }
        /// <summary>
        /// Workitem to open
        /// </summary>
        protected IWorkItem Workitem { get; private set; }
        /// <summary>
        /// Parent workitem
        /// </summary>
        protected IWorkItem Parent { get; private set; }
        /// <summary>
        /// Initialization data
        /// </summary>
        protected object Data { get; private set; }
        /// <summary>
        /// Default or custom modal metadata
        /// depending if LaunchModal was called with null or not
        /// </summary>
        protected ModalOptions ModalMetadata { get; private set; }
        /// <summary>
        /// Applciation task manager
        /// </summary>
        protected ITaskManager TaskManager { get; private set; }
        /// <summary>
        /// Applciation UI Manager
        /// </summary>
        protected IUIManager UIManager { get; private set; }

        protected Project Project { get; private set; }
        /// <summary>
        /// Should the workitem be opened in modal state
        /// </summary>
        public bool ShouldOpenModal { get; private set; }

        internal WorkitemLaunchStrategy(ContextService currentContextService, IWorkItem workItem, IWorkItem parent = null, object data = null)
        {
            CurrentContextService = currentContextService;
            Workitem = workItem;
            Parent = parent;
            Data = data;
            TaskManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>();
            UIManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUIManager>();
            Project = CommonServiceLocator.ServiceLocator.Current.GetInstance<Project>();
        }

        /// <summary>
        /// Get the startegy to open the workitem
        /// The Launch startegy should be obtained from 
        /// here and never be instaciated outside of this method
        /// </summary>
        /// <param name="contextService">ContextService</param>
        /// <param name="workItem">Workitem to open</param>
        /// <param name="parent">Parent workitem</param>
        /// <param name="data">Initialization data</param>
        /// <returns></returns>
        public static WorkitemLaunchStrategy GetLaunchStrategy(ContextService contextService, IWorkItem workItem, IWorkItem parent = null, object data = null)
        {
            // If parent exists open with ChildWorkitemLaunchStrategy 
            // otherwise with RootWorkitemLaunchStrategy
            if (parent != null)
                return new ChildWorkitemLaunchStrategy(contextService, workItem, parent, data);
            else
                return new RootWorkitemLaunchStrategy(contextService, workItem, parent, data);
        }

        /// <summary>
        /// Instance specific launch logic
        /// </summary>
        /// <returns></returns>
        protected abstract Task Execute();

        /// <summary>
        /// Run the workitem
        /// Workitem.Run should be called from here only
        /// </summary>
        protected async Task RunWorkitem()
        {

            await Application.Current.Dispatcher.InvokeAsyncIfNeeded(async () =>
            {
                if (ShouldOpenModal)
                    Channel = await Workitem.RunModal().ConfigureAwait(false);
                else
                    Channel = await Workitem.Run().ConfigureAwait(false);
            }).ConfigureAwait(false);

        }

        /// <summary>
        /// Internal Launch implementation
        /// </summary>
        /// <param name="modalMetadata">Modal metadata</param>
        /// <returns></returns>
        private async Task<IObservable<WorkitemEventArgs>> LaunchInternal(ModalOptions modalMetadata = null)
        {

            try
            {
                //Logger.LogWithWorkitemData(String.Format("Opening workitem {0}{1}.", Workitem.WorkItemName, ShouldOpenModal ? " in modal state" : ""), LogLevel.Informative, Workitem);

                // Trick to let ui have some time to update its state
                await TaskManager.Run(() => Thread.Sleep(50)).ConfigureAwait(false);

                // Get the workitem type
                Type type = Workitem.GetType();

                // If workitem is single instance and there is alredy a workitem
                // with the same type open focus the opened otherwise continue launching 
                SingleInstanceWorkitemAttribute attribute = type.GetCustomAttributes(typeof(SingleInstanceWorkitemAttribute), false).FirstOrDefault() as SingleInstanceWorkitemAttribute;
                if (attribute != null)
                {
                    IWorkItem exists = CurrentContextService.Collection.Where(w => w.GetType().Equals(type)).FirstOrDefault();
                    if (exists != null)
                    {
                        await CurrentContextService.FocusWorkitem(exists).ConfigureAwait(false);
                        return null;
                    }
                }

                // If workitem supports initialization than do initialize it
                if (Workitem is ISupportsInitialization)
                {
                    var initable = Workitem as ISupportsInitialization;
                    //Logger.LogWithWorkitemData("Initializing workitem", LogLevel.Informative, Workitem);
                    try
                    {
                        // initialize
                        initable.Initialize(Data);
                    }
                    catch (Exception e)
                    {
                        // Log error with workitem data
                        //Logger.LogWithWorkitemData("Workitem initialization failed", LogLevel.Exception, Workitem, e);
                        // Show error to the UI
                        UIManager.Error("Failed to Initialize Workitem");
                        return null;
                    }
                }

                // Log
                //Logger.LogWithWorkitemData("Configuring Workitem", LogLevel.Informative, Workitem);

                // Configure Workitem
                Workitem.Configure();

                // if should open modal set the workitem window
                if (ShouldOpenModal)
                {
                    // if no custom modalMetadata is provided get the workitem default
                    if (modalMetadata == null)
                        ModalMetadata = Workitem.Configuration.GetOption<ModalOptions>();
                    else
                        ModalMetadata = modalMetadata;

                    // Set the workitem window in UI thread
                    Application.Current.Dispatcher.InvokeIfNeeded(() => Workitem.Window = InitModalWindow(Project.GetOption<RegionOptions>().GetModalWindow()));

                }

                // Execute isnatce specific launching
                await Execute().ConfigureAwait(false);

                // If Workitem is NullWorkitem set the WorkitemCollection.Null
                // otherwise add to the collection
                if (Workitem is NullWorkitem)
                    CurrentContextService.Collection.Null = Workitem;
                else if (!ShouldOpenModal || !ModalMetadata.IsDialog)
                    CurrentContextService.Collection.Add(Workitem);

                // If should open modal open the window
                if (ShouldOpenModal)
                {
                    //// Log
                    //Logger.LogWithWorkitemData("Showing modal window", LogLevel.Informative, Workitem);
                    // If should open in dialog mode call Workitem.Window.ShowDialog 
                    if (ModalMetadata.IsDialog)
                    {
                        //// end loading now because after show dialog is execution will be blocked
                        //CurrentContextService.EndLoading();
                        // Show dialog
                        Application.Current.Dispatcher.InvokeIfNeeded(() => Workitem.Window.ShowDialog());
                    }
                    else
                        Application.Current.Dispatcher.InvokeIfNeeded(() => Workitem.Window.Show());


                }

                if (!ShouldOpenModal || !ModalMetadata.IsDialog)
                    await CurrentContextService.FocusWorkitem(Workitem).ConfigureAwait(false);
                // return the communication channel
                return Channel;
            }
            catch (Exception e)
            {
                // Try and fix the broken parts
                CurrentContextService.Collection.Remove(Workitem);
                Workitem.Dispose();
                //// Log workitem exception
                //Logger.LogWithWorkitemData("Failed to open workitem", LogLevel.Exception, Workitem, e);
                // Show error in the UI
                UIManager.Error("Failed to open workitem");
                // return an empty channel
                return Observable.Empty<WorkitemEventArgs>();
            }
            finally
            {
                //// finally end loding for modal
                //if (ShouldOpenModal)
                //    CurrentContextService.EndLoading();
            }
        }

        /// <summary>
        /// Launch the workitem
        /// </summary>
        /// <returns>Communication channel</returns>
        public Task<IObservable<WorkitemEventArgs>> Launch()
        {
            return LaunchInternal();
        }

        /// <summary>
        /// Launch the workitem in modal state
        /// </summary>
        /// <param name="modalWorkitemMetadata"> custom modal metadata</param>
        /// <returns>Communication channel</returns>
        public Task<IObservable<WorkitemEventArgs>> LaunchModal(ModalOptions modalWorkitemMetadata)
        {
            ShouldOpenModal = true;
            return LaunchInternal(modalWorkitemMetadata);
        }

        private IModalWindow InitModalWindow(IModalWindow popup)
        {
            var metadata = ModalMetadata;
            if (metadata == null)
                metadata = Workitem.Configuration.GetOption<ModalOptions>();
            global::Prism.Regions.RegionManager.SetRegionManager(popup.GetRegionHolder(), global::Prism.Regions.RegionManager.GetRegionManager(Application.Current.MainWindow));
            global::Prism.Regions.RegionManager.UpdateRegions();
            popup.WindowStartupLocation = metadata.WindowStartupLocation;
            if (!metadata.Size.IsEmpty)
            {
                popup.Width = metadata.Size.Width;
                popup.Height = metadata.Size.Height;
            }
            popup.ShowIcon = metadata.ShowIcon;
            popup.ResizeMode = metadata.ResizeMode;
            popup.Title = Workitem.WorkItemName;
            popup.Closing += (o, args) => ModalWindowClosing(Workitem, args);
            return popup;
        }

        private static async void ModalWindowClosing(IWorkItem workitem, System.ComponentModel.CancelEventArgs e)
        {
            if (!(await workitem.Close()))
            {
                e.Cancel = true;
                return;
            }

            global::Prism.Regions.RegionManager.SetRegionManager(workitem.Window.GetRegionHolder(), null);
            global::Prism.Regions.RegionManager.UpdateRegions();
        }
    }
}

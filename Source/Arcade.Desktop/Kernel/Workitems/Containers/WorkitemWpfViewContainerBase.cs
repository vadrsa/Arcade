using Kernel.Prism;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Windows;

namespace Kernel.Workitems
{
    abstract class WorkitemWpfViewContainerBase : WorkitemViewContainerBase
    {
        protected IRegionManagerExtension RegionManager { get; private set; }
        protected IContainerExtension Container { get; private set; }
        protected IRegionTransformationCollection TransformationCollection { get; private set; }

        public WorkitemWpfViewContainerBase(IWorkItem workItem, IRegionManagerExtension regionManager, IContainerExtension container) : base(workItem)
        {
            RegionManager = regionManager;
            Container = container;
            TransformationCollection = container.Resolve<IRegionTransformationCollection>();
        }


        protected override object RegisterView(object view, string region)
        {
            if (view is DependencyObject)
                WorkitemManager.SetOwner(view as DependencyObject, WorkItem);

            if (view is FrameworkElement)
            {
                object viewModel = ((FrameworkElement)view).DataContext;
                if (viewModel is IWorkitemAware)
                    ((IWorkitemAware)viewModel).SetWorkitem(WorkItem);

                if (view is IDisposable)
                    Disposable((IDisposable)view);

                if (viewModel is IDisposable)
                    Disposable((IDisposable)viewModel);

            }
            return view;
        }

        protected override void UnregisterView(object view, string region)
        {
        }
    }
}

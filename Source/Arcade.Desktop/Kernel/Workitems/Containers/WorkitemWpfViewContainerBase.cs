using Kernel.Prism;
using Kernel.Utility;
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
                if (viewModel != null)
                {
                    var interfaceImplType = viewModel.GetType().GetGenericInterface(typeof(IWorkitemAware<>));
                    if (interfaceImplType != null)
                    {
                        var initType = interfaceImplType.GetGenericArguments()[0];

                        // initialize
                        if (initType.IsAssignableFrom(WorkItem.GetType()))
                        {
                            var methodInfo = viewModel.GetType().GetMethod("SetWorkitem", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                            methodInfo.Invoke(viewModel, new object[] { WorkItem });
                        }
                        else
                            throw new ArgumentException($"Workitem supports initialization only by {initType}");
                    }

                    if (viewModel is IDisposable)
                        Disposable((IDisposable)viewModel);
                }

                if (view is IDisposable)
                    Disposable((IDisposable)view);


            }
            return view;
        }

        protected override void UnregisterView(object view, string region)
        {
        }
    }
}

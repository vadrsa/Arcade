using Infrastructure.Constants;
using Infrastructure.Mvvm;
using Kernel.Workitems;
using Prism.Ioc;
using System;
using System.Reactive.Linq;
using System.Windows;

namespace Infrastructure.Workitems
{
    public abstract class WorkitemWpfBase : WorkitemBase
    {
        public WorkitemWpfBase(IContainerExtension container) : base(container)
        {
        }

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(KnownCommands.CloseWorkitem, CloseCommand);
        }

        protected override IViewContainer CreateViewContainer()
        {
            IViewContainer container = base.CreateViewContainer();
            Disposable(container.WhenRegisteringView.Subscribe(ContainerWhenRegisteringView));
            return container;
        }

        private void ContainerWhenRegisteringView(object view)
        {

            if (view is FrameworkElement)
            {
                object viewModel = ((FrameworkElement)view).DataContext;

                if (view is IGridView && viewModel is IGridViewModel)
                    ((IGridViewModel)viewModel).Grid = ((IGridView)view).Grid;
            }
        }
    }
}

using AutoMapper;
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
        private IMapper _mapper;

        public IMapper Mapper => _mapper;

        public WorkitemWpfBase(IContainerExtension container) : base(container)
        {
            _mapper = container.Resolve<IMapper>();
        }

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(KnownCommands.CloseWorkitem, CloseCommand);
        }

        protected override IViewContainer CreateViewContainer()
        {
            IViewContainer container = base.CreateViewContainer();
            //Disposable(container.WhenRegisteringView.Subscribe(ContainerWhenRegisteringView));
            return container;
        }

        //private void ContainerWhenRegisteringView(object view)
        //{

        //    if (view is FrameworkElement)
        //    {
        //        object viewModel = ((FrameworkElement)view).DataContext;

        //    }
        //}
    }
}

using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel;
using Kernel.Workitems;
using Prism.Ioc;
using QueueModule.Services;
using QueueModule.Workitems.QueueDisplay.Views;

namespace QueueModule.Workitems.QueueDisplay
{
    class QueueDisplayWorkitem : WorkitemWpfBase, ISupportsInitialization<string>
    {
        private QueueDisplayViewModel viewModel;
        private string computerId;

        public QueueDisplayWorkitem(IContainerExtension container) : base(container)
        {
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            viewModel = (QueueDisplayViewModel)container.Register<QueueDisplayView>(new QueueDisplayView(), KnownRegions.Content).DataContext;
            viewModel.ComputerID = computerId;
            viewModel.Load();
        }

        public void Initialize(string id)
        {
            computerId = id;
        }

        public override string WorkItemName => "Queues";

    }
}

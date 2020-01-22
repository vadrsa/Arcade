using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel.Workitems;
using Prism.Ioc;
using System.Windows.Controls;

namespace GamesModule.Workitems
{
    class GamesDisplayWorkitem : WorkitemWpfBase
    {

        public override string WorkItemName => "Games";

        public GamesDisplayWorkitem(IContainerExtension container) : base(container)
        {
        }


        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new UserControl(), KnownRegions.Content);
        }
    }
}

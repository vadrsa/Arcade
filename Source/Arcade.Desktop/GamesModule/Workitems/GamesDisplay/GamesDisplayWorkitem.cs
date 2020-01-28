using GamesModule.Workitems.GameDetails;
using GamesModule.Workitems.GamesDisplay.Views;
using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel.Workitems;
using Prism.Ioc;

namespace GamesModule.Workitems.GamesDisplay
{
    class GamesDisplayWorkitem : WorkitemWpfBase
    {

        public override string WorkItemName => "Games";

        public GamesDisplayWorkitem(IContainerExtension container) : base(container)
        {
        }

        public void OpenDetails(string Id)
        {
            CurrentContextService.LaunchWorkItem<GameDetailsWorkitem>(Id, this);
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new GamesDisplayView(), KnownRegions.Content);
        }

        protected override void OnFocused()
        {
            base.OnFocused();
            CurrentContextService.CloseChildren(this);
        }
    }
}

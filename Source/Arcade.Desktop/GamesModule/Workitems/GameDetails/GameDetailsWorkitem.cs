using Arcade.ViewModels;
using GamesModule.Services;
using GamesModule.Workitems.GameDetails.Views;
using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel;
using Kernel.Workitems;
using Prism.Ioc;

namespace GamesModule.Workitems.GameDetails
{
    public class GameDetailsWorkitem : WorkitemWpfBase, ISupportsInitialization
    {
        string id;
        GameDetailsDisplayViewModel _viewModel;

        public GameDetailsWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Game Details";


        public void Initialize(object data)
        {
            id = (string)data;
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            _viewModel = (GameDetailsDisplayViewModel)container.Register<GameDetailsDisplayView>(new GameDetailsDisplayView(), KnownRegions.Content).DataContext;
        }

        protected override async void AfterWorkitemRun()
        {
            base.AfterWorkitemRun();
            _viewModel.LoadGame(id);
        }
    }
}

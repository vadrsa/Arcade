using GamesModule.Views;
using GamesModule.Workitems;
using GamesModule.Workitems.AddEditGame;
using GamesModule.Workitems.GameManager;
using GamesModule.Workitems.GamesDisplay;
using Infrastructure.Constants;
using Infrastructure.Mvvm;
using Infrastructure.Prism;
using Infrastructure.Security;
using Kernel;
using Prism.Ioc;
using System;
using System.Threading.Tasks;

namespace Modules
{
    public class GamesModule : Module
    {
        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            // hadck to create referance
            new Arcade.CustomControls.FaultedPage();
            RegionManager.AddToRegion(KnownRegions.MainMenu, new GamesDisplayButton());
            RegionManager.AddToRegion(KnownRegions.MainMenu, new GameManagerButton());
            CommandManager.RegisterCommand(global::GamesModule.Constants.Commands.OpenGamesWorkitem, new AsyncCommand(OpenGamesWorkitem));
            CommandManager.RegisterCommand(global::GamesModule.Constants.Commands.OpenGameManagerWorkitem, new SecureAsyncCommand(OpenGameManagerWorkietm));
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.Register<AddEditGameWorkitem>();
            containerRegistry.Register<GamesDisplayWorkitem>();
            containerRegistry.Register<GameManagerWorkitem>();
        }

        private async Task OpenGamesWorkitem()
        {
            await CurrentContextService.LaunchWorkItem<GamesDisplayWorkitem>();
        }

        private async Task OpenGameManagerWorkietm()
        {
            await CurrentContextService.LaunchWorkItem<GameManagerWorkitem>();
        }
    }
}

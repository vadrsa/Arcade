using GamesModule.Views;
using GamesModule.Workitems.GamesDisplay;
using Infrastructure.Constants;
using Infrastructure.Mvvm;
using Kernel;
using Kernel.Configuration;
using System.Threading.Tasks;

namespace GamesModule.Features
{
    public class GameDisplayFeature : Feature
    {

        public override void Attach()
        {
            base.Attach();

            RegionManager.AddToRegion(KnownRegions.MainMenu, new GamesDisplayButton());
            CommandManager.RegisterCommand(global::GamesModule.Constants.Commands.OpenGamesWorkitem, new AsyncCommand(OpenGamesWorkitem));

        }

        private async Task OpenGamesWorkitem()
        {
            await CurrentContextService.LaunchWorkItem<GamesDisplayWorkitem>();
        }
    }
}

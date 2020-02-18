using GamesModule.Views;
using GamesModule.Workitems.GameManager;
using Infrastructure.Constants;
using Infrastructure.Security;
using Kernel;
using Kernel.Configuration;
using System.Threading.Tasks;

namespace GamesModule.Features
{
    public class GamesEditFeature : Feature
    {

        public override void Attach()
        {
            base.Attach();
            RegionManager.AddToRegion(KnownRegions.MainMenu, new GameManagerButton());
            CommandManager.RegisterCommand(global::GamesModule.Constants.Commands.OpenGameManagerWorkitem, new SecureAsyncCommand(OpenGameManagerWorkietm));
        }

        private async Task OpenGameManagerWorkietm()
        {
            await CurrentContextService.LaunchWorkItem<GameManagerWorkitem>();
        }
    }
}

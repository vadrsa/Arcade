using Infrastructure.Constants;
using Infrastructure.Security;
using Kernel;
using Kernel.Configuration;
using StaffModule.Views;
using StaffModule.Workitems.SessionManager;
using System.Threading.Tasks;

namespace StaffModule.Features
{
    public class ManageSessionsFeature : Feature
    {
        public override void Attach()
        {
            base.Attach();

            RegionManager.AddToRegion(KnownRegions.MainMenu, new SessionManagerButton());

            CommandManager.RegisterCommand(global::StaffModule.Constants.Commands.OpenSessionManagerWorkitem, new SecureAsyncCommand(OpenSessionManager));
        }

        private async Task OpenSessionManager()
        {
            await CurrentContextService.LaunchWorkItem<SessionManagerWorkitem>();
        }
    }
}

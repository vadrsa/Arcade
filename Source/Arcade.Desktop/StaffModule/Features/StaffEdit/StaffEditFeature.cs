using Infrastructure.Constants;
using Infrastructure.Security;
using Kernel;
using Kernel.Configuration;
using SharedEntities;
using StaffModule.Views;
using StaffModule.Workitems.StaffManager;
using System.Threading.Tasks;

namespace StaffModule.Features
{
    public class StaffEditFeature : Feature
    {
        public override void Attach()
        {
            base.Attach();

            RegionManager.AddToRegion(KnownRegions.MainMenu, new StaffManagerButton());
            CommandManager.RegisterCommand(global::StaffModule.Constants.Commands.OpenStaffManagerWorkitem, new SecureAsyncCommand(OpenStaffWorkitem, ApplicationRole.Admin));
        }

        private async Task OpenStaffWorkitem()
        {
            await CurrentContextService.LaunchWorkItem<StaffManagerWorkitem>();
        }
    }
}

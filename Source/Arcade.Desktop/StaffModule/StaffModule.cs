using Infrastructure.Constants;
using Infrastructure.Mvvm;
using Infrastructure.Prism;
using Infrastructure.Security;
using Kernel;
using Prism.Ioc;
using SharedEntities;
using StaffModule.Views;
using StaffModule.Workitems.StaffManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class StaffModule : Module
    {

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);

            new Arcade.CustomControls.FaultedPage();
            RegionManager.AddToRegion(KnownRegions.MainMenu, new StaffManagerButton());
            CommandManager.RegisterCommand(global::StaffModule.Constants.Commands.OpenStaffManagerWorkitem, new SecureAsyncCommand(OpenStaffWorkitem, ApplicationRole.Admin));
        }

        private async Task OpenStaffWorkitem()
        {
            await CurrentContextService.LaunchWorkItem<StaffManagerWorkitem>();
        }
    }
}

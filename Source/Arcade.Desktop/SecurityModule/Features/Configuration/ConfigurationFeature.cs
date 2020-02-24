using Infrastructure.Constants;
using Infrastructure.Mvvm;
using Infrastructure.Security;
using Kernel;
using Kernel.Configuration;
using SecurityModule.Views;
using SecurityModule.Workitems.Configuration;
using System.Threading.Tasks;

namespace SecurityModule.Features
{
    public class ConfigurationFeature : Feature
    {
        public override void Attach()
        {
            base.Attach();

            RegionManager.AddToRegion(KnownRegions.MainMenu, new ConfigurationButton());
            CommandManager.RegisterCommand(global::SecurityModule.Constants.Commands.OpenConfigurationWorkitem, new SecureAsyncCommand(OpenConfigurationWorkitem, SharedEntities.ApplicationRole.Admin));

        }

        private async Task OpenConfigurationWorkitem()
        {
            await CurrentContextService.LaunchWorkItem<ConfigurationWorkitem>();
        }
    }
}

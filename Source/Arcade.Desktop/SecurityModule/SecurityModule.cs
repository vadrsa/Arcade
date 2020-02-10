using Infrastructure.Constants;
using Infrastructure.Prism;
using Infrastructure.Security;
using Kernel;
using Prism.Ioc;
using SecurityModule.Views;
using SecurityModule.Workitems.Configuration;
using SecurityModule.Workitems.Login;
using SharedEntities;
using System;
using System.Threading.Tasks;

namespace Modules
{
    public class SecurityModule : Module
    {
        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            RegionManager.AddToRegion(KnownRegions.MainMenu, new ConfigurationButton());
            CommandManager.RegisterCommand(global::SecurityModule.Constants.Commands.OpenConfigurationWorkitem, new SecureAsyncCommand(OpenConfigurationWorkitem, ApplicationRole.Admin));

        }

        private async Task OpenConfigurationWorkitem()
        {
            await CurrentContextService.LaunchWorkItem<ConfigurationWorkitem>();
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.Register<LoginWorkitem>();
        }

    }
}

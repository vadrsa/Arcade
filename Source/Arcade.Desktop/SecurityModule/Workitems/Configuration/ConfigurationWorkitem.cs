using Infrastructure.Constants;
using Infrastructure.Mvvm;
using Infrastructure.Security;
using Infrastructure.Workitems;
using Kernel.Workitems;
using Prism.Ioc;
using SecurityModule.Workitems.ArcadeSettings;
using SecurityModule.Workitems.ComputerSettings;
using SecurityModule.Workitems.Configuration.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityModule.Workitems.Configuration
{
    public class ConfigurationWorkitem : WorkitemWpfBase
    {
        public ConfigurationWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override bool SupportsMultiFocus => true;

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new ConfigurationMenu(), KnownRegions.SecondaryMenu);
        }

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(SecurityModule.Constants.Commands.OpenSystemSettingsWorkitem, new AsyncCommand(OpenSystemSettingsWorkitem));
            container.Register(SecurityModule.Constants.Commands.OpenComputerSettingsWorkitem, new AsyncCommand(OpenComputerSettingsWorkitem));
            container.Register(SecurityModule.Constants.Commands.OpenArcadeSettingsWorkitem, new AsyncCommand(OpenArcadeSettingsWorkitem));
        }


        private async Task OpenArcadeSettingsWorkitem()
        {
            await CurrentContextService.LaunchWorkItem<ArcadeSettingsWorkitem>(parent: this);
        }

        private async Task OpenSystemSettingsWorkitem()
        {
            await CurrentContextService.LaunchWorkItem<SystemSettingsWorkitem>(parent: this);
        }

        private async Task OpenComputerSettingsWorkitem()
        {
            await CurrentContextService.LaunchWorkItem<ComputerSettingsWorkitem>(parent: this);
        }

        public override string WorkItemName => "Configuration";

    }
}

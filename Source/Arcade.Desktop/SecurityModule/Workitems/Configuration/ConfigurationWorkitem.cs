using Arcade.ViewModels;
using Infrastructure.Constants;
using Infrastructure.ObjectManagement;
using Infrastructure.Workitems;
using Kernel.Workitems;
using Prism.Ioc;
using SecurityModule.Workitems.Configuration.Views;
using SecurityModule.Workitems.ConfigurationEdit;

namespace SecurityModule.Workitems.Configuration
{
    public class ConfigurationWorkitem : ObjectManagerWorkitem<ConfigurationView, SystemSettingViewModel, SystemSettingViewModel, ConfigurationEditWorkitem>
    {
        public ConfigurationWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Configuration";
    }
}

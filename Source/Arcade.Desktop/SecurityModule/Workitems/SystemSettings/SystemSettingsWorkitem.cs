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
    public class SystemSettingsWorkitem : ObjectManagerWorkitem<ConfigurationView, SystemSettingViewModel, SystemSettingViewModel, SystemSettingEditWorkitem>
    {
        public SystemSettingsWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "System Settings";
    }
}

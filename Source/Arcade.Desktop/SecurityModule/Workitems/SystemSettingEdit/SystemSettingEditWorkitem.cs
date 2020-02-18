using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using Prism.Ioc;
using SecurityModule.Services;
using SecurityModule.Workitems.ConfigurationEdit.Views;

namespace SecurityModule.Workitems.ConfigurationEdit
{
    public class SystemSettingEditWorkitem : ObjectManagerDetailsWorkitem<ConfigurationEditView, SystemSettingViewModel, SystemSettingViewModel, int>
    {
        public SystemSettingEditWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Edit System Setting";

        protected override IObjectManagementService<SystemSettingViewModel, SystemSettingViewModel, int> ObjectManagementService => Container.Resolve<SystemSettingOMService>();
    }
}

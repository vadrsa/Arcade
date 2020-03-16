using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using Prism.Ioc;
using SecurityModule.Services;
using SecurityModule.Workitems.ConfigurationEdit.Views;
using SecurityModule.Workitems.SystemSettingEdit.Views;
using System;

namespace SecurityModule.Workitems.ConfigurationEdit
{
    public class SystemSettingEditWorkitem : ObjectManagerDetailsWorkitem<ConfigurationEditView, SystemSettingViewModel, SystemSettingViewModel, int>
    {
        public SystemSettingEditWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Edit System Setting";

        protected override IObjectManagementService<SystemSettingViewModel, SystemSettingViewModel, int> ObjectManagementService => Container.Resolve<SystemSettingOMService>();

        protected override Type ViewType
        {
            get
            {
                switch (Data.Details.Setting)
                {
                    case SharedEntities.SystemSettingType.QueuePrintDimensions:
                        return typeof(QueueDimensionsEdit);
                    default:
                        return base.ViewType;
                }
            }
        }
    }
}

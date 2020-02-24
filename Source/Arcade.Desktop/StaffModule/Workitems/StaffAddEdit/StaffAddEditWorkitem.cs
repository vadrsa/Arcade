using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using Prism.Ioc;
using StaffModule.Services;
using StaffModule.Workitems.StaffAddEdit.Views;

namespace StaffModule.Workitems.StaffAddEdit
{
    public class StaffAddEditWorkitem : ObjectManagerDetailsWorkitem<StaffDetailsView, EmployeeViewModel, EmployeeUploadViewModel, string>
    {
        public StaffAddEditWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Add/Edit Staff";

        protected override IObjectManagementService<EmployeeViewModel, EmployeeUploadViewModel, string> ObjectManagementService => Container.Resolve<StaffManagerOMService>();

    }
}

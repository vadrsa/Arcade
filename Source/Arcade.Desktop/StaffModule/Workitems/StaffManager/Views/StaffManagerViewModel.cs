using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using Prism.Ioc;
using StaffModule.Services;

namespace StaffModule.Workitems.StaffManager.Views
{
    class StaffManagerViewModel : ObjectManagerViewModel<EmployeeViewModel, EmployeeUploadViewModel, string>
    {
        protected override IObjectManagementService<EmployeeViewModel, EmployeeUploadViewModel, string> ObjectManagementService => ContainerProvider.Resolve<StaffManagerOMService>();
    }
}

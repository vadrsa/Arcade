using Arcade.Management.ViewModels;
using Arcade.StaffModule.Workitems.StaffManagement.Views;
using Infrastructure.ObjectManagment;
using Prism.Ioc;

namespace Arcade.StaffModule.Workitems.StaffManagement
{
    public class StaffManagementWorkitem : ObjectManagerWorkitem<StaffManagerView, EmployeeViewModel, EmployeeViewModel, string>
    {
        public StaffManagementWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Staff";
    }
}

using Arcade.Management.ViewModels;
using Infrastructure.ObjectManagment;

namespace Arcade.StaffModule.Workitems.StaffManagement.Views
{
    /// <summary>
    /// Details Part
    /// </summary>
    public partial class StaffManagerViewModel : ObjectManagerViewModel<EmployeeViewModel, EmployeeViewModel, string>
    {

        private bool CanEditObject()
        {
            return !IsReadOnly;
        }

        protected override EmployeeViewModel CreateEmptyDetails()
        {
            return new EmployeeViewModel();
        }

    }
}

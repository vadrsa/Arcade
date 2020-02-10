
using Arcade.Management.ViewModels;
using Infrastructure.ObjectManagment;
using System.Collections.ObjectModel;

namespace Arcade.StaffModule.Workitems.StaffManagement.Views
{
    /// <summary>
    /// List Part
    /// </summary>
    public partial class StaffManagerViewModel : ObjectManagerViewModel<EmployeeViewModel, EmployeeViewModel, string>
    {

        private ObservableCollection<EmployeeViewModel> products;

        public override ObservableCollection<EmployeeViewModel> ListItems
        {
            get { return products; }
            set { SetProperty(ref products, value, nameof(ListItems)); }
        }

    }
}
using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using Prism.Ioc;
using StaffModule.Workitems.SessionManagerDetails;
using StaffModule.Workitems.StaffAddEdit;
using StaffModule.Workitems.StaffManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffModule.Workitems.StaffManager
{
    class StaffManagerWorkitem : ObjectManagerWorkitem<StaffManagerView, EmployeeViewModel, EmployeeUploadViewModel, StaffAddEditWorkitem>
    {
        public StaffManagerWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Staff";

        
    }
}

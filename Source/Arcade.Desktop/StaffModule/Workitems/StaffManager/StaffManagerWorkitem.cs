using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using Kernel.Workitems;
using Prism.Ioc;
using StaffModule.Workitems.DatePicker;
using StaffModule.Workitems.EmployeeReport;
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

        public async Task OpenReport(string id)
        {
            var channel = await CurrentContextService.LaunchModalWorkItem<DatePickerWorkitem>(null, this);
            channel.Subscribe(async (value) => {

                if (value.Data is DateTime)
                {
                    var initializer = new EmployeeReportWorkitemInitializer { Id = id, Date = (DateTime)value.Data };
                    await CurrentContextService.LaunchModalWorkItem<EmployeeReportWorkitem>(initializer, this);
                }
            });
        }
    }
}

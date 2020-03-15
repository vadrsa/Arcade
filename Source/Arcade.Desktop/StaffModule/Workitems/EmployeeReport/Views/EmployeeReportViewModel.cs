using Arcade.ViewModels;
using Infrastructure.Mvvm;
using StaffModule.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StaffModule.Workitems.EmployeeReport.Views
{
    class EmployeeReportViewModel : WorkitemViewModel<EmployeeReportWorkitem>
    {
        private string id;
        private DateTime date;

        private EmployeeReportDataViewModel _report;
        public EmployeeReportDataViewModel Report
        {
            get { return _report; }
            set { Set(ref _report, value, nameof(Report)); }
        }

        public void Load(string id, DateTime date)
        {
            this.id = id;
            this.date = date;
            Load();
        }

        protected override async Task DoLoad(CancellationToken token)
        {
            await LoadCustom(async _ =>
            {
                Report = Mapper.Map<EmployeeReportDataViewModel>(await new StaffService().GetReport(id, date.Date.ToUniversalTime().Date));
                Report.Date = date;
            });
        }

    }
}

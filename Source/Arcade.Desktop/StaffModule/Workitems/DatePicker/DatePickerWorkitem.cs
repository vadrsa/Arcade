using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel.Workitems;
using Prism.Ioc;
using StaffModule.Workitems.DatePicker.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffModule.Workitems.DatePicker
{
    public class DatePickerWorkitem : WorkitemWpfBase
    {
        public DatePickerWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Date Picker";

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new DatePickerView(), KnownRegions.Content);
        }

        public async void OnDateSelected(DateTime date)
        {
            Communication.OnNext(new WorkitemEventArgs(this, date));
            await Close();
        }
    }
}

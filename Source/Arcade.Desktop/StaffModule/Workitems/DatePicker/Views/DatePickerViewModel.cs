using Infrastructure.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffModule.Workitems.DatePicker.Views
{
    public class DatePickerViewModel : WorkitemViewModel<DatePickerWorkitem>
    {
        private DateTime date = DateTime.Now;

        public DateTime Date
        {
            get { return date; }
            set { Set(ref date, value, nameof(Date)); }
        }

        private DelegateCommand _selectCommand;

        public DelegateCommand SelectCommand
        {
            get
            {
                if (_selectCommand == null)
                    _selectCommand = new DelegateCommand(Select);
                return _selectCommand;
            }
        }

        private void Select()
        {
            Workitem.OnDateSelected(Date);
        }
    }
}

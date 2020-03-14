using Infrastructure.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcade.ViewModels
{
    public class EmployeeReportDataViewModel : EditableViewModel<EmployeeReportDataViewModel>, IIdEntityViewModel<string>
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => Set(ref _id, value, nameof(Id));
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value, nameof(UserName));
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value, nameof(FirstName));
        }

        private string _lastname;
        public string LastName
        {
            get => _lastname;
            set => Set(ref _lastname, value, nameof(LastName));
        }

        private bool _isTerminated;
        public bool IsTerminated
        {
            get => _isTerminated;
            set => Set(ref _isTerminated, value, nameof(IsTerminated));
        }

        private double _AmountWorked;
        public double AmountWorked
        {
            get => _AmountWorked;
            set
            {
                Set(ref _AmountWorked, value, nameof(AmountWorked));
                RaisePropertyChanged(nameof(WorkedSpan));
            }
        }

        private TimeSpan _WorkedSpan;
        public TimeSpan WorkedSpan
        {
            get => TimeSpan.FromMinutes(_AmountWorked);
        }

        private List<ActivityViewModel> _Activities;
        public List<ActivityViewModel> Activities
        {
            get => _Activities;
            set => Set(ref _Activities, value, nameof(Activities));
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => Set(ref _date, value, nameof(Date));
        }

    }
}

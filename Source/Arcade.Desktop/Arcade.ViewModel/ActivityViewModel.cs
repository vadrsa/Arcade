using Infrastructure.Mvvm;
using SharedEntities;
using System;

namespace Arcade.ViewModels
{
    public class ActivityViewModel : EditableViewModel<ActivityViewModel>
    {
        private DateTime _Date;
        public DateTime Date
        {
            get => _Date;
            set => Set(ref _Date, value, nameof(Date));
        }

        private ActivityType _Type;
        public ActivityType Type
        {
            get => _Type;
            set => Set(ref _Type, value, nameof(Type));
        }

    }
}

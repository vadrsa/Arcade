using Infrastructure.Mvvm;

namespace Arcade.ViewModels
{
    public class ComputerTypeViewModel : EditableViewModel<ComputerTypeViewModel>, IIdEntityViewModel<string>
    {

        private string _id;
        public string Id
        {
            get => _id;
            set => Set(nameof(Id), ref _id, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(nameof(Name), ref _name, value);
        }

        private float _rate;
        public float HourlyRate
        {
            get => _rate;
            set => Set(nameof(HourlyRate), ref _rate, value);
        }

    }
}

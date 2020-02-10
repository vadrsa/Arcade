using Infrastructure.Mvvm;
using SharedEntities;

namespace Arcade.ViewModels
{
    public class SystemSettingViewModel : EditableViewModel<SystemSettingViewModel>, IIdEntityViewModel<int>
    {

        private int _id;
        public int Id
        {
            get => (int)Setting;
            set => Set(ref _id, value, nameof(Id));
        }

        private SystemSettingType _setting;
        public SystemSettingType Setting
        {
            get => _setting;
            set {
                Set(ref _setting, value, nameof(Setting));
                RaisePropertyChanged(nameof(Id));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value, nameof(Name));
        }

        private string _value;
        public string Value
        {
            get => _value;
            set => Set(ref _value, value, nameof(Value));
        }

    }

}

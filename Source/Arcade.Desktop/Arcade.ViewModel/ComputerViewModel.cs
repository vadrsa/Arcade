using Infrastructure.Mvvm;

namespace Arcade.ViewModels
{
    public class ComputerViewModel : EditableViewModel<ComputerViewModel>, IIdEntityViewModel<string>
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => Set(nameof(Id), ref _id, value);
        }

        private string _typeId;
        public string TypeId
        {
            get => _typeId;
            set => Set(nameof(TypeId), ref _typeId, value);
        }

        private int _number;
        public int Number
        {
            get => _number;
            set
            {
                Set(nameof(Number), ref _number, value);
                RaisePropertyChanged(nameof(ComputerFullName));
            }
        }

        private ComputerTypeViewModel _type;
        public ComputerTypeViewModel Type
        {
            get => _type;
            set {
                Set(nameof(Type), ref _type, value);
                TypeId = value?.Id;
                RaisePropertyChanged(nameof(ComputerFullName));
            }
        }

        private bool _isTerminated;
        public bool IsTerminated
        {
            get => _isTerminated;
            set => Set(nameof(IsTerminated), ref _isTerminated, value);
        }

        public string ComputerFullName
        {
            get
            {
                if (Type == null)
                    return null;
                return $"{Type.Name} #{Number}";
            }
        }
    }
}

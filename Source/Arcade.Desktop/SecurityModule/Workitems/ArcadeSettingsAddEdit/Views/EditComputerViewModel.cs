using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using System.Collections.ObjectModel;

namespace SecurityModule.Workitems.ArcadeSettingsAddEdit.Views
{
    public class EditComputerViewModel : ObjectManagerDetailsViewModel<ComputerViewModel>
    {
        private ObservableCollection<ComputerTypeViewModel> _computerTypes;
        public ObservableCollection<ComputerTypeViewModel> ComputerTypes
        {
            get => _computerTypes;
            set => Set(ref _computerTypes, value, nameof(ComputerTypes));
        }
    }
}

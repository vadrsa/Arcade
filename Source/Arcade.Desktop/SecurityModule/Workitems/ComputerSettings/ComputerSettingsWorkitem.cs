using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using Prism.Ioc;
using SecurityModule.Workitems.ComputerSettings.Views;
using SecurityModule.Workitems.ComputerSettingsAddEdit;

namespace SecurityModule.Workitems.ComputerSettings
{
    class ComputerSettingsWorkitem : ObjectManagerWorkitem<ComputerTypesView, ComputerTypeViewModel, ComputerTypeViewModel, ComputerSettingsAddEditWorkitem>
    {
        public ComputerSettingsWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Computer Settings";
    }
}

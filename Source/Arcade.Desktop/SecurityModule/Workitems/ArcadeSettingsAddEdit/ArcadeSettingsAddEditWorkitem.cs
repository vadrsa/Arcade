using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using Prism.Ioc;
using SecurityModule.Services;
using SecurityModule.Workitems.ArcadeSettings;
using SecurityModule.Workitems.ArcadeSettingsAddEdit.Views;
using System.Collections.Generic;

namespace SecurityModule.Workitems.ComputerSettingsAddEdit
{
    public class ArcadeSettingsAddEditWorkitem : ObjectManagerDetailsWorkitem<EditComputerView, ComputerViewModel, ComputerViewModel, string>
    {
        public ArcadeSettingsAddEditWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Add/Edit Arcade Computer";

        protected override IObjectManagementService<ComputerViewModel, ComputerViewModel, string> ObjectManagementService => Container.Resolve<ComputersOMService>();

        protected override void AfterWorkitemRun()
        {
            base.AfterWorkitemRun();
            var computerTypes = Parent.RequestResource(ArcadeSettingsWorkitem.ComputerTypesResourceKey);
            if (computerTypes == null) return;
            var castedToList = (List<ComputerTypeViewModel>)computerTypes;
            ((EditComputerViewModel)ViewModel).ComputerTypes = new System.Collections.ObjectModel.ObservableCollection<ComputerTypeViewModel>(castedToList);
        }

    }
}

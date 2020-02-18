using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using SecurityModule.Services;
using SecurityModule.Workitems.ComputerSettingsAddEdit.Views;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityModule.Workitems.ComputerSettingsAddEdit
{
    public class ComputerSettingsAddEditWorkitem : ObjectManagerDetailsWorkitem<EditComputerTypeView, ComputerTypeViewModel, ComputerTypeViewModel, string>
    {
        public ComputerSettingsAddEditWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Add/Edit Computer Type";

        protected override IObjectManagementService<ComputerTypeViewModel, ComputerTypeViewModel, string> ObjectManagementService => Container.Resolve<ComputerTypesOMService>();
    }
}

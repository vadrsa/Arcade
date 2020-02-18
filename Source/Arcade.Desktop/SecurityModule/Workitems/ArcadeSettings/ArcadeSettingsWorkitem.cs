using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using Prism.Ioc;
using SecurityModule.Workitems.ArcadeSettings.Views;
using SecurityModule.Workitems.ComputerSettingsAddEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityModule.Workitems.ArcadeSettings
{
    class ArcadeSettingsWorkitem : ObjectManagerWorkitem<ComputersView, ComputerViewModel, ComputerViewModel, ArcadeSettingsAddEditWorkitem>
    {
        public const string ComputerTypesResourceKey = "ComputerTypesResourceKey";

        public ArcadeSettingsWorkitem(IContainerExtension container) : base(container)
        {
        }

        protected override void AfterWorkitemRun()
        {
            base.AfterWorkitemRun();
        }

        public override string WorkItemName => "Arcade Settings";


        public void RegisterComputerTypes(List<ComputerTypeViewModel> computerTypes)
        {
            if(!HasResource(ComputerTypesResourceKey))
                Resource(ComputerTypesResourceKey, () => computerTypes);
        }
    }
}

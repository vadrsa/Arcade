using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using SecurityModule.Services;
using System;
using Prism.Ioc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityModule.Workitems.ComputerSettings.Views
{
    public class ComputerTypesViewModel : ObjectManagerViewModel<ComputerTypeViewModel, ComputerTypeViewModel, string>
    {
        protected override IObjectManagementService<ComputerTypeViewModel, ComputerTypeViewModel, string> ObjectManagementService => ContainerProvider.Resolve<ComputerTypesOMService>();
    }
}

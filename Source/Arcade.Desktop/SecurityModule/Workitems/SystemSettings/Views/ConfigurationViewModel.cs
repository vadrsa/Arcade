using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using SecurityModule.Services;
using Prism.Ioc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityModule.Workitems.Configuration.Views
{
    public class ConfigurationViewModel : ObjectManagerViewModel<SystemSettingViewModel, SystemSettingViewModel, int>
    {
        protected override IObjectManagementService<SystemSettingViewModel, SystemSettingViewModel, int> ObjectManagementService => ContainerProvider.Resolve<SystemSettingOMService>();
    }
}

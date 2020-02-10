using Arcade.StaffModule.Services;
using Arcade.StaffModule.Workitems.StaffManagement;
using Arcade.StaffModule.Workitems.StaffManagement.Services;
using Infrastructure.Prism;
using Prism.Ioc;

namespace Modules
{
    public class StaffModule : Module
    {

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            CurrentContextService.LaunchWorkItem<StaffManagementWorkitem>();
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.RegisterSingleton<EmployeeService>();
            containerRegistry.RegisterSingleton<StaffManagerOMService>();
        }
    }
}

using Arcade.ViewModels;
using Infrastructure.Api;
using Infrastructure.ObjectManagement;
using Prism.Ioc;
using StaffModule.Services;
using System;
using System.Threading.Tasks;

namespace StaffModule.Workitems.StaffManager.Views
{
    class StaffManagerViewModel : ObjectManagerViewModel<EmployeeViewModel, EmployeeUploadViewModel, string>
    {
        protected override IObjectManagementService<EmployeeViewModel, EmployeeUploadViewModel, string> ObjectManagementService => ContainerProvider.Resolve<StaffManagerOMService>();

        protected override async Task DoDelete(string id)
        {
            EditCommand.RaiseCanExecuteChanged();
            try
            {
                await ObjectManagementService.Remove(id);
                Reload();
            }
            catch (Exception e)
            {
                var response = await ApiExceptionHandling.GetResponse(e);
                UIManager.Error(response.Message);
            }
        }
    }
}

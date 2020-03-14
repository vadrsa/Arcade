using Arcade.ViewModels;
using Infrastructure.Api;
using Infrastructure.Mvvm;
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

        private AsyncCommand<string> _terminateCommand;
        public AsyncCommand<string> TerminateCommand
        {
            get
            {
                if (_terminateCommand == null)
                    _terminateCommand = new AsyncCommand<string>(Terminate);
                return _terminateCommand;
            }
        }

        private async Task Terminate(string id)
        {
            EditCommand.RaiseCanExecuteChanged();
            try
            {
                await new StaffService().Terminate(id);
                Reload();
            }
            catch (Exception e)
            {
                var response = await ApiExceptionHandling.GetResponse(e);
                UIManager.Error(response.Message);
            }
        }

        private AsyncCommand<string> _showReportCommand;
        public AsyncCommand<string> ShowReportCommand
        {
            get
            {
                if (_showReportCommand == null)
                    _showReportCommand = new AsyncCommand<string>(ShowReport);
                return _showReportCommand;
            }
        }

        private async Task ShowReport(string id) {

            await ((StaffManagerWorkitem)Workitem).OpenReport(id);
        }
    }
}

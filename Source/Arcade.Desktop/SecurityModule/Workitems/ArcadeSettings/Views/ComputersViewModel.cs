using Arcade.ViewModels;
using Infrastructure.Api;
using Infrastructure.Mvvm;
using Infrastructure.ObjectManagement;
using Prism.Commands;
using Prism.Ioc;
using SecurityModule.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SecurityModule.Workitems.ArcadeSettings.Views
{
    public class ComputersViewModel : ObjectManagerViewModel<ComputerViewModel, ComputerViewModel, string>
    {
        List<ComputerTypeViewModel> ComputerTypes;

        protected override IObjectManagementService<ComputerViewModel, ComputerViewModel, string> ObjectManagementService => ContainerProvider.Resolve<ComputersOMService>();

        private DelegateCommand<string> copyCommand;
        public DelegateCommand<string> CopyToClipboardCommand =>
            copyCommand ?? (copyCommand = new DelegateCommand<string>(CopyToClipboard));

        private void CopyToClipboard(string id)
        {
            Clipboard.SetText(id);
            MessageQueueContainer.Queue.Enqueue("Copied to clipboard.");
        }

        protected override async Task Edit(string id)
        {
            await LoadCustom(LoadTypes);
            await base.Edit(id);
        }

        protected async override Task Add()
        {
            await LoadCustom(LoadTypes);
            await base.Add();
        }

        private async Task LoadTypes(CancellationToken token)
        {

            var service = ContainerProvider.Resolve<ComputerTypesOMService>();
            if (ComputerTypes == null)
                ComputerTypes = await service.GetAll(token);
            ((ArcadeSettingsWorkitem)Workitem).RegisterComputerTypes(ComputerTypes);
        }

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

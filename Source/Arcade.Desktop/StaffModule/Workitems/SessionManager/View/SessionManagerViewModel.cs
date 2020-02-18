using Arcade.ViewModels;
using Infrastructure.Api;
using Infrastructure.Mvvm;
using Infrastructure.ObjectManagement;
using Prism.Commands;
using Prism.Ioc;
using SharedEntities;
using StaffModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffModule.Workitems.SessionManager.View
{
    public class SessionManagerViewModel : ObjectManagerViewModel<ComputerQueueViewModel, ComputerQueueViewModel, string>
    {
        protected override IObjectManagementService<ComputerQueueViewModel, ComputerQueueViewModel, string> ObjectManagementService => ContainerProvider.Resolve<SessionManagerOMService>();

        private AsyncCommand<string> _createCommand;

        public AsyncCommand<string> CreateCommand
        {
            get
            {
                if (_createCommand == null)
                    _createCommand = new AsyncCommand<string>(Create, CanCreate);
                return _createCommand;
            }
        }

        private bool CanCreate(string id)
        {
            var item = List.Where(c => c.Id == id).FirstOrDefault();
            if (item == null) return false;
            return item.Current == null;
        }

        private async Task Create(string id)
        {
            try
            {
                var item = await LoadCustom<ComputerQueueViewModel>((t) => GetForUpload(id, t));
                if (item == null) return;

                (await ((SessionManagerWorkitem)Workitem).RunCreateOrEnqueue(new SessionUploadViewModel { ComputerId = id, Type = item.Type }, true)).Subscribe(this);
            }
            catch (Exception e)
            {
                var response = await ApiExceptionHandling.GetResponse(e);
                UIManager.Error(response.Message);
            }
        }


        private AsyncCommand<string> _enqueueCommand;

        public AsyncCommand<string> EnqueueCommand
        {
            get
            {
                if (_enqueueCommand == null)
                    _enqueueCommand = new AsyncCommand<string>(Enqueue, CanEnqueue);
                return _enqueueCommand;
            }
        }

        private async Task Enqueue(string id)
        {
            try
            {
                var item = await LoadCustom<ComputerQueueViewModel>((t) => GetForUpload(id, t));
                if (item == null) return;

                (await ((SessionManagerWorkitem)Workitem).RunCreateOrEnqueue(new SessionUploadViewModel { ComputerId = id, Type = item.Type }, false)).Subscribe(this);
                Load();
            }
            catch (Exception e)
            {
                var response = await ApiExceptionHandling.GetResponse(e);
                UIManager.Error(response.Message);
            }
        }

        private bool CanEnqueue(string id)
        {
            var item = List.Where(c => c.Id == id).FirstOrDefault();
            if (item == null) return false;
            return item.Current != null;
        }

        private AsyncCommand<string> _EndSessionCommand;

        public AsyncCommand<string> EndSessionCommand
        {
            get
            {
                if (_EndSessionCommand == null)
                    _EndSessionCommand = new AsyncCommand<string>(EndSession, CanEndSession);
                return _EndSessionCommand;
            }
        }

        private async Task EndSession(string id)
        {
            try
            {
                await new SessionService().EndSession(id);
                Load();
            }
            catch (Exception e)
            {
                var response = await ApiExceptionHandling.GetResponse(e);
                UIManager.Error(response.Message);
            }
        }

        private bool CanEndSession(string id)
        {
            var item = List.Where(c => c.Id == id).FirstOrDefault();
            if (item == null) return false;
            return item.Current != null;
        }
    }
}

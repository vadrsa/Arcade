using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using Prism.Ioc;
using SharedEntities;
using StaffModule.Workitems.SessionManager.View;
using StaffModule.Workitems.SessionManagerDetails;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace StaffModule.Workitems.SessionManager
{
    public class SessionManagerWorkitem : ObjectManagerWorkitem<SessionManagerView, ComputerQueueViewModel, ComputerQueueViewModel, SessionManagerDetailsWorkitem>
    {
        public SessionManagerWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Session Manager";

        public async Task<IObservable<bool>> RunCreateOrEnqueue(SessionUploadViewModel sessionUpload, bool isAdding)
        {

            return (await CurrentContextService.LaunchModalWorkItem<SessionManagerDetailsWorkitem>(new ObjectManagerDetailsInitializer<SessionUploadViewModel> { Details = sessionUpload, IsAdding = isAdding }, this)).Where(w => w.Data is bool).Select(w => (bool)w.Data);
        }
    }
}

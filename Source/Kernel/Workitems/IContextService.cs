using Kernel.Workitems;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Kernel.Workitems
{
    public interface IContextService
    {
        Task<IObservable<WorkitemEventArgs>> LaunchWorkItem<T>(object data = null, IWorkItem parent = null) where T : IWorkItem;
        Task<IObservable<WorkitemEventArgs>> LaunchModalWorkItem<T>(object data = null, IWorkItem parent = null) where T : IWorkItem;
        Task FocusWorkitem(IWorkItem workItem);
        Task<bool> CloseWorkitem(IWorkItem workItem);
        Task CloseAllWorkitems();
        event NotifyCollectionChangedEventHandler WorkitemCollectionChanged;
    }
}

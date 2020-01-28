using Kernel.Workitems;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Kernel.Workitems
{
    public interface IContextService
    {
        IReadOnlyCollection<IWorkItem> Workitems { get; }
        Task<IObservable<WorkitemEventArgs>> LaunchWorkItem<T>(object data = null, IWorkItem parent = null) where T : IWorkItem;
        Task<IObservable<WorkitemEventArgs>> LaunchModalWorkItem<T>(object data = null, IWorkItem parent = null) where T : IWorkItem;
        Task FocusWorkitem(IWorkItem workItem);
        Task<bool> CloseWorkitem(IWorkItem workItem);
        Task CloseAllWorkitems();
        void CloseChildren(IWorkItem workitem);

        event NotifyCollectionChangedEventHandler WorkitemCollectionChanged;
    }
}

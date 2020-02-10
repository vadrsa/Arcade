using Kernel.Workitems;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectManagement
{
    public interface IObjectManagerWorkitem<TList, TDetails> : IWorkItem
    {
        Task<IObservable<bool>> RunAddEdit(TDetails details, bool isAdding);
        IContainerExtension Container { get; }
    }
}

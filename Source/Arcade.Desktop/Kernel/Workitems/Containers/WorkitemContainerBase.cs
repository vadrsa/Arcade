using System;
using System.Collections.Generic;

namespace Kernel.Workitems
{
    /// <summary>
    /// The base of all workitem containers
    /// </summary>
    public abstract class WorkitemContainerBase : IDisposableContainer, IDisposable
    {
        // Objects to dispose of on Dispose()
        private List<IDisposable> Disposables;

        protected IWorkItem WorkItem { get; set; }

        public WorkitemContainerBase(IWorkItem workItem)
        {
            WorkItem = workItem;
            Disposables = new List<IDisposable>();
        }

        /// <summary>
        /// Registers an object as a disposable that will be disposed of 
        /// when the  Dispose method of the class is called
        /// </summary>
        public T Disposable<T>(T obj) where T : IDisposable
        {
            Disposables.Add(obj);
            return obj;
        }

        /// <summary>
        /// Dispose of disposables
        /// </summary>
        public void Dispose()
        {
            Disposables.ForEach(d => d?.Dispose());
        }
    }
}

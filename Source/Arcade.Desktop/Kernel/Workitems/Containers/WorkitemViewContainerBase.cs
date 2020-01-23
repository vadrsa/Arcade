using Kernel.Utility;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Kernel.Workitems
{
    /// <summary>
    /// Base class for workitem view containers
    /// </summary>
    public abstract class WorkitemViewContainerBase : WorkitemContainerBase, IViewContainer
    {

        public WorkitemViewContainerBase(IWorkItem workItem) : base(workItem)
        {
            Views = new Dictionary<string, List<ViewDescriptor>>();
            // Clear views on Disopose
            Disposable(new DisposableAction(() => Views?.Clear()));
        }

        /// Keep track of aded views
        internal Dictionary<string, List<ViewDescriptor>> Views;

        public event Action<object> OnRegisteringView;
        public IObservable<object> WhenRegisteringView => Observable.FromEvent<object>(a => OnRegisteringView += a, a => OnRegisteringView -= a);

        /// <summary>
        /// Import views from another container
        /// </summary>
        /// <param name="viewContainer">Another container</param>
        public void ImportFrom(IViewContainer container)
        {
            if (container is WorkitemViewContainerBase)
            {
                WorkitemViewContainerBase viewContainer = (WorkitemViewContainerBase)container;
                foreach (string region in viewContainer.Views.Keys)
                {
                    foreach (ViewDescriptor view in viewContainer.Views[region])
                    {
                        viewContainer.UnregisterView(view.Modified, region);
                        RegisterView(view.Unmodified, region);
                    }
                }

            }

        }

        /// <summary>
        /// Register a view in a specific screen region
        /// </summary>
        /// <param name="view">the view to register</param>
        /// <param name="region">the region to register in</param>
        /// <returns>the view</returns>
        public object Register(object view, string region)
        {
            OnRegisteringView?.Invoke(view);
            object newView = RegisterView(view, region);
            ViewDescriptor descriptor = new ViewDescriptor(newView, view);
            if (Views.ContainsKey(region))
                Views[region].Add(descriptor);
            else
                Views.Add(region, new List<ViewDescriptor>() { descriptor });
            return view;
        }

        /// <summary>
        /// Unregister a view from a specific screen region
        /// </summary>
        /// <param name="view">the view to unregister</param>
        /// <param name="region">the region to unregister from</param>
        public void Unregister(object view, string region)
        {
            UnregisterView(view, region);
        }

        // Abstract Imlementation
        protected abstract object RegisterView(object view, string region);
        protected abstract void UnregisterView(object view, string region);



    }
}

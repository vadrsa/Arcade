using Infrastructure.Security;
using Kernel.Workitems;
using System;
using System.Collections.Generic;

namespace Infrastructure.Mvvm
{
    /// <summary>
    /// Base class for all WorkitemViewModel's
    /// </summary>
    public class WorkitemViewModel : BaseViewModel, IWorkitemAware, IDisposableContainer
    {
        List<IDisposable> disposables = new List<IDisposable>();

        private IWorkItem workitem;

        public IWorkItem Workitem
        {
            get { return workitem; }
            private set { SetProperty(ref workitem, value, nameof(Workitem)); }
        }

        public void SetWorkitem(IWorkItem workItem)
        {
            Workitem = workItem;

            AppSecurityContext.AppPrincipalChanged += HandleAutheticationStateChanged;
        }

        private void HandleAutheticationStateChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(IsAuthenticated));
            OnAutheticationStateChanged();
        }

        protected virtual void OnAutheticationStateChanged()
        {

        }

        protected bool IsAuthenticated
        {
            get => AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated;
        }

        public override void Dispose()
        {
            base.Dispose();

            AppSecurityContext.AppPrincipalChanged -= HandleAutheticationStateChanged;
            disposables.ForEach(d => d.Dispose());
        }

        public T Disposable<T>(T obj) where T : IDisposable
        {
            disposables.Add(obj);
            return obj;
        }

    }
}

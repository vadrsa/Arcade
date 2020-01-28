﻿using Flurl.Http;
using Infrastructure.Api;
using Infrastructure.Security;
using Kernel.Workitems;
using Prism.Commands;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
            private set { Set(ref workitem, value, nameof(Workitem)); }
        }

        protected CancellationTokenSource CanncellationTokenSource;

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                Set(ref isLoading, value, nameof(IsLoading));
                RaisePropertyChanged(nameof(IsLoading));
            }
        }

        public bool _isLoadingFaulted;
        public bool IsLoadingFaulted
        {
            get => _isLoadingFaulted;
            set => Set(ref _isLoadingFaulted, value, nameof(IsLoadingFaulted));
        }

        public string _loadingErrorMessage;
        public string LoadingErrorMessage
        {
            get => _loadingErrorMessage;
            set => Set(ref _loadingErrorMessage, value, nameof(LoadingErrorMessage));
        }

        public DelegateCommand _reloadCommand;
        public DelegateCommand ReloadCommand
        {
            get
            {
                if (_reloadCommand == null)
                    _reloadCommand = new DelegateCommand(Reload);
                return _reloadCommand;
            }
        }

        protected void Load()
        {
            LoadCustom(DoLoad);
        }


        public async Task LoadCustom(Func<CancellationToken, Task> task)
        {
            IsLoading = true;
            IsLoadingFaulted = false;
            try
            {
                CanncellationTokenSource?.Cancel();
                CanncellationTokenSource = new System.Threading.CancellationTokenSource();
                await task(CanncellationTokenSource.Token);
            }
            catch (Exception e)
            {
                IsLoadingFaulted = true;
                var response = await ApiExceptionHandling.GetResponse(e);
                LoadingErrorMessage = response.Message;
                if (response.TraceId != null)
                    LoadingErrorMessage += $"\n Trace Id: {response.TraceId}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task<T> LoadCustom<T>(Func<CancellationToken, Task<T>> task)
        {
            IsLoading = true;
            IsLoadingFaulted = false;
            try
            {
                CanncellationTokenSource?.Cancel();
                CanncellationTokenSource = new System.Threading.CancellationTokenSource();
                return await task(CanncellationTokenSource.Token);
            }
            catch (Exception e)
            {
                IsLoadingFaulted = true;
                var response = await ApiExceptionHandling.GetResponse(e);
                LoadingErrorMessage = response.Message;
                if (response.TraceId != null)
                    LoadingErrorMessage += $"\n Trace Id: {response.TraceId}";
                return default;
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected virtual Task DoLoad(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        protected void Reload()
        {
            IsLoadingFaulted = false;
            Load();
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

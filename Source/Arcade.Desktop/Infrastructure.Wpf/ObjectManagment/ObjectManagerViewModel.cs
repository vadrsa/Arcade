using DevExpress.Xpf.Grid;
using Infrastructure.ChangeTracking;
using Infrastructure.Mvvm;
using Infrastructure.Security;
using Infrastructure.Utility;
using Kernel.Utility;
using Kernel.Workitems;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.ObjectManagment
{
    public abstract class ObjectManagerViewModel<TList, TDetails> : CrudWorkitemManagerViewModel, ISupportsListManipulation, IWorkitemAware, IGridViewModel, IObserver<TDetails>
        where TList : IIdEntityViewModel
        where TDetails : IEditableObject, IIdEntityViewModel
    {
        #region Bindable Properties

        private EditMode editMode = EditMode.Default;
        public EditMode EditMode
        {
            get
            {
                return editMode;
            }
            set
            {
                SetProperty(ref editMode, value, nameof(EditMode), UpdateCrudCommands);
                RaisePropertyChanged(nameof(IsDirty));
            }
        }

        public virtual bool IsDirty
        {
            get
            {
                return EditMode != EditMode.Default;
            }
        }

        private bool CanRefreshList()
        {
            return IsListEnabled && !IsListLoading;
        }

        protected async virtual Task RefreshList()
        {
            await RefreshItems(CurrentItemDetails?.ID);
        }

        private bool isListEnabled = true;
        public bool IsListEnabled
        {
            get { return isListEnabled && !IsListLoading; }
            set { SetProperty(ref isListEnabled, value, nameof(IsListEnabled)); }
        }

        private bool isDetailsEnabled = true;
        public bool IsDetailsEnabled
        {
            get { return isDetailsEnabled && !IsObjectLoading; }
            set { SetProperty(ref isDetailsEnabled, value, nameof(IsDetailsEnabled)); }
        }

        private bool isListLoading;
        public bool IsListLoading
        {
            get { return isListLoading; }
            set
            {
                SetProperty(ref isListLoading, value, nameof(IsListLoading));
                RaisePropertyChanged(nameof(IsListEnabled));
            }
        }

        private bool isReadOnly = true;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                SetProperty(ref isReadOnly, value, nameof(IsReadOnly), OnReadOnlyChanged);
            }
        }

        private bool isobjectLoading;
        public bool IsObjectLoading
        {
            get { return isobjectLoading; }
            set
            {
                SetProperty(ref isobjectLoading, value, nameof(IsObjectLoading), OnObjectLoadingChanged);
                RaisePropertyChanged(nameof(IsDetailsEnabled));
            }
        }

        private TList currentItem;
        public TList CurrentItem
        {
            get { return currentItem; }
            set
            {
                SetProperty(ref currentItem, value, nameof(CurrentItem));

                WhenCurrentItemChanged.OnNext(value?.ID);
            }
        }

        private TDetails currentItemDetails;
        public TDetails CurrentItemDetails
        {
            get { return currentItemDetails; }
            set
            {
                SetProperty(ref currentItemDetails, value, nameof(CurrentItemDetails));
                UpdateCrudCommands();
            }
        }

        #endregion

        #region Commands

        private SecureAsyncCommand refreshListCommand;
        public SecureAsyncCommand RefreshListCommand
        {
            get
            {
                if (refreshListCommand == null)
                    refreshListCommand = Disposable(new SecureAsyncCommand(RefreshList, CanRefreshList));
                return refreshListCommand;
            }
        }

        private SecureCommand searchCommand;
        public SecureCommand SearchCommand =>
            searchCommand ?? (searchCommand = Disposable(new SecureCommand(Search, CanExecuteSearchCommand)));

        private SecureCommand collapseAllCommand;
        public SecureCommand CollapseAllCommand =>
            collapseAllCommand ?? (collapseAllCommand = Disposable(new SecureCommand(ExecuteCollapseAllCommand, CanExecuteCollapseAllCommand)));


        private SecureCommand expandAllCommand;
        public SecureCommand ExpandAllCommand =>
            expandAllCommand ?? (expandAllCommand = Disposable(new SecureCommand(ExecuteExpandAllCommand, CanExecuteExpandAllCommand)));

        #endregion

        #region Command Implementation

        protected override void Add()
        {
            base.Add();
            IsListEnabled = false;
            IsReadOnly = false;
            memento = CurrentItemDetails;
            CurrentItemDetails = CreateEmptyDetails();
            EditMode = EditMode.Add;
        }

        protected override bool CanAdd()
        {
            return base.CanAdd() && editMode == EditMode.Default && IsListEnabled;
        }

        protected override void AddCopy()
        {
            base.AddCopy();
            IsListEnabled = false;
            IsReadOnly = false;
            memento = CurrentItemDetails;
            CurrentItemDetails = CreateCopyDetails();
            EditMode = EditMode.Add;
        }

        protected override bool CanAddCopy()
        {
            return base.CanAddCopy() && editMode == EditMode.Default && IsListEnabled && CurrentItemDetails != null;
        }

        protected override void Edit()
        {
            base.Edit();
            IsListEnabled = false;
            EditMode = EditMode.Edit;
            IsReadOnly = false;

            CurrentItemDetails.BeginEdit();
        }

        protected override bool CanEdit()
        {
            return base.CanEdit() && editMode == EditMode.Default && CurrentItemDetails != null && IsListEnabled;
        }

        protected override bool CanSave()
        {
            return base.CanSave() && editMode != EditMode.Default && CurrentItemDetails != null;
        }

        protected override bool CanDelete()
        {
            return base.CanDelete() && editMode == EditMode.Default && CurrentItem != null && IsListEnabled;
        }

        protected override void CancelEditing()
        {
            base.CancelEditing();
            IsListEnabled = true;
            if (EditMode == EditMode.Add)
            {
                CurrentItemDetails = memento;
                memento = default(TDetails);
            }
            else
            {
                CurrentItemDetails?.CancelEdit();
            }
            IsReadOnly = true;
            EditMode = EditMode.Default;
        }


        protected void EndEditing()
        {
            IsListEnabled = true;
            if (EditMode == EditMode.Add)
            {
                memento = default(TDetails);
            }
            else
            {
                CurrentItemDetails.EndEdit();
            }
            IsReadOnly = true;
            EditMode = EditMode.Default;
        }

        protected override async Task Save()
        {
            try
            {
                if (!Validate())
                {
                    UIManager.Error("Correct all errors before saving.");
                    return;
                }
                IsObjectLoading = true;
                CommitSuccessAction res = await GetCommitObservable(EditMode);
                EndEditing();
                res.Invoke(this);
                IsObjectLoading = false;
            }
            catch (Exception ex)
            {
                ApiHelper.HandleApiException(ex);
                IsObjectLoading = false;
            }
        }

        protected override async Task Delete()
        {
            if (UIManager.AskForConfirmation(String.Format("Do you really want to remove {0}", CurrentItem.ToString())))
            {

                int id = ((IIdEntityViewModel)CurrentItem).ID;
                IsListLoading = true;
                try
                {
                    CommitSuccessAction res = await GetRemoveObservable();
                    res.Invoke(this);
                    IsListLoading = false;
                }
                catch (Exception e)
                {
                    ApiHelper.HandleApiException(e);
                    IsListLoading = false;
                }
            }

        }

        protected override bool CanCancelEditing()
        {
            return base.CanCancelEditing() && editMode != EditMode.Default && !SaveCommand.IsExecuting;
        }

        void Search()
        {
            Grid.View.ShowSearchPanel(true);
        }

        bool CanExecuteSearchCommand()
        {
            return Grid != null;
        }

        void ExecuteCollapseAllCommand()
        {
            for (int i = 0; i < Grid.VisibleRowCount; i++)
            {
                var handle = Grid.GetRowHandleByVisibleIndex(i);
                Grid.CollapseMasterRow(handle);
            }
            Grid.CollapseAllGroups();
        }

        protected virtual bool CanExecuteCollapseAllCommand()
        {
            return Grid != null;
        }

        void ExecuteExpandAllCommand()
        {
            for (int i = 0; i < Grid.VisibleRowCount; i++)
            {
                var handle = Grid.GetRowHandleByVisibleIndex(i);
                Grid.ExpandMasterRow(handle);
            }
            Grid.ExpandAllGroups();
        }

        bool CanExecuteExpandAllCommand()
        {
            return Grid != null;
        }


        #endregion

        #region Properties

        public GridControl Grid { get; set; }

        protected IWorkItem WorkItem { get; private set; }


        #endregion

        public ObjectManagerViewModel()
        {
            WhenCurrentItemChanged = new Subject<int?>();
            WhenCurrentItemChanged.Throttle(TimeSpan.FromMilliseconds(200));
            Disposable(WhenCurrentItemChanged.Subscribe(HandleCurrentItemChanged));
            AppSecurityContext.AppPrincipalChanged += HandleAutheticationStateChanged;
        }

        #region Private
        private TDetails memento;

        private Subject<int?> WhenCurrentItemChanged;
        private CancellationTokenSource objectLoadCancellationSource;

        private void HandleAutheticationStateChanged(object sender, EventArgs args)
        {
            RaisePropertyChanged(nameof(IsDetailsEnabled));
            RaisePropertyChanged(nameof(IsListEnabled));
            UpdateCrudCommands();
        }

        private void HandleCurrentItemChanged(int? id)
        {
            CurrentItemDetails = default(TDetails);

            if (id.HasValue)
            {
                IsObjectLoading = true;
                objectLoadCancellationSource?.Cancel();
                objectLoadCancellationSource = new CancellationTokenSource();
                IObservable<TDetails> obs = Observable.FromAsync(() => ObjectManagementService.GetForUploadByID(id.Value, objectLoadCancellationSource.Token));
                obs.SubscribeOnDispatcher().Subscribe(this);
            }
            else
                CurrentItemDetails = default(TDetails);
        }

        private IObservable<CommitSuccessAction> GetCommitObservable(EditMode mode)
        {
            if (EditMode == EditMode.Add)
                return
                    Observable.Select(
                        Observable.FromAsync(() => ObjectManagementService.Add(CurrentItemDetails)),
                        id => new CommitSuccessAction((o) =>
                            o.RefreshItems(id)
                        )
                    );
            else
                return
                    Observable.Select(
                        Observable.FromAsync(() => ObjectManagementService.Update(CurrentItemDetails)),
                        _ => new CommitSuccessAction((o) =>
                            o.RefreshItems(CurrentItemDetails.ID)
                        )
                    );
        }

        private IObservable<CommitSuccessAction> GetRemoveObservable()
        {
            return
                Observable.Select(
                    Observable.FromAsync(() => ObjectManagementService.Remove(CurrentItem.ID)),
                    _ =>
                        new CommitSuccessAction((o) => o.RemoveFromList(CurrentItem.ID))
                    );
        }

        #endregion

        #region Abstract Interface
        public abstract ObservableCollection<TList> ListItems { get; set; }
        protected abstract TDetails CreateEmptyDetails();
        protected abstract TDetails CreateCopyDetails();
        protected abstract IObjectManagementService<TList, TDetails> ObjectManagementService { get; }
        protected virtual void OnReadOnlyChanged() { }
        protected virtual void OnObjectLoadingChanged() { }
        #endregion

        #region Protected

        protected virtual async Task LoadList()
        {
            try
            {
                ListItems = new ObservableCollection<TList>(await ObjectManagementService.GetAll());

            }
            catch (Exception ex)
            {
                //ExceptionHandler.HandleError(ex);
            }
        }

        protected virtual bool Validate()
        {
            IDataErrorInfo dataErrorInfo = CurrentItemDetails as IDataErrorInfo;
            return !dataErrorInfo.HasErrors();
        }

        protected virtual async void Initialize()
        {

            IsListLoading = true;
            IsObjectLoading = true;

            await LoadList();

            IsListLoading = false;
            IsObjectLoading = false;
        }

        #endregion

        #region ISupportsListManipulation Implementation


        public void RemoveFromList(int id)
        {
            var prod = ListItems.FirstOrDefault(p => p.ID == id);
            if (prod != null)
                Application.Current.Dispatcher.InvokeIfNeeded(() => ListItems.Remove(prod));

        }

        public async Task RefreshItems(int? focuseID = null)
        {
            Initialize();

            if (focuseID.HasValue)
            {
                var prod = ListItems.FirstOrDefault(p => p.ID == focuseID.Value);
                if (prod != null)
                {
                    await Task.Delay(100);
                    CurrentItem = prod;
                }
            }
        }


        #endregion

        #region IObserver<TDetails> Implementation

        public void OnNext(TDetails value)
        {
            CurrentItemDetails = value;
            IsObjectLoading = false;
        }

        public void OnError(Exception e)
        {
            ApiHelper.HandleApiException(e, "Failed to load object", () =>
            {
                IsObjectLoading = false;
            });
        }

        public void OnCompleted()
        {
        }
        #endregion

        public override void Dispose()
        {
            base.Dispose();
            WorkItem = null;
            Grid = null;

            AppSecurityContext.AppPrincipalChanged -= HandleAutheticationStateChanged;
        }

    }
}

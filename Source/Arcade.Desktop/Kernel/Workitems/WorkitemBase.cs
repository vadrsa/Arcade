using AsyncAwaitBestPractices.MVVM;
using Kernel.Configuration;
using Kernel.Managers;
using Kernel.Prism;
using Kernel.Storage;
using Kernel.Utility;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kernel.Workitems
{

    public abstract class WorkitemBase : IWorkItem, IDisposableContainer
    {
        #region Private
        private IViewContainer viewContainer;
        private IContainerExtension _container;
        private ISubject<WorkitemEventArgs> communicationChannel;
        IOptionConfiguration configuration;
        private bool _isFocused;
        private Dictionary<string, Func<object>> Resources;
        private List<IDisposable> Disposables;

        private string LocalStorageKey
        {
            get
            {
                return WorkItemName.Replace(" ", "");
            }
        }


        /// <summary>
        /// Handle the workitem focus event
        /// </summary>
        private void HandleFocused()
        {
            // If window is defined(Modal) focus the window
            if (Window != null)
                Window.Focus();
            // continue event execution
            OnFocused();
        }

        /// <summary>
        /// Handle the workitem unfocus event
        /// </summary>
        private void HandleLostFocus()
        {
            // continue event execution
            OnLostFocus();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command to close the workitem
        /// </summary>
        private ICommand _closeCommand;
        protected ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new AsyncCommand(Close);
                return _closeCommand;
            }
        }

        #endregion

        #region Properties


        public IStorage LocalStorage { get; private set; }

        protected Project Project { get; private set; }

        protected IRegionManagerExtension RegionManager { get; private set; }

        protected IUIManager UIManager { get; private set; }

        protected ITaskManager TaskManager { get; private set; }

        /// <summary>
        /// The Communication Channel that allows the workitem to talk to its creator
        /// </summary>
        protected IObserver<WorkitemEventArgs> Communication => communicationChannel;

        /// <summary>
        /// The view container that allows to register workitem views
        /// </summary>
        internal IViewContainer ViewContainer => viewContainer;

        /// <summary>
        /// Shows if workitme has unsaved changes.
        /// If overriden you must take care of calling OnIsDirtyChanged
        /// every time IsDirty changes
        /// </summary>
        public virtual bool IsDirty => false;

        /// <summary>
        /// Specifies if the workitem is opened
        /// </summary>
        public bool IsOpen { get; set; } = false;

        /// <summary>
        /// If True the workitem will remian focused after a child workitem is opened
        /// </summary>
        public virtual bool SupportsMultiFocus { get; } = false;

        /// <summary>
        /// The name of the Workitem, for example Product Manager
        /// </summary>
        public abstract string WorkItemName { get; }

        /// <summary>
        /// Unique ID of the workitem
        /// </summary>
        public string WorkItemID
        {
            get;
            private set;
        }

        /// <summary>
        /// The Window workitem is displayed in
        /// Window is set only on workitems opened in modal mode
        /// </summary>
        public IModalWindow Window { get; set; }

        /// <summary>
        /// IsDirty changed event
        /// </summary>
        public virtual event EventHandler<EventArgs> IsDirtyChanged;

        /// <summary>
        /// Workitems Parent in the Workitem Tree
        /// </summary>
        public IWorkItem Parent { get; set; }

        /// <summary>
        /// Configuration object of the workitem
        /// </summary>
        public IOptionConfiguration Configuration => configuration;

        /// <summary>
        /// Is the workitem opened in modal mode
        /// </summary>
        public bool IsModal { get; private set; }

        /// <summary>
        /// Current ContextService
        /// </summary>
        protected IContextService CurrentContextService { get; set; }

        /// <summary>
        /// The prism container extended
        /// </summary>
        public IContainerExtension Container
        {
            get
            {
                return _container;
            }
        }

        #endregion

        #region Constructor

        public WorkitemBase(IContainerExtension container)
        {
            _container = container;
            CurrentContextService = container.Resolve<IContextService>();
            Resources = new Dictionary<string, Func<object>>();
            configuration = new OptionConfiguration();
            RegionManager = container.Resolve<IRegionManagerExtension>();
            UIManager = container.Resolve<IUIManager>();
            TaskManager = container.Resolve<ITaskManager>();
            Project = container.Resolve<Project>();
            Configuration.Configure(new ModalOptions());
            LocalStorage = new LocalStorage(LocalStorageKey, Project);
        }

        #endregion

        #region Public/Protected Methods

        /// <summary>
        /// Create and return the view container that should be used tyo register workitem views
        /// </summary>
        /// <returns>The newly created view container</returns>
        protected virtual IViewContainer CreateViewContainer()
        {
            if (IsModal)
                return new ModalWorkitemViewContainer(this, RegionManager, Container);
            else
                return new WorkitemViewContainer(this, RegionManager, Container);
        }

        /// <summary>
        /// Create and return the command container that should be used tyo register workitem commands
        /// </summary>
        /// <returns>The newly created command container</returns>
        protected virtual ICommandContainer CreateCommandContainer()
        {
            return new WorkitemCommandContainer(this);
        }

        /// <summary>
        /// Executed after the workitem run method
        /// </summary>
        protected virtual void AfterWorkitemRun() { }

        /// <summary>
        /// Executed before the workitem run method
        /// </summary>
        protected virtual void BeforeWorkitemRun() { }

        /// <summary>
        /// Close the workitem
        /// </summary>
        /// <returns></returns>
        public Task<bool> Close()
        {
            return CurrentContextService.CloseWorkitem(this);
        }

        /// <summary>
        /// Lifecycle hook to add workitem configuration
        /// </summary>
        public virtual void Configure()
        {
        }

        /// <summary>
        /// Launch the workitem
        /// </summary>
        public async Task<IObservable<WorkitemEventArgs>> Run()
        {
            // WorkitemID must be set before any operation takes place
            WorkItemID = Guid.NewGuid().ToString();
            Disposables = new List<IDisposable>();

            var channel = new WorkitemCommunicationChannel();
            communicationChannel = channel;

            BeforeWorkitemRun();

            IsOpen = true;
            await Create().ConfigureAwait(false);

            viewContainer = CreateViewContainer();

            Disposable(viewContainer);
            RegisterViews(viewContainer);

            ICommandContainer container = CreateCommandContainer();
            Disposable(container);
            RegisterCommands(container);

            AfterWorkitemRun();

            return communicationChannel;
        }

        /// <summary>
        /// Launch the workitem in modal mode
        /// </summary>
        public Task<IObservable<WorkitemEventArgs>> RunModal()
        {
            IsModal = true;
            return Run();
        }

        /// <summary>
        /// Change the workitem state to modal at runtime
        /// </summary>
        public void ChangeToModalState()
        {
            IsModal = true;
            var oldContainer = viewContainer;
            viewContainer = CreateViewContainer();
            viewContainer.ImportFrom(oldContainer);
        }

        /// <summary>
        /// Get a resource if it exists
        /// </summary>
        /// <param name="name">The name of the resource</param>
        /// <returns>The resource if found null otherwise</returns>
        public object RequestResource(string name)
        {
            if (Resources.ContainsKey(name))
                return Resources[name]?.Invoke();
            return null;
        }

        /// <summary>
        /// Notifies that IsDirty has changed.
        /// Should be called every time IsDirty chnages if ovveriding IsDirty
        /// </summary>
        protected void OnIsDirtyChanged()
        {
            IsDirtyChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Registeres an IDisposable obejct as Disposable
        /// note that all Disposables will be disposed during cleanup
        /// </summary>
        /// <param name="disposable"></param>
        public T Disposable<T>(T disposable)
            where T : IDisposable
        {
            Disposables.Add(disposable);
            return disposable;
        }

        protected bool HasResource(string name)
        {
            return Resources.ContainsKey(name);
        }

        /// <summary>
        /// Registeres a new resource
        /// </summary>
        /// <param name="name"></param>
        /// <param name="getter"></param>
        protected void Resource(string name, Func<object> getter)
        {
            Resources.Add(name, getter);
        }

        /// <summary>
        /// Register all commands the workitem wants to expose
        /// This method is part of the launch process and shouldn't be invoked
        /// </summary>
        /// <param name="container"></param>
        protected virtual void RegisterCommands(ICommandContainer container)
        {
        }

        /// <summary>
        /// Regitster views
        /// This method is part of the launch process and shouldn't be invoked
        /// </summary>
        /// <param name="container"></param>
        protected virtual void RegisterViews(IViewContainer container)
        {
        }

        /// <summary>
        /// Lifecicle hook to create anything async to be used by the workitem
        /// </summary>
        /// <returns></returns>
        protected virtual Task Create()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when workitem loses focus
        /// </summary>
        protected virtual void OnLostFocus()
        {
            if (!IsModal)
                RegionManager.DeactivateWorkitem(this);
        }

        /// <summary>
        /// Called when workitem is focused
        /// </summary>
        protected virtual void OnFocused()
        {
            RegionManager.ActivateWorkitem(this);
        }

        public override bool Equals(object obj)
        {
            IWorkItem workItem = obj as IWorkItem;
            if (workItem == null)
                return false;

            return workItem.WorkItemID == this.WorkItemID;
        }

        public override int GetHashCode()
        {
            var hashCode = 310520135;
            hashCode = hashCode * -1521134295 + IsDirty.GetHashCode();
            hashCode = hashCode * -1521134295 + IsOpen.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WorkItemName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WorkItemID);
            hashCode = hashCode * -1521134295 + ((ISupportsFocus)this).IsFocused.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IWorkItem>.Default.GetHashCode(Parent);
            hashCode = hashCode * -1521134295 + IsModal.GetHashCode();
            return hashCode;
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Dispose of the workitem
        /// </summary>
        public virtual void Dispose()
        {
            Disposables.ForEach(d => d?.Dispose());
            DispisableHelper.DisposeEvent(IsDirtyChanged);
            communicationChannel = null;
        }

        #endregion

        #region ISupportFocus Implementation

        /// <summary>
        /// Set the workitem focus
        /// </summary>
        bool ISupportsFocus.IsFocused
        {
            get
            {
                return _isFocused;
            }
            set
            {
                if (value != _isFocused || IsModal)
                {
                    _isFocused = value;
                    if (value == true)
                        HandleFocused();
                    else
                        HandleLostFocus();
                }
            }
        }

        #endregion
    }
}

using AsyncAwaitBestPractices.MVVM;
using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel;
using Kernel.Workitems;
using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.ObjectManagement
{
    public abstract class ObjectManagerDetailsWorkitem<TView, TList, TDetails, K> : WorkitemWpfBase, ISupportsInitialization<ObjectManagerDetailsInitializer<TDetails>>
        where TView : FrameworkElement
    {

        private ObjectManagerDetailsInitializer<TDetails> _data;
        private ObjectManagerDetailsViewModel<TDetails> viewModel;
        protected ObjectManagerDetailsInitializer<TDetails> Data => _data;
        protected ObjectManagerDetailsViewModel<TDetails> ViewModel => viewModel;
        protected abstract IObjectManagementService<TList, TDetails, K> ObjectManagementService { get; }

        public ObjectManagerDetailsWorkitem(IContainerExtension container) : base(container)
        {
        }

        protected virtual Type ViewType
        {
            get => typeof(TView);
        }

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(Constants.Commands.SaveObject, SaveCommand);
        }

        public void Initialize(ObjectManagerDetailsInitializer<TDetails> data)
        {
            _data = data;

        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            var view = container.Register<FrameworkElement>((FrameworkElement)Container.Resolve(ViewType), KnownRegions.Content);
            viewModel = (ObjectManagerDetailsViewModel<TDetails>)view.DataContext;
            viewModel.Details = _data.Details;
            viewModel.IsAdding = _data.IsAdding;
            IsAdding = _data.IsAdding;
        }

        public bool IsAdding { get; set; }

        private AsyncCommand _saveCommand;

        public AsyncCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new AsyncCommand(Save);
                return _saveCommand;
            }
        }

        private async Task Save()
        {
            await viewModel.LoadCustom(DoSave);
        }

        protected virtual async Task DoSave(CancellationToken token)
        {
            if (IsAdding)
            {
                await DoAdd(token);
                Communication.OnNext(new WorkitemEventArgs(this, true));
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                await DoUpdate(token);
                Communication.OnNext(new WorkitemEventArgs(this, true));
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        protected virtual async Task DoAdd(CancellationToken token)
        {
            await ObjectManagementService.Add(viewModel.Details, token);
        }

        protected virtual async Task DoUpdate(CancellationToken token)
        {
            await ObjectManagementService.Update(viewModel.Details, token);
        }
    }
}

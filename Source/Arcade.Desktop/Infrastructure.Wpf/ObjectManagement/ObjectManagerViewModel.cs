using Infrastructure.Api;
using Infrastructure.Mvvm;
using Prism.Ioc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.ObjectManagement
{
    public abstract class ObjectManagerViewModel<TList, TDetails, K> : WorkitemViewModel<IObjectManagerWorkitem<TList, TDetails>>, IObserver<bool>
        where TList : IIdEntityViewModel<K>
        where TDetails : IIdEntityViewModel<K>
    {

        protected IContainerProvider ContainerProvider => Workitem.Container;
        protected abstract IObjectManagementService<TList, TDetails, K> ObjectManagementService { get; }


        private FilteredObservableCollection<TList> items;
        public FilteredObservableCollection<TList> List
        {
            get => items;
            set => Set(ref items, value, nameof(List));
        }

        protected override void OnWorkitemSet()
        {
            base.OnWorkitemSet();
            Load();
        }

        protected override async Task DoLoad(CancellationToken token)
        {
            List = new FilteredObservableCollection<TList>(await ObjectManagementService.GetAll(token));
        }

        private AsyncCommand<K> _editCommand;

        public AsyncCommand<K> EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new AsyncCommand<K>(Edit, CanEdit);
                return _editCommand;
            }
        }

        private bool CanEdit(K id)
        {
            return !DeleteCommand.IsExecuting(id);
        }

        private async Task Edit(K id)
        {
            var item = await LoadCustom<TDetails>((t) => GetForUpload(id, t));
            if (item == null) return;

            (await Workitem.RunAddEdit(item, false)).Subscribe(this);
        }

        private async Task<TDetails> GetForUpload(K id, CancellationToken token)
        {
            return await ObjectManagementService.GetForUploadByID(id, token);
        }


        private AsyncCommand<K> _deleteCommand;

        public AsyncCommand<K> DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new AsyncCommand<K>(DoDelete);
                return _deleteCommand;
            }
        }

        private async Task DoDelete(K id)
        {
            EditCommand.RaiseCanExecuteChanged();
            try
            {
                await ObjectManagementService.Remove(id);
                var item = List.FirstOrDefault(g => g.Id.Equals(id));
                if (item != null)
                    List.Remove(item);
            }
            catch (Exception e)
            {
                var response = await ApiExceptionHandling.GetResponse(e);
                UIManager.Error(response.Message);
            }
        }

        private AsyncCommand _addCommand;

        public AsyncCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new AsyncCommand(Add);
                return _addCommand;
            }
        }

        private async Task Add()
        {
            (await Workitem.RunAddEdit(Activator.CreateInstance<TDetails>(), true)).Subscribe(this);

        }

        public void OnNext(bool value)
        {
            if (value)
                Load();
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }
    }
}

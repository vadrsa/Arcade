using Arcade.ViewModels;
using GamesModule.Services;
using Infrastructure.Api;
using Infrastructure.Mvvm;
using Infrastructure.Utils;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GamesModule.Workitems.GameManager.Views
{
    public class GameManagerViewModel : WorkitemViewModel, IObserver<bool>
    {

        public GameManagerViewModel()
        {
            Load();
        }

        private FilteredObservableCollection<GameViewModel> _games;
        public FilteredObservableCollection<GameViewModel> Games
        {
            get => _games;
            set => Set(ref _games, value, nameof(Games));
        }

        protected override async Task DoLoad(CancellationToken token)
        {
            var games = await new GamesService().GetAll(token);
            Games = Mapper.Map<FilteredObservableCollection<GameViewModel>>(games);
        }


        private AsyncCommand<string> _editGameCommand;

        public AsyncCommand<string> EditGameCommand
        {
            get
            {
                if (_editGameCommand == null)
                    _editGameCommand = new AsyncCommand<string>(EditGame, CanEditGame);
                return _editGameCommand;
            }
        }

        private bool CanEditGame(string id)
        {
            return !DeleteGameCommand.IsExecuting(id);
        }

        private async Task EditGame(string id)
        {
            var game = await LoadCustom<GameUploadViewModel>((t) => GetForUpload(id, t));
            if (game == null) return;

            (await ((GameManagerWorkitem)Workitem).RunAddEdit(game, false)).Subscribe(this);
        }

        private async Task<GameUploadViewModel> GetForUpload(string id, CancellationToken token)
        {
            var games = await new GamesService().GetForUpload(id, token);
            return Mapper.Map<GameUploadViewModel>(games);
        }


        private AsyncCommand<string> _deleteGameCommand;

        public AsyncCommand<string> DeleteGameCommand
        {
            get
            {
                if (_deleteGameCommand == null)
                    _deleteGameCommand = new AsyncCommand<string>(DoDeleteGame);
                return _deleteGameCommand;
            }
        }

        private async Task DoDeleteGame(string id)
        {
            EditGameCommand.RaiseCanExecuteChanged();
            try
            {
                await new GamesService().Remove(id);
                var game = Games.FirstOrDefault(g => g.Id == id);
                if (game != null)
                    Games.Remove(game);
            }
            catch (Exception e)
            {
                var response = await ApiExceptionHandling.GetResponse(e);
                UIManager.Error(response.Message);
            }
        }

        private AsyncCommand _addGameCommand;

        public AsyncCommand AddGameCommand
        {
            get
            {
                if (_addGameCommand == null)
                    _addGameCommand = new AsyncCommand(AddGame);
                return _addGameCommand;
            }
        }

        private async Task AddGame()
        {
            (await ((GameManagerWorkitem)Workitem).RunAddEdit(new GameUploadViewModel(), true)).Subscribe(this);

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

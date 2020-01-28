using Arcade.ViewModels;
using GamesModule.Services;
using Infrastructure.Mvvm;
using Prism.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace GamesModule.Workitems.GameDetails.Views
{
	public class GameDetailsDisplayViewModel : WorkitemViewModel
	{
		private string id;

		private GameDetailsViewModel _details;

		public GameDetailsViewModel Details
		{
			get => _details;
			set => Set(ref _details, value, nameof(Details));
		}

		private DelegateCommand _goBackCommand;
		public DelegateCommand GoBackCommand
		{
			get
			{
				if (_goBackCommand == null)
					_goBackCommand = new DelegateCommand(GoBack);
				return _goBackCommand;
			}
		}

		private void GoBack()
		{
			CanncellationTokenSource?.Cancel();
			Workitem.Close();
		}

		protected override async Task DoLoad(CancellationToken token)
		{
			var game = await new GamesService().GetById(id, token);
			Details = Mapper.Map<GameDetailsViewModel>(game);
		}

		public void LoadGame(string id)
		{
			this.id = id;
			Load();
		}
	}
}

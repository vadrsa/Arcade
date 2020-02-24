using Arcade.ViewModels;
using GamesModule.Services;
using Infrastructure.ObjectManagement;
using Newtonsoft.Json.Linq;
using SharedEntities;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StaffModule.Workitems.SessionManagerDetails.Views
{
    public class SessionDetailsViewModel : ObjectManagerDetailsViewModel<SessionUploadViewModel>
    {

        private ObservableCollection<GameViewModel> _games;

        public ObservableCollection<GameViewModel> Games
        {
            get { return _games; }
            set { Set(ref _games, value, nameof(Games)); }
        }

        public SessionDetailsViewModel()
        {
            Load();
        }

        protected override async Task DoLoad(CancellationToken token)
        {
            Games = Mapper.Map<ObservableCollection<GameViewModel>>(await new GamesService().GetAll(token));
        }
    }
}

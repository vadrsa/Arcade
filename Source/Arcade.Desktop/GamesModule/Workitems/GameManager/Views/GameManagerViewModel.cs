using Arcade.ViewModels;
using GamesModule.Services;
using Infrastructure.ObjectManagement;
using Prism.Ioc;

namespace GamesModule.Workitems.GameManager.Views
{
    public class GameManagerViewModel : ObjectManagerViewModel<GameViewModel, GameUploadViewModel, string>
    {

        protected override IObjectManagementService<GameViewModel, GameUploadViewModel, string> ObjectManagementService => ContainerProvider.Resolve<GamesOMService>();
    }
}

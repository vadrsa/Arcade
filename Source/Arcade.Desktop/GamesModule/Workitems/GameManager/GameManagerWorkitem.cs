using Arcade.ViewModels;
using GamesModule.Workitems.AddEditGame;
using GamesModule.Workitems.GameManager.Views;
using Infrastructure.Constants;
using Infrastructure.ObjectManagement;
using Infrastructure.Workitems;
using Kernel.Workitems;
using Prism.Ioc;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace GamesModule.Workitems.GameManager
{
    class GameManagerWorkitem : ObjectManagerWorkitem<GameManagerView, GameViewModel, GameUploadViewModel, AddEditGameWorkitem>
    {
        public GameManagerWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Game Manager";

    }
}

using Arcade.ViewModels;
using GamesModule.Workitems.AddEditGame;
using GamesModule.Workitems.GameManager.Views;
using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel.Workitems;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesModule.Workitems.GameManager
{
    class GameManagerWorkitem : WorkitemWpfBase, IObserver<WorkitemEventArgs>
    {
        public GameManagerWorkitem(IContainerExtension container) : base(container)
        {
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new GameManagerView(), KnownRegions.Content);
        }

        public override string WorkItemName => "Game Manager";

        public async Task<IObservable<bool>> RunAddEdit(GameUploadViewModel game, bool isAdding)
        {
            return (await CurrentContextService.LaunchModalWorkItem<AddEditGameWorkitem>(new AddEditGameInitializer { Game = game, IsAdding = isAdding }, this)).Where(w => w.Data is bool).Select(w => (bool)w.Data);
        }

        public void OnNext(WorkitemEventArgs value)
        {
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}

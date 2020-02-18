using GamesModule.Workitems.AddEditGame;
using GamesModule.Workitems.GameManager;
using GamesModule.Workitems.GamesDisplay;
using Infrastructure.Prism;
using Prism.Ioc;

namespace Modules
{
    public class GamesModule : Module
    {
        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            // hadck to create referance
            new Arcade.CustomControls.FaultedPage();
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.Register<AddEditGameWorkitem>();
            containerRegistry.Register<GamesDisplayWorkitem>();
            containerRegistry.Register<GameManagerWorkitem>();
        }


    }
}

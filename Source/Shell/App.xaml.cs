using Arcade;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.NavBar;
using DevExpress.Xpf.Ribbon;
using Infrastructure.Constants;
using Infrastructure.Prism;
using Infrastructure.Utility;
using Kernel;
using Kernel.Prism;
using Kernel.Workitems;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Linq;
using System.Windows;

namespace Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App() : base(new ArcadeProject())
        {
        }

        protected override Window CreateShell()
        {
            return new MainWindow();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            CommandManager.RegisterCommand(KnownCommands.CloseAllTabs, CloseAllTabs);
            CommandManager.RegisterCommand(KnownCommands.Exit, Application.Current.MainWindow.Close);
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<Modules.SecurityModule>();
            moduleCatalog.AddModule<Modules.GamesModule>();
        }

        protected override void ConfigureRegionTransformations(RegionTransformationCollection collection)
        {
            base.ConfigureRegionTransformations(collection);
            collection.Register<DXTabControlRegionTransformation>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings mappings)
        {
            base.ConfigureRegionAdapterMappings(mappings);
            var factory = Container.Resolve<IRegionBehaviorFactory>();

            mappings.RegisterMapping(typeof(DXTabControl),
                DevExpress.Xpf.Prism.AdapterFactory.Make<RegionAdapterBase<DXTabControl>>(factory));
            mappings.RegisterMapping(typeof(NavBarControl),
                DevExpress.Xpf.Prism.AdapterFactory.Make<RegionAdapterBase<NavBarControl>>(factory));

        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            PrismHelper.SetCustomViewTypeToViewModelTypeResolver();
        }

        private void CloseAllTabs()
        {
            IContextService contextService = Container.Resolve<IContextService>();
            contextService.CloseAllWorkitems();
        }
    }
}

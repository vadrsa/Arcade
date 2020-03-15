using Arcade.Configuration;
using Arcade.Views;
using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Prism;
using Infrastructure.Utility;
using Kernel;
using Kernel.Configuration;
using Kernel.Managers;
using Kernel.Prism;
using Kernel.Workitems;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Arcade
{
    public abstract class ArcadeWpfApplication : PrismApplication
    {
        public ArcadeWpfApplication(Project project) : base(project)
        {
        }

        protected override Window CreateShell()
        {
            return project.GetOption<RegionOptions>().GetWindow();
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
            moduleCatalog.AddModule<Modules.StaffModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings mappings)
        {
            base.ConfigureRegionAdapterMappings(mappings);
            var factory = Container.Resolve<IRegionBehaviorFactory>();
            mappings.RegisterMapping(typeof(WrapPanel), new PanelHostRegionAdapter<WrapPanel>(factory));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(Assembly.GetAssembly(typeof(GamesProfile))));
            configuration.AssertConfigurationIsValid();
            containerRegistry.RegisterInstance<IMapper>(configuration.CreateMapper());
            containerRegistry.RegisterInstance<IUIManager>(new UIManager());
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

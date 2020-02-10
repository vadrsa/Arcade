using Arcade.Management;
using Arcade.Management.Configuration;
using AutoMapper;
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
using System.Reflection;
using System.Windows;

namespace Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App() : base(new ArcadeManagemantApplication())
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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(Assembly.GetAssembly(typeof(EmployeeProfile))));
            configuration.AssertConfigurationIsValid();
            containerRegistry.RegisterInstance<IMapper>(configuration.CreateMapper());
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<Modules.SecurityModule>();
            moduleCatalog.AddModule<Modules.StaffModule>();
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

            mappings.RegisterMapping(typeof(RibbonControl),
                new RibbonControlRegionAdapter(factory));
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

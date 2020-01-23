using Kernel.Configuration;
using Kernel.Prism;
using Kernel.Workitems;
using Prism.Ioc;
using Prism.Modularity;

namespace Infrastructure.Prism
{
    public abstract class Module : IModule
    {
        private IRegionManagerExtension _regionManager;
        private Project _project;
        private IContextService _contextService;

        protected IContextService CurrentContextService => _contextService;
        protected IRegionManagerExtension RegionManager => _regionManager;
        protected Project Project => _project;

        public virtual void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager = containerProvider.Resolve<IRegionManagerExtension>();
            _project = containerProvider.Resolve<Project>();
            _contextService = containerProvider.Resolve<IContextService>();
        }


        public virtual void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}

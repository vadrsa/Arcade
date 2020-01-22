using Kernel.Prism;
using Kernel.Workitems;
using System;

namespace Kernel.Configuration
{
    public abstract class Feature : IFeature
    {
        Project _project;
        IRegionManagerExtension _regionManager;
        IContextService _contextService;
        bool _isInitialized;

        protected Project Project => _project;
        protected IRegionManagerExtension RegionManager => _regionManager;
        protected IContextService CurrentContextService => _contextService;

        public virtual void Attach()
        {
            if (!_isInitialized) throw new InvalidOperationException("Cannot attach a feature it is initialized");
        }

        internal void Initialize(Project project, IRegionManagerExtension regionManager, IContextService contextService)
        {
            _isInitialized = true;
            _regionManager = regionManager;
            _project = project;
            _contextService = contextService;
        }

    }
}

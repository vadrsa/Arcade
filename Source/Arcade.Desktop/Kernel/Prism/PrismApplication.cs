using Kernel.Configuration;
using Kernel.Managers;
using Kernel.Workitems;
using Prism.Ioc;
using System;

namespace Kernel.Prism
{
    /// <summary>
    /// Prism Applciation base class based on Unity Container implementation
    /// </summary>
    public abstract class PrismApplication : global::Prism.Unity.PrismApplication
    {

        public readonly bool IsDebug;
        protected readonly Project project;

        public PrismApplication(Project project)
        {
#if DEBUG
            IsDebug = true;
#endif
            ValidateProject();
            this.project = project;
        }

        // TODO: check if all required options are configured
        private void ValidateProject()
        {
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            project.RegisterFeatures(Container.Resolve<IFeatureRegister>());
        }

        /// <summary>
        /// Register main types with the container
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry
                .RegisterInstance<Project>(project)
                .RegisterSingleton<IFeatureRegister, FeatureRegister>()
                .RegisterSingleton<IUIManager, UIManager>()
                .RegisterSingleton<ITaskManager, BaseTaskManager>()
                .RegisterSingleton<IContextService, ContextService>()
                .RegisterSingleton<IRegionManagerExtension, RegionManager>();

            RegionTransformationCollection regionTarnsformationCollection = new RegionTransformationCollection();
            ConfigureRegionTransformations(regionTarnsformationCollection);
            containerRegistry.RegisterInstance<IRegionTransformationCollection>(regionTarnsformationCollection);
        }

        protected virtual void ConfigureRegionTransformations(RegionTransformationCollection collection) { }

    }
}

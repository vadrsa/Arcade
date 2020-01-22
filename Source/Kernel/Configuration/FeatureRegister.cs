using Kernel.Prism;
using Kernel.Workitems;
using Prism.Ioc;
using System;
using System.Collections.Generic;

namespace Kernel.Configuration
{
    class FeatureRegister : Dictionary<Type, IFeature>, IFeatureRegister
    {
        IContainerExtension _container;

        public FeatureRegister(IContainerExtension container)
        {
            _container = container;
        }

        bool IFeatureRegister.Contains<T>()
        {
            return this.ContainsKey(typeof(T));
        }

        T IFeatureRegister.Get<T>()
        {
            return (T)this[typeof(T)];
        }

        void IFeatureRegister.Register<T>(T feature)
        {
            // TODO: check if the module containing this feature is registered before adding
            Add(typeof(T), feature);
            if (feature is Feature)
                (feature as Feature).Initialize(_container.Resolve<Project>(), _container.Resolve<IRegionManagerExtension>(), _container.Resolve<IContextService>());
            feature.Attach();
        }
    }
}

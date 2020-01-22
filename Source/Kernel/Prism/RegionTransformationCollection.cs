using System;
using System.Collections.Generic;
using System.Linq;

namespace Kernel.Prism
{
    public class RegionTransformationCollection : IRegionTransformationCollection
    {
        private List<IRegionTransformation> _regionTransformations;

        public RegionTransformationCollection()
        {
            _regionTransformations = new List<IRegionTransformation>();
        }

        public void Register<T>() where T : IRegionTransformation
        {
            _regionTransformations.Add(Activator.CreateInstance<T>());
        }

        public IRegionTransformation GetTransformation(Type type)
        {
            return _regionTransformations
                .Where(r => r.TransformationType == type)
                .FirstOrDefault();
        }
    }
}

using System;

namespace Kernel.Prism
{
    public abstract class RegionTransformationBase<T> : IRegionTransformation where T : class
    {
        public Type TransformationType => typeof(T);

        public abstract object Transform(object view);
    }
}

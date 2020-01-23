using System;

namespace Kernel.Prism
{
    public interface IRegionTransformationCollection
    {
        IRegionTransformation GetTransformation(Type type);
    }
}

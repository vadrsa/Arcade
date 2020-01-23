using System;

namespace Kernel.Prism
{
    public interface IRegionTransformation
    {
        Type TransformationType { get; }

        object Transform(object view);
    }
}

using Prism.Regions;

namespace Kernel.Prism
{
    public interface IRegionManagerExtension
    {
        IRegionCollection Regions { get; }
        object AddToRegion(string regionName, object view);
    }
}

using Prism.Ioc;

namespace Kernel.Workitems
{
    public abstract class NullWorkitem : WorkitemBase
    {
        public NullWorkitem(IContainerExtension container) : base(container)
        {
        }


    }
}

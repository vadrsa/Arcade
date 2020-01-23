using Infrastructure.Prism;
using Prism.Ioc;
using SecurityModule.Workitems.Test;

namespace Modules
{
    public class SecurityModule : Module
    {
        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
        }
    }
}

using Infrastructure.Prism;
using Kernel.Configuration;
using Prism.Ioc;
using SecurityModule.Features.Login;
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

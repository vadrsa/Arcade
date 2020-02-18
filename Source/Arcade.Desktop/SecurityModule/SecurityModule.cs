using Infrastructure.Constants;
using Infrastructure.Mvvm;
using Infrastructure.Prism;
using Infrastructure.Security;
using Kernel;
using Prism.Ioc;
using SecurityModule.Views;
using SecurityModule.Workitems.Configuration;
using SecurityModule.Workitems.Login;
using SharedEntities;
using System;
using System.Threading.Tasks;

namespace Modules
{
    public class SecurityModule : Module
    {

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.Register<LoginWorkitem>();
        }

    }
}

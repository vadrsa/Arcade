using Infrastructure.Constants;
using Kernel;
using Kernel.Configuration;
using SecurityModule.Constants;
using SecurityModule.Features.Login.Views;
using SecurityModule.Workitems.Login;
using System;

namespace SecurityModule.Features.Login
{
    public class LoginFeature : Feature
    {
        Func<IAuthenticationService> _authenticationServiceResolver;
        public LoginFeature(Func<IAuthenticationService> authenticationServiceResolver)
        {
            _authenticationServiceResolver = authenticationServiceResolver;
        }

        public override void Attach()
        {
            base.Attach();
            if (Project.GetOption<RegionOptions>().IsSupported(KnownRegions.Ribbon))
                RegionManager.AddToRegion(KnownRegions.Ribbon, new LoginItemsDefaultPageCategory());
            else if (Project.GetOption<RegionOptions>().IsSupported(KnownRegions.MainMenu))
                RegionManager.AddToRegion(KnownRegions.MainMenu, new LoginNavBarGroup());

            CommandManager.RegisterCommand(SecurityCommands.Login, OnLoginButtonClicked);
        }

        private void OnLoginButtonClicked()
        {
            CurrentContextService.LaunchModalWorkItem<LoginWorkitem>(
                new LoginWorkitemInitializationData { 
                    AuthenticationService = _authenticationServiceResolver.Invoke() 
                });
        }
    }
}

using Infrastructure.Api;
using Infrastructure.Constants;
using Infrastructure.Mvvm;
using Infrastructure.Security;
using Kernel;
using Kernel.Configuration;
using SecurityModule.Constants;
using SecurityModule.Features.Login;
using SecurityModule.Features.Login.Views;
using SecurityModule.Workitems.Login;
using System;
using System.Threading.Tasks;

namespace SecurityModule.Features
{
    public class LoginFeature : Feature
    {

        Func<IAuthenticationService> _authenticationServiceResolver;
        private LoginButton loginBtn;

        public LoginFeature(Func<IAuthenticationService> authenticationServiceResolver)
        {
            _authenticationServiceResolver = authenticationServiceResolver;

            AppSecurityContext.AppPrincipalChanged += AppSecurityContext_AppPrincipalChanged;
        }

        private void AppSecurityContext_AppPrincipalChanged(object sender, EventArgs e)
        {
            if (AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated)
                loginBtn.Content = "Logout";
            else
                loginBtn.Content = "Login";
        }

        public override void Attach()
        {
            base.Attach();

            loginBtn = (LoginButton)RegionManager.AddToRegion(KnownRegions.MainMenu, new LoginButton());

            CommandManager.RegisterCommand(SecurityCommands.Login, new AsyncCommand(OnLoginButtonClicked));
        }

        private async Task OnLoginButtonClicked()
        {

            if (AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated)
            {
                try
                {
                    await _authenticationServiceResolver.Invoke().LogoutAsync();
                }
                catch (Exception e)
                {

                }
            }
            else
            {
                await CurrentContextService.LaunchModalWorkItem<LoginWorkitem>(
                    new LoginWorkitemInitializationData
                    {
                        AuthenticationService = _authenticationServiceResolver.Invoke()
                    });
            }
        }
    }
}

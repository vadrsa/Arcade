using SecurityModule.Features.Login;

namespace SecurityModule.Workitems.Login
{
    public class LoginWorkitemInitializationData
    {
        public IAuthenticationService AuthenticationService { get; set; }
    }
}

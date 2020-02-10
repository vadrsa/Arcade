using Flurl.Http;
using Infrastructure.Api;
using Infrastructure.Security;
using SecurityModule.Features.Login;
using SharedEntities.Users;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Arcade.Services
{
    public class AuthenticationService : RestConsumingServiceBase, IAuthenticationService
    {
        public AuthenticationService() : base("authentication", ConfigurationManager.AppSettings["ApiUrl"])
        {
        }

        public async Task<UserDto> AuthenticateAsync(string username, string password, CancellationToken token = default)
        {
            var user = await BuildRequest("login").PostJsonAsync(new LoginDto { Username = username, Password = password }, token).ReceiveJson<UserDto>();
            AppSecurityContext.SetCurrentPrincipal(new AppPrincipal(new AppIdentity(user.UserName, user.Token, user.Roles)));
            return user;
        }

        public async Task LogoutAsync(CancellationToken token = default)
        {
            await BuildRequest("logout").PostAsync(new StringContent(""), token);
            AppSecurityContext.SetCurrentPrincipal(AppPrincipal.Anonymous);
        }
    }
}

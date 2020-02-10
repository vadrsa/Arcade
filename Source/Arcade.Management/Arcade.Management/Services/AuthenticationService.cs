using Flurl.Http;
using Infrastructure.Api;
using Infrastructure.Security;
using SecurityModule.Features.Login;
using SharedEntities.Users;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Arcade.Management.Services
{
    public class AuthenticationService : RestConsumingServiceBase, IAuthenticationService
    {
        public AuthenticationService() : base("authentication", ConfigurationManager.AppSettings["ApiUrl"])
        {

        }

        [ApiExceptionHandling]
        private async Task<UserDto> AuthenticateApiAsync(string username, string password, CancellationToken token = default)
        {
            return await BuildRequest("login").PostJsonAsync(new LoginDto { Username = username, Password = password }, token).ReceiveJson<UserDto>().ConfigureAwait(false);
        }

        public async Task AuthenticateAsync(string username, string password, CancellationToken token = default)
        {
            UserDto user = await AuthenticateApiAsync(username, password, token);

            if (user != null)
            {
                AppSecurityContext.SetCurrentPrincipal(new AppPrincipal(new AppIdentity(user.UserName, user.Token)));
            }
        }

        public async Task LogoutAsync(CancellationToken token = default)
        {
            await LogoutApiAsync(token);

            AppSecurityContext.SetCurrentPrincipal(AppPrincipal.Anonymous);

        }

        private async Task LogoutApiAsync(CancellationToken token)
        {
            await BuildRequest("logout").SendAsync(System.Net.Http.HttpMethod.Post).ConfigureAwait(false);
        }
    }
}

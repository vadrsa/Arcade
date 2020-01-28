using SharedEntities.Users;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityModule.Features.Login
{
    public interface IAuthenticationService
    {
        Task<UserDto> AuthenticateAsync(string username, string password, CancellationToken token = default);
        Task LogoutAsync(CancellationToken token = default);
    }
}

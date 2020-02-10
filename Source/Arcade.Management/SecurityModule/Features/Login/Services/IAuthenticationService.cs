using System.Threading;
using System.Threading.Tasks;

namespace SecurityModule.Features.Login
{
    public interface IAuthenticationService
    {
        Task AuthenticateAsync(string username, string password, CancellationToken token = default);
        Task LogoutAsync(CancellationToken token = default);
    }
}

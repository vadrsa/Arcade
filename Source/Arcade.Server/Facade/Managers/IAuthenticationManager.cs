using SharedEntities.Users;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IAuthenticationManager
    {

        Task LogoutAsync();

        Task<UserDto> LoginAsync(LoginDto model);
        
        Task<UserDto> RegisterAsync(RegisterDto model);
    }
}

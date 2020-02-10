using SharedEntities;
using SharedEntities.Users;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IAuthenticationManager
    {
        Task RemoveAsync(string id);

        Task LogoutAsync();

        Task<UserDto> LoginAsync(LoginDto model);
        
        Task<UserDto> RegisterAsync(RegisterDto model);

        Task SetRoleAsync(string id, ApplicationRole role);
    }
}

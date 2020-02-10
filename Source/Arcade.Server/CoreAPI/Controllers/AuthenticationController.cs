using Common.Core;
using Facade.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities;
using SharedEntities.Users;
using System;
using System.Threading.Tasks;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApiControllerBase
    {
        public AuthenticationController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpPost("login")]
        public async Task<UserDto> Login(LoginDto dto)
        {
            return await this.ServiceProvider.GetService<IAuthenticationManager>().LoginAsync(dto);
        }

        [HttpPost("register")]
        [Authorize(ApplicationRole.Admin)]
        public async Task<UserDto> Register(RegisterDto dto)
        {
            return await this.ServiceProvider.GetService<IAuthenticationManager>().RegisterAsync(dto);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task Logout()
        {
            await this.ServiceProvider.GetService<IAuthenticationManager>().LogoutAsync();
        }
    }
}
using Common.Core;
using Common.Faults;
using Facade.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.Users;
using System;
using System.Collections.Generic;
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

        [HttpGet("test")]
        public async Task<List<Fault>> Test()
        {
            return await ServiceProvider.GetService<IFaultManager>().GetAll();
            //var image = new Bitmap(100, 100);
            //var memStream = new MemoryStream();
            //image.Save(memStream, ImageFormat.Jpeg);
            //return await this.ServiceProvider.GetService<IGameManager>().AddAsync(new GameUploadDto { Name = "test", Image = memStream.ToArray() });
        }

        [HttpPost("login")]
        public async Task<UserDto> Login(LoginDto dto)
        {
            return await this.ServiceProvider.GetService<IAuthenticationManager>().LoginAsync(dto);
        }

        [HttpPost("register")]
        public async Task<UserDto> Register(RegisterDto dto)
        {
            try
            {
                return await this.ServiceProvider.GetService<IAuthenticationManager>().RegisterAsync(dto);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task Logout()
        {
            await this.ServiceProvider.GetService<IAuthenticationManager>().LogoutAsync();
        }
    }
}
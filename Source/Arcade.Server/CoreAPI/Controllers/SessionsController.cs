using Common.Core;
using Facade.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ApiControllerBase
    {
        public SessionsController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ComputerQueueDto>> Get()
        {
            return await ServiceProvider.GetService<ISessionManager>().GetAllComputers();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ComputerQueueDto> GetById(string id)
        {
            return await ServiceProvider.GetService<ISessionManager>().GetComputerById(id);
        }

        [HttpGet("{id}/endsession")]
        [Authorize]
        public async Task EndSession(string id)
        {
            await ServiceProvider.GetService<ISessionManager>().EndSession(id);
        }

        [HttpGet("{id}/queue")]
        public async Task<ComputerQueueDto> GetQueue(string id)
        {
            return await ServiceProvider.GetService<ISessionManager>().GetFullComputerById(id);
        }

        [HttpPut("create")]
        [Authorize]
        public async Task<SessionDto> Create([FromBody] SessionUploadDto session)
        {
            return await ServiceProvider.GetService<ISessionManager>().Create(session);
        }

        [HttpPut("enqueue")]
        [Authorize]
        public async Task<SessionDto> Enqueue([FromBody] SessionUploadDto session)
        {
            return await ServiceProvider.GetService<ISessionManager>().AddToQueue(session);
        }
    }
}

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
    public class ComputersController : ApiControllerBase
    {
        public ComputersController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ComputerDto>> Get()
        {
            return await ServiceProvider.GetService<IComputerManager>().GetAll();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ComputerDto> GetById(string id)
        {
            return await ServiceProvider.GetService<IComputerManager>().GetById(id);
        }

        [HttpPost]
        [Authorize(ApplicationRole.Admin)]
        public async Task<ComputerDto> Post([FromBody] ComputerDto computer)
        {
            return await ServiceProvider.GetService<IComputerManager>().AddAsync(computer);
        }

        [HttpPut]
        [Authorize(ApplicationRole.Admin)]
        public async Task Put([FromBody] ComputerDto computer)
        {
            await ServiceProvider.GetService<IComputerManager>().UpdateAsync(computer);
        }

        [HttpDelete("{id}")]
        [Authorize(ApplicationRole.Admin)]
        public async Task Delete(string id)
        {
            await ServiceProvider.GetService<IComputerManager>().RemoveAsync(id);
        }
    }
}

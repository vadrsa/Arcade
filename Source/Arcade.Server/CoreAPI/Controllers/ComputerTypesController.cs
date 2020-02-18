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
    public class ComputerTypesController : ApiControllerBase
    {
        public ComputerTypesController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<ComputerTypeDto>> Get()
        {
            return await ServiceProvider.GetService<IComputerTypeManager>().GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ComputerTypeDto> GetById(string id)
        {
            return await ServiceProvider.GetService<IComputerTypeManager>().GetById(id);
        }

        [HttpPost]
        [Authorize]
        public async Task<ComputerTypeDto> Post([FromBody] ComputerTypeDto type)
        {
            return await ServiceProvider.GetService<IComputerTypeManager>().AddAsync(type);
        }

        [HttpPut]
        [Authorize]
        public async Task Put([FromBody] ComputerTypeDto type)
        {
            await ServiceProvider.GetService<IComputerTypeManager>().UpdateAsync(type);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task Delete(string id)
        {
            await ServiceProvider.GetService<IComputerTypeManager>().RemoveAsync(id);
        }
    }
}

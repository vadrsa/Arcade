using CoreAPI;
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
    public class EmployeeController : ApiControllerBase
    {
        public EmployeeController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        [Authorize(ApplicationRole.Admin)]
        public async Task<IEnumerable<EmployeeDto>> Get()
        {
            return await ServiceProvider.GetService<IEmployeeManager>().GetAll();
        }

        [HttpPost("{id}/report")]
        [Authorize(ApplicationRole.Admin)]
        public async Task<EmployeeReportDto> GetReport(string id, [FromBody]DateTime date)
        {
            return await ServiceProvider.GetService<IEmployeeManager>().GetReport(id, date);
        }

        [HttpGet("{id}")]
        [Authorize(ApplicationRole.Admin)]
        public async Task<EmployeeDto> GetById(string id)
        {
            return await ServiceProvider.GetService<IEmployeeManager>().GetById(id);
        }

        [HttpPost]
        [Authorize(ApplicationRole.Admin)]
        public async Task<EmployeeAddResultDto> Post([FromBody] EmployeeUploadDto employee)
        {
            return await ServiceProvider.GetService<IEmployeeManager>().AddAsync(employee);
        }

        [HttpPut]
        [Authorize(ApplicationRole.Admin)]
        public async Task Put([FromBody] EmployeeDto employee)
        {
            await ServiceProvider.GetService<IEmployeeManager>().UpdateAsync(employee);
        }

        [HttpDelete("{id}")]
        [Authorize(ApplicationRole.Admin)]
        public async Task Delete(string id)
        {
            await ServiceProvider.GetService<IEmployeeManager>().RemoveAsync(id);
        }

        [HttpDelete("{id}/terminate")]
        [Authorize(ApplicationRole.Admin)]
        public async Task Terminate(string id)
        {
            await ServiceProvider.GetService<IEmployeeManager>().TerminateAsync(id);
        }
    }
}
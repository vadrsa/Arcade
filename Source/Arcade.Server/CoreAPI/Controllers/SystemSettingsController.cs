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
    public class SystemSettingsController : ApiControllerBase
    {
        public SystemSettingsController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<SystemSettingDto>> Get()
        {
            return await ServiceProvider.GetService<ISystemSettingManager>().GetAllSettings();
        }

        [HttpGet("{id}")]
        public async Task<SystemSettingDto> GetByType(int id)
        {
            return await ServiceProvider.GetService<ISystemSettingManager>().GetSetting(id);
        }

        [HttpPut]
        public async Task Put([FromBody] SystemSettingDto setting)
        {
            await ServiceProvider.GetService<ISystemSettingManager>().SetSetting(setting);
        }
    }
}

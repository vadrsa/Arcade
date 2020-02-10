using Flurl.Http;
using Infrastructure.Api;
using SharedEntities;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityModule.Services
{
    public class SystemSettingService : RestConsumingServiceBase
    {
        public SystemSettingService() : base("systemsettings", ConfigurationManager.AppSettings["ApiUrl"])
        {
        }

        public async Task<List<SystemSettingDto>> GetAll(CancellationToken token = default)
        {
            return await BuildRequest().GetJsonAsync<List<SystemSettingDto>>(token);
        }


        public async Task<SystemSettingDto> GetById(int id, CancellationToken token = default)
        {
            return await BuildRequest($"{id}").GetJsonAsync<SystemSettingDto>(token);
        }

        public async Task Udpate(SystemSettingDto setting, CancellationToken token = default)
        {
            await BuildRequest().PutJsonAsync(setting, token);
        }

    }
}


using Flurl.Http;
using Infrastructure.Api;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace QueueModule.Services
{
    public class SessionService : RestConsumingServiceBase
    {
        public SessionService() : base("sessions", ConfigurationManager.AppSettings["ApiUrl"])
        {
        }

        public async Task<List<ComputerSessionDto>> GetAll()
        {
            return await BuildRequest().GetJsonAsync<List<ComputerSessionDto>>();
        }

        public async Task<ComputerQueueDto> GetQueue(string id, CancellationToken token = default(CancellationToken))
        {
            return await BuildRequest(id + "/queue").GetJsonAsync<ComputerQueueDto>();
        }
    }
}

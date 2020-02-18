using Flurl.Http;
using Infrastructure.Api;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace StaffModule.Services
{
    public class SessionService : RestConsumingServiceBase
    {
        public SessionService() : base("sessions", ConfigurationManager.AppSettings["ApiUrl"])
        {
        }

        public async Task<List<ComputerQueueDto>> GetAll()
        {
            return await BuildRequest().GetJsonAsync<List<ComputerQueueDto>>();
        }

        public async Task<ComputerQueueDto> GetForUploadByID(string id, CancellationToken token = default(CancellationToken))
        {
            return await BuildRequest($"{id}").GetJsonAsync<ComputerQueueDto>();
        }

        public async Task<SessionDto> Create(SessionUploadDto uploadDTO)
        {
            return await BuildRequest("create").PutJsonAsync(uploadDTO).ReceiveJson<SessionDto>();
        }

        public async Task<SessionDto> Enqueue(SessionUploadDto uploadDTO)
        {
            return await BuildRequest("enqueue").PutJsonAsync(uploadDTO).ReceiveJson<SessionDto>();
        }

        public async Task EndSession(string id)
        {
            await BuildRequest($"{id}/endsession").GetAsync();
        }
    }
}

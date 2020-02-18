using Flurl.Http;
using Infrastructure.Api;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityModule.Services
{
    public class ComputerTypesService : RestConsumingServiceBase
    {
        public ComputerTypesService() : base("ComputerTypes", ConfigurationManager.AppSettings["ApiUrl"])
        {
        }

        public async Task<List<ComputerTypeDto>> GetAll(CancellationToken token = default)
        {
            return await BuildRequest().GetJsonAsync<List<ComputerTypeDto>>(token);
        }


        public async Task<ComputerTypeDto> GetById(string id, CancellationToken token = default)
        {
            return await BuildRequest(id).GetJsonAsync<ComputerTypeDto>(token);
        }

        public async Task<ComputerTypeDto> Add(ComputerTypeDto game, CancellationToken token = default)
        {
            return await BuildRequest().PostJsonAsync(game, token).ReceiveJson<ComputerTypeDto>();
        }

        public async Task Udpate(ComputerTypeDto game, CancellationToken token = default)
        {
            await BuildRequest().PutJsonAsync(game, token);
        }

        public async Task Remove(string id, CancellationToken token = default)
        {
            await BuildRequest(id).DeleteAsync(token);
        }
    }
}

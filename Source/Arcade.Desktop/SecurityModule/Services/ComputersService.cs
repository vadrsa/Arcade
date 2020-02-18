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
    public class ComputersService : RestConsumingServiceBase
    {
        public ComputersService() : base("Computers", ConfigurationManager.AppSettings["ApiUrl"])
        {
        }

        public async Task<List<ComputerDto>> GetAll(CancellationToken token = default)
        {
            return await BuildRequest().GetJsonAsync<List<ComputerDto>>(token);
        }


        public async Task<ComputerDto> GetById(string id, CancellationToken token = default)
        {
            return await BuildRequest(id).GetJsonAsync<ComputerDto>(token);
        }

        public async Task<ComputerDto> Add(ComputerDto computer, CancellationToken token = default)
        {
            return await BuildRequest().PostJsonAsync(computer, token).ReceiveJson<ComputerDto>();
        }

        public async Task Udpate(ComputerDto computer, CancellationToken token = default)
        {
            await BuildRequest().PutJsonAsync(computer, token);
        }

        public async Task Remove(string id, CancellationToken token = default)
        {
            await BuildRequest(id).DeleteAsync(token);
        }
    }
}

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
    public class StaffService : RestConsumingServiceBase
    {
        public StaffService() : base("employee", ConfigurationManager.AppSettings["ApiUrl"])
        {
        }

        public async Task<List<EmployeeDto>> GetAll()
        {
            return await BuildRequest().GetJsonAsync<List<EmployeeDto>>();
        }

        public async Task<EmployeeDto> GetForUploadByID(string id, CancellationToken token = default(CancellationToken))
        {
            return await BuildRequest($"{id}").GetJsonAsync<EmployeeDto>();

        }

        public Task<EmployeeAddResultDto> Add(EmployeeUploadDto uploadDTO)
        {
            return BuildRequest().PostJsonAsync(uploadDTO).ReceiveJson<EmployeeAddResultDto>();
        }

        public async Task Update(EmployeeDto uploadDTO)
        {
            await BuildRequest().PutJsonAsync(uploadDTO).ConfigureAwait(false);
        }

        public async Task Remove(string id)
        {
            await BuildRequest("" + id).DeleteAsync().ConfigureAwait(false);
        }
    }
}

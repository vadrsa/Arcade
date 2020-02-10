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

namespace Arcade.StaffModule.Services
{
    class EmployeeService : RestConsumingServiceBase
    {
        public EmployeeService() : base("employee", ConfigurationManager.AppSettings["ApiUrl"])
        {
        }

        [ApiExceptionHandling]
        public async Task<List<EmployeeDto>> GetAll()
        {
            return await BuildRequest().GetJsonAsync<List<EmployeeDto>>();
        }


        [ApiExceptionHandling]
        public async Task<EmployeeDto> GetForUploadByID(string id, CancellationToken token = default(CancellationToken))
        {
            return await BuildRequest($"{id}").GetJsonAsync<EmployeeDto>();

        }

        [ApiExceptionHandling]
        public Task<int> Add(EmployeeDto uploadDTO)
        {

            return BuildRequest("insert").PostJsonAsync(uploadDTO).ReceiveJson<int>();
        }

        [ApiExceptionHandling]
        public async Task AddList(List<EmployeeDto> dtos)
        {
            throw new NotImplementedException();
        }

        [ApiExceptionHandling]
        public async Task Update(EmployeeDto uploadDTO)
        {
            await BuildRequest("update").PostJsonAsync(uploadDTO).ConfigureAwait(false);
        }

        [ApiExceptionHandling]
        public async Task Remove(string id)
        {
            await BuildRequest("" + id).DeleteAsync().ConfigureAwait(false);
        }
    }
}

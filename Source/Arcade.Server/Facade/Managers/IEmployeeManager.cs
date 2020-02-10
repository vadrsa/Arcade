using SharedEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IEmployeeManager
    {
        Task<List<EmployeeDto>> GetAll();
        Task<EmployeeDto> GetById(string id);
        Task<EmployeeAddResultDto> AddAsync(EmployeeUploadDto employee, CancellationToken token = new CancellationToken());
        Task UpdateAsync(EmployeeDto employee, CancellationToken token = new CancellationToken());
        Task RemoveAsync(string id, CancellationToken token = new CancellationToken());
    }
}

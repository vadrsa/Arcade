using SharedEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IComputerManager
    {
        Task<List<ComputerDto>> GetAll();
        Task<ComputerDto> GetById(string id);
        Task<ComputerDto> AddAsync(ComputerDto computer, CancellationToken token = new CancellationToken());
        Task UpdateAsync(ComputerDto computer, CancellationToken token = new CancellationToken());
        Task RemoveAsync(string id, CancellationToken token = new CancellationToken());
    }
}

using SharedEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IComputerTypeManager
    {
        Task<List<ComputerTypeDto>> GetAll();
        Task<ComputerTypeDto> GetById(string id);
        Task<ComputerTypeDto> AddAsync(ComputerTypeDto type, CancellationToken token = new CancellationToken());
        Task UpdateAsync(ComputerTypeDto type, CancellationToken token = new CancellationToken());
        Task RemoveAsync(string id, CancellationToken token = new CancellationToken());
    }
}

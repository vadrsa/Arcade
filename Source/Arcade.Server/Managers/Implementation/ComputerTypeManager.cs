using BusinessEntities;
using Common.Core;
using Facade.Managers;
using Facade.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class ComputerTypeManager : ManagerBase, IComputerTypeManager
    {
        public ComputerTypeManager(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<List<ComputerTypeDto>> GetAll()
        {
            return Mapper.Map<List<ComputerTypeDto>>(await ServiceProvider.GetService<IComputerTypeRepository>().GetAllAsync());
        }

        public async Task<ComputerTypeDto> GetById(string id)
        {
            return Mapper.Map<ComputerTypeDto>(await ServiceProvider.GetService<IComputerTypeRepository>().FindByIDAsync(id));
        }

        [Transaction]
        public async Task<ComputerTypeDto> AddAsync(ComputerTypeDto type, CancellationToken token = default)
        {

            var added = await ServiceProvider.GetService<IComputerTypeRepository>().InsertAsync(Mapper.Map<ComputerType>(type), token);

            return Mapper.Map<ComputerTypeDto>(added);
        }

        [Transaction]
        public async Task UpdateAsync(ComputerTypeDto type, CancellationToken token = default)
        {
            await ServiceProvider.GetService<IComputerTypeRepository>().UpdateAsync(Mapper.Map<ComputerType>(type), token);
        }

        [Transaction]
        public async Task RemoveAsync(string id, CancellationToken token = default)
        {
            var type = await ServiceProvider.GetService<IComputerTypeRepository>().FindByIDAsync(id, token);
            await ServiceProvider.GetService<IComputerTypeRepository>().RemoveAsync(type);
        }
    }
}

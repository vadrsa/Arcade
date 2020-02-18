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
    public class ComputerManager : ManagerBase, IComputerManager
    {
        public ComputerManager(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<List<ComputerDto>> GetAll()
        {
            return Mapper.Map<List<ComputerDto>>(await ServiceProvider.GetService<IComputerRepository>().LoadWith(c => c.Type).GetAllAsync());
        }

        public async Task<ComputerDto> GetById(string id)
        {
            return Mapper.Map<ComputerDto>(await ServiceProvider.GetService<IComputerRepository>().LoadWith(c => c.Type).FindByIDAsync(id));
        }

        [Transaction]
        public async Task<ComputerDto> AddAsync(ComputerDto computer, CancellationToken token = default)
        {

            var added = await ServiceProvider.GetService<IComputerRepository>().InsertAsync(Mapper.Map<Computer>(computer), token);

            return Mapper.Map<ComputerDto>(added);
        }

        [Transaction]
        public async Task UpdateAsync(ComputerDto computer, CancellationToken token = default)
        {
            await ServiceProvider.GetService<IComputerRepository>().UpdateAsync(Mapper.Map<Computer>(computer), token);
        }

        [Transaction]
        public async Task RemoveAsync(string id, CancellationToken token = default)
        {
            var computer = await ServiceProvider.GetService<IComputerRepository>().FindByIDAsync(id, token);
            computer.IsTerminated = !computer.IsTerminated;
            await ServiceProvider.GetService<IComputerRepository>().UpdateAsync(computer);
        }
    }
}

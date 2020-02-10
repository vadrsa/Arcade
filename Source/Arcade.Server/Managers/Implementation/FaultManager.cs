using Common.Faults;
using DataAccess;
using LinqToDB;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class FaultManager : ManagerBase, IFaultManager
    {
        public FaultManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<Fault>> GetAll()
        {
            return await MemoryCache.GetOrCreate("Faults", GetFaults);
        }

        private async Task<List<Fault>> GetFaults(ICacheEntry arg)
        {
            return await ServiceProvider.GetService<ArcadeContext>().Faults.ToListAsync();
        }

        public async Task<Fault> GetByCode(int code)
        {
            return (await this.GetAll()).FirstOrDefault(f => f.Code == code);
        }

        public async Task<Fault> GetByType(FaultType type)
        {
            return await this.GetByCode((int)type);
        }
    }
}

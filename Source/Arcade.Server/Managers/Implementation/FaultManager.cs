using Common.Faults;
using DataAccess;
using Microsoft.Extensions.Caching.Memory;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class FaultManager : ManagerBase<ArcadeContext>, IFaultManager
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
            return await Context.Faults.ToAsyncEnumerable().ToList();
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

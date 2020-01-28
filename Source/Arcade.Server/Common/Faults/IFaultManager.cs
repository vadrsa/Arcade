using SharedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Faults
{
    public interface IFaultManager
    {
        Task<List<Fault>> GetAll();
        Task<Fault> GetByCode(int code);
        Task<Fault> GetByType(FaultType type);
    }
}

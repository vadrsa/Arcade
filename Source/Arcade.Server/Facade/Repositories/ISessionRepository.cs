using BusinessEntities;
using Common.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facade.Repositories
{
    public interface ISessionRepository : IRepository<Session, string, ISessionRepository>
    {

        Task<List<Session>> GetComputerSessionsAsync(string computerId);
    }
}

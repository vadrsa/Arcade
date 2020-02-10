using BusinessEntities;
using Common.DataAccess;

namespace Facade.Repositories
{
    public interface IGameRepository : IRepository<Game, string, IGameRepository>
    {
    }
}

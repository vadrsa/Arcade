using BusinessEntities;
using Common.DataAccess;

namespace Facade.Repositories
{
    public interface IImageRepository : IRepository<Image, string, IImageRepository>
    {
    }
}

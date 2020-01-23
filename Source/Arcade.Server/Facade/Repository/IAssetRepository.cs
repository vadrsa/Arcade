using BusinessEntities;
using System.Threading;
using System.Threading.Tasks;

namespace Facade.Repository
{
    public interface IAssetRepository
    {
        Task<string> InsertAsync(Asset asset, CancellationToken token = new CancellationToken());
        Task<string> UpdateAsync(Asset asset, CancellationToken token = new CancellationToken());
        void Remove(string path);
    }
}

using BusinessEntities;
using System.Threading;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IAssetManager
    {
        Task<string> InsertAsync(Asset asset, CancellationToken token = new CancellationToken());
        Task<string> UpdateAsync(Asset asset, CancellationToken token = new CancellationToken());
        void Remove(string path);
    }
}

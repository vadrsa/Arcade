using BusinessEntities;
using System.Threading;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IImageManager
    {
        Task<Image> InsertBytesAsync(byte[] image, CancellationToken token = new CancellationToken());
        Task<Image> UpdateBytesAsync(byte[] bytes, string path, CancellationToken token = new CancellationToken());
        Task RemoveAsync(Image image, CancellationToken token = new CancellationToken());
    }
}

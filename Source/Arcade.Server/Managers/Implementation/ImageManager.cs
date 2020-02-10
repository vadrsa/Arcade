using BusinessEntities;
using Common.Core;
using DataAccess;
using Facade.Managers;
using Facade.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class ImageManager : ManagerBase ,IImageManager
    {
        public ImageManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [Transaction]
        public async Task<Image> InsertBytesAsync(byte[] bytes, CancellationToken token = new CancellationToken())
        {
            string path = await ServiceProvider.GetService<IAssetManager>()
                .InsertAsync(new ImageAsset(bytes), token);
            Image image = new Image { Path = path };
            return await ServiceProvider.GetService<IImageRepository>().InsertAsync(image, token);
        }

        [Transaction]
        public async Task<Image> UpdateBytesAsync(byte[] bytes, string path, CancellationToken token = new CancellationToken())
        {
            if (String.IsNullOrEmpty(path)) return await InsertBytesAsync(bytes, token);
            else
            {
                ServiceProvider.GetService<IAssetManager>().Remove(path);
                var newPath = await ServiceProvider.GetService<IAssetManager>()
                    .InsertAsync(new ImageAsset(bytes), token);
                Image image = (await ServiceProvider.GetService<IImageRepository>().FindAsync(i => i.Path == path)).Single();
                image.Path = newPath;
                await ServiceProvider.GetService<IImageRepository>().UpdateAsync(image);
                return image;
            }
        }

        [Transaction]
        public async Task RemoveAsync(Image image, CancellationToken token = default(CancellationToken))
        {
            Image original = await ServiceProvider.GetService<IImageRepository>().FindByIDAsync(image.Id);
            if (original != null)
            {
                await ServiceProvider.GetService<IImageRepository>().RemoveAsync(original);
                ServiceProvider.GetService<IAssetManager>().Remove(original.Path);
            }
        }
    }
}

using BusinessEntities;
using Facade.Managers;
using Facade.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class ImageManager : IImageManager
    {
        private IServiceProvider serviceProvider;

        public ImageManager(IServiceProvider provider)
        {
            serviceProvider = provider;
        }

        public async Task<Image> InsertBytesAsync(byte[] image, CancellationToken token = new CancellationToken())
        {
            throw new NotImplementedException();
            //string path = await serviceProvider.GetService<IAssetRepository>()
            //    .InsertAsync(new ImageAsset(image), token);
            //return await serviceProvider.GetService<IImageRepository>()
            //    .InsertAsync(new Image() { Path = path }, token);
        }


        public async Task<Image> UpdateBytesAsync(byte[] bytes, string path, CancellationToken token = new CancellationToken())
        {
            throw new NotImplementedException();
            //Image image = (await serviceProvider.GetService<IImageRepository>()
            //    .FindAsync(i => i.Path == path)).First();
            //path = await serviceProvider.GetService<IAssetRepository>()
            //    .UpdateAsync(new ImageAsset(bytes, path), token);
            //return image;
        }

        public async Task RemoveAsync(Image image, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
            //Image original = await serviceProvider.GetService<IImageRepository>().FindByIDAsync(image.ID);
            //int id = await serviceProvider.GetService<IImageRepository>().RemoveAsync(original, token);
            //if (id != 0)
            //    serviceProvider.GetService<IAssetRepository>().Remove(original.Path);
        }
    }
}

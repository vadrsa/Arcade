using BusinessEntities;
using DataAccess;
using Facade.Managers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class ImageManager : ManagerBase<ArcadeContext> ,IImageManager
    {
        public ImageManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<Image> InsertBytesAsync(byte[] bytes, CancellationToken token = new CancellationToken())
        {
            string path = await ServiceProvider.GetService<IAssetManager>()
                .InsertAsync(new ImageAsset(bytes), token);
            Image image = new Image { Path = path };
            await Context.Images.AddAsync(image, token);
            return image;
        }


        public async Task<Image> UpdateBytesAsync(byte[] bytes, string path, CancellationToken token = new CancellationToken())
        {
            if (String.IsNullOrEmpty(path)) return await InsertBytesAsync(bytes, token);
            else
            {
                ServiceProvider.GetService<IAssetManager>().Remove(path);
                var newPath = await ServiceProvider.GetService<IAssetManager>()
                    .InsertAsync(new ImageAsset(bytes), token);
                Image image = Context.Images.Where(i => i.Path == path).Single();
                image.Path = newPath;
                Context.Update(image);
                return image;
            }
        }

        public async Task RemoveAsync(Image image, CancellationToken token = default(CancellationToken))
        {
            Image original = await Context.Images.FindAsync(image.Id);
            if (original != null)
            {
                Context.Images.Remove(original);
                ServiceProvider.GetService<IAssetManager>().Remove(original.Path);
            }
        }
    }
}

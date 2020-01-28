using BusinessEntities;
using Facade.Managers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Managers.Implementation
{

    public class AssetManager : IAssetManager
    {
        IHostingEnvironment Env { get; set; }
        public AssetManager(IHostingEnvironment env)
        {
            Env = env;
        }

        private string GetFullPath(string relativePath)
        {
            return Path.Combine(Env.WebRootPath, relativePath);
        }

        public async Task<string> InsertAsync(Asset asset, CancellationToken token = default(CancellationToken))
        {
            string relativePath = Path.Combine("assets", asset.Path);
            string fullPath = GetFullPath(relativePath);
            using (System.IO.FileStream fs = new FileStream(fullPath, FileMode.CreateNew))
            {
                await fs.WriteAsync(asset.Contents, 0, asset.Contents.Length);
            }
            return relativePath;
        }

        public async Task<string> UpdateAsync(Asset asset, CancellationToken token = default(CancellationToken))
        {
            string relativePath = asset.Path;
            string fullPath = GetFullPath(relativePath);
            using (System.IO.FileStream fs = new FileStream(fullPath, FileMode.Open))
            {
                fs.SetLength(0);
                await fs.WriteAsync(asset.Contents, 0, asset.Contents.Length);
            }
            return relativePath;
        }

        public void Remove(string path)
        {
            string fullPath = GetFullPath(path);
            File.Delete(fullPath);
        }
    }
}

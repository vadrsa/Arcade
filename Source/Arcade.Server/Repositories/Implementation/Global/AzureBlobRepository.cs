using BusinessEntities;
using Facade.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Implementation.Global
{
    public class AzureBlobRepository : IAssetRepository
    {
        string accessKey = string.Empty;

        public AzureBlobRepository(IConfiguration configuration)
        {
            accessKey = configuration.GetConnectionString("AccessKey");
        }

        public async Task<string> InsertAsync(Asset asset, CancellationToken token = default)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            string strContainerName = "uploads";
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(GenerateFileName());
            cloudBlockBlob.Properties.ContentType = asset.ContentType;
            await cloudBlockBlob.UploadFromByteArrayAsync(asset.Contents, 0, asset.Contents.Length);
            return cloudBlockBlob.Uri.AbsoluteUri;
        }

        private string GenerateFileName()
        {
            return "file-"+Guid.NewGuid().ToString().Replace("-", "");
        }

        public async Task<string> UpdateAsync(Asset asset, CancellationToken token = default)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var blob = await cloudBlobClient.GetBlobReferenceFromServerAsync(new Uri(asset.Path));
            await blob.UploadFromByteArrayAsync(asset.Contents, 0, asset.Contents.Length);
            return blob.Uri.AbsoluteUri;
        }

        public async void Remove(string path)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var blob = await cloudBlobClient.GetBlobReferenceFromServerAsync(new Uri(path));
            blob.DeleteAsync();
        }
    }
}

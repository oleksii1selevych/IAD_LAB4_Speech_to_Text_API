using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWithGoogleCloudStorage.Configuration;

namespace WorkingWithGoogleCloudStorage.CloudStorage
{
    internal class GoogleCloudStorage : ICloudStorage
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;

        public GoogleCloudStorage(ApplicationConfiguration configuration)
        {
            googleCredential = GoogleCredential.FromFile(configuration.GoogleCloudCredentialFile);
            storageClient = StorageClient.Create(googleCredential);
            bucketName = configuration.GoogleCloudStorageBucket;
        }

        public async Task DeleteFileAsync(string objectName)
            => await storageClient.DeleteObjectAsync(bucketName, objectName);

        public async Task<string> UploadFileAsync(string localPath, string? objectName = null)
        {
            using (var file = File.OpenRead(localPath))
            {
                objectName = objectName ?? Path.GetFileName(localPath);
                var dataObject = await storageClient.UploadObjectAsync(bucketName, objectName, null, file);

                return String.Format("gs://{0}/{1}", dataObject.Bucket, dataObject.Name);
            }
        }
    }
}

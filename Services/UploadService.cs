using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ForumProject.Services
{
    public class UploadService : IUploadService
    {
        private readonly string _containerName;
        private readonly string _connectionString;
        private readonly BlobServiceClient _blobServiceClient;

        public UploadService(IConfiguration configuration)
        {
            
            _connectionString = configuration.GetConnectionString("azure_blob_storage");
            _containerName = configuration.GetConnectionString("container_name");
            _blobServiceClient = new BlobServiceClient(_connectionString);
        }

        public BlobContainerClient GetBlobContainerClient()
        {
            return _blobServiceClient.GetBlobContainerClient(_containerName);
        }

        public async Task<string> UploadImageAndGetUriAsync(string imageName,Stream imageStream){
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(imageName);
            var blobUploadOptions = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = "image/jpg" }
            };
            await blobClient.UploadAsync(imageStream,blobUploadOptions);
            return blobClient.Uri.AbsoluteUri;
        }
    }
    public interface IUploadService{
        BlobContainerClient GetBlobContainerClient();
        Task<string> UploadImageAndGetUriAsync(string imageName, Stream imageStream);
    }
}
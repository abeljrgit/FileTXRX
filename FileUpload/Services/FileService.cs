using Azure.Storage.Blobs;
using FileUpload.Models;

namespace FileUpload.Services
{
    public class FileService : IFileService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public FileService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task Upload(FileModel fileModel)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("mycontainer");
            var blobClient = blobContainer.GetBlobClient(fileModel.ImageFile.FileName);
            await blobClient.UploadAsync(fileModel.ImageFile.OpenReadStream());
        }

        public async Task<Stream> Get(string imageName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("mycontainer");

            var blobClient = blobContainer.GetBlobClient(imageName);
            var downloadContent = await blobClient.DownloadAsync();
            return downloadContent.Value.Content;
        }
    }
}

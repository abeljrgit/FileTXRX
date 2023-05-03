using FileUpload.Models;

namespace FileUpload.Services
{
    public interface IFileService
    {
        Task Upload(FileModel fileModel);
        Task<Stream> Get(string imageName);
    }
}

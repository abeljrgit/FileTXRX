using FileUpload.Models;
using FileUpload.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileUpload.Controllers
{
    [Route("api/File")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        //  https://localhost:7032/api/File/upload
        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] FileModel model)
        {
            if (model.ImageFile != null)
            {
                await _fileService.Upload(model);
            }

            return Ok();
        }

        // https://localhost:7032/api/File/get?fileName=next.svg
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get(string fileName)
        {
            var imgStream = await _fileService.Get(fileName);
            string fileType = "jpeg";
            if (fileName.Contains("png"))
            {
                fileType = "png";
            }
            if (fileName.Contains("svg"))
            {
                fileType = "svg+xml";
            }
            return File(imgStream, $"image/{fileType}");
        }


        // https://localhost:7032/api/File/download?fileName=next.svg
        [Route("download")]
        [HttpGet]
        public async Task<IActionResult> GetDownload(string fileName)
        {
            var imgStream = await _fileService.Get(fileName);
            string fileType = "jpeg";
            if (fileName.Contains("png"))
            {
                fileType = "png";
            }
            if (fileName.Contains("svg"))
            {
                fileType = "svg";
            }
            return File(imgStream, $"image/{fileType}", $"blobfile.{fileType}");
        }
    }
}

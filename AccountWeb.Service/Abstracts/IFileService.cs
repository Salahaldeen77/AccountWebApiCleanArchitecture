using Microsoft.AspNetCore.Http;

namespace AccountWeb.Service.Abstracts
{
    public interface IFileService
    {
        public Task<string> UploadImageAsync(string Location, IFormFile file);
    }
}

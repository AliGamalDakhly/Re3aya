using Microsoft.AspNetCore.Http;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IAiService
    {
        Task<string> UploadFileToAssemblyAi(IFormFile file);
        Task<string> TranscribeFromUrlAsync(string audioUrl);
    }
}

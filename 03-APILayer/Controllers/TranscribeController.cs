
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranscribeController : ControllerBase
    {

        private readonly IAiService _aiService;
        private readonly IMedicalSuggest _medicalSuggestService;
        private readonly IRagService _ragService;
        public TranscribeController(IAiService aiService, IMedicalSuggest medicalSuggestService, IRagService ragService)
        {
            _aiService = aiService;
            _medicalSuggestService = medicalSuggestService;
            _ragService = ragService;
        }

        [HttpPost("AudioToText")]
        public async Task<IActionResult> TranscribeAudio(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Upload file to AssemblyAI
            var uploadUrl = await _aiService.UploadFileToAssemblyAi(file);

            // Transcribe
            var transcript = await _aiService.TranscribeFromUrlAsync(uploadUrl);

            var suggestions = await _medicalSuggestService.GetMedicalResponseAsync(transcript);
            string responseText = suggestions.formal_text;

            var ragResponse = await _ragService.GetRagResponseAsync(responseText);
            return Ok(ragResponse);
        }


    }
}

using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["AssemblyAi:ApiKey"];
            Console.WriteLine("[DEBUG] Loaded AssemblyAI API Key: " + (_apiKey ?? "null"));
            _httpClient.BaseAddress = new Uri("https://api.assemblyai.com/v2/");
            _httpClient.DefaultRequestHeaders.Add("authorization", _apiKey);
        }

        public async Task<string> UploadFileToAssemblyAi(IFormFile file)
        {
            using var content = new StreamContent(file.OpenReadStream());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            var response = await _httpClient.PostAsync("upload", content);
            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Upload response: " + json);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Upload failed: {json}");

            var root = JsonDocument.Parse(json).RootElement;
            return root.GetProperty("upload_url").GetString();
        }

        public async Task<string> TranscribeFromUrlAsync(string audioUrl)
        {
            var requestPayload = new
            {
                audio_url = audioUrl,
                language_code = "ar"
            };
            var content = new StringContent(JsonSerializer.Serialize(requestPayload));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync("transcript", content);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Transcription request failed: {json}");

            var transcriptId = JsonDocument.Parse(json).RootElement.GetProperty("id").GetString();

            // Polling
            while (true)
            {
                await Task.Delay(3000);

                var statusResponse = await _httpClient.GetAsync($"transcript/{transcriptId}");
                var statusJson = await statusResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Polling response: " + statusJson);

                if (!statusResponse.IsSuccessStatusCode)
                    throw new Exception($"Polling failed: {statusJson}");

                var root = JsonDocument.Parse(statusJson).RootElement;
                var status = root.GetProperty("status").GetString();

                if (status == "completed")
                    return root.GetProperty("text").GetString();
                if (status == "error")
                    return $"Error: {root.GetProperty("error").GetString()}";
            }
        }
    }
}

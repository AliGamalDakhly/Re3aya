using _02_BusinessLogicLayer.Service.IServices;
using System.Text;
using System.Text.Json;
using static _02_BusinessLogicLayer.DTOs.AiDTO.RagDTO;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class RagService : IRagService
    {
        private readonly HttpClient _httpClient;

        public RagService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DoctorResult>> GetRagResponseAsync(string question)
        {
            var url = "https://sidALi212-rag.hf.space/rag";

            var payload = new { question = question };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var ragResponse = JsonSerializer.Deserialize<RagResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return ragResponse?.Results ?? new List<DoctorResult>();
        }
    }
}

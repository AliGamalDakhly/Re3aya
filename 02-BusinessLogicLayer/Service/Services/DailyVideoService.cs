using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _02_BusinessLogicLayer.Service.IServices;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class DailyVideoService: IDailyVideoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "2f6fd93b833d128bb24c9bcc3cab2f634fe88dae834b5be41d914ae9a68902dd";

        public DailyVideoService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.daily.co/v1/")
            };
       
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string?> CreateRoomAsync(string appointmentId)
        {
            var body = new
            {
                name = $"appointment-{appointmentId}-{Guid.NewGuid()}",
                properties = new
                {
                    exp = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds(), // 1-hour expiry
                    is_private = true,
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("rooms", content);

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("url").GetString();
        }
    }
}

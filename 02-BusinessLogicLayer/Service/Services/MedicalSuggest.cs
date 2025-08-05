using _02_BusinessLogicLayer.DTOs.AiDTO;
using _02_BusinessLogicLayer.Service.IServices;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class MedicalSuggest : IMedicalSuggest
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "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";
        private const string ApiUrl = "https://openrouter.ai/api/v1/chat/completions";
        private const string Model = "openrouter/horizon-beta";

        public MedicalSuggest(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
            _httpClient.DefaultRequestHeaders.Add("X-Title", "GemmaMedicalBot");
        }

        public async Task<SuggestResponsesDTO> GetMedicalResponseAsync(string userInput)
        {
            string prompt = $@"
    You are a smart medical assistant. When the user provides a message in colloquial Arabic:
    Add suggestions for up to 3 doctors in one string and no need to add extra words just the needed doctors.
    pick from these specialties:[طب القلب,
    طب الأعصاب,
    طب الأطفال,
    طب العيون,
    طب الجلدية,
    جراحة العظام,
    الطب الباطني,
    جراحة عامة,
    أنف وأذن وحنجرة,
    طب النساء والتوليد,
    طب الأورام,
    طب الجهاز الهضمي,
    طب الروماتيزم,
    طب المسالك البولية,
    طب الغدد الصماء,
    طب الرئة,
    طب النفسي,
    طب التخدير,
    طب الأشعة,
    طب الطوارئ]
    Return the result strictly in this JSON format:
    {{
        ""formal_text"": ""your answer""
    }}

    User's message: {userInput.Trim().Substring(0, Math.Min(userInput.Length, 500))}
    ";

            var requestBody = new
            {
                model = Model,
                messages = new[]
                {
            new { role = "user", content = prompt }
        }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(ApiUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Response from OpenRouter:");
            Console.WriteLine(responseString);

            using var doc = JsonDocument.Parse(responseString);
            var root = doc.RootElement;

            // Check for error response
            if (root.TryGetProperty("error", out var errorProp))
            {
                var errorMsg = errorProp.GetProperty("message").GetString();
                throw new Exception($"OpenRouter API error: {errorMsg}");
            }

            // Try to safely extract the message content
            if (root.TryGetProperty("choices", out var choicesArray) &&
                choicesArray.GetArrayLength() > 0 &&
                choicesArray[0].TryGetProperty("message", out var messageObj) &&
                messageObj.TryGetProperty("content", out var contentElem))
            {
                var rawContent = contentElem.GetString();

                var cleanedJson = rawContent
                    .Replace("```json", "")
                    .Replace("```", "")
                    .Trim();

                return JsonSerializer.Deserialize<SuggestResponsesDTO>(cleanedJson);
            }
            else
            {
                Console.WriteLine("Unexpected response format:");
                Console.WriteLine(responseString);
                throw new Exception("LLM response missing expected structure: choices/message/content.");
            }
        }
    }
}

 using _02_BusinessLogicLayer.DTOs.PaymentDTOs;
 using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class PaymobService : IPaymobService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public PaymobService(HttpClient httpClient, IConfiguration config)
        {
            _http = httpClient;
            _config = config;
        }

        public async Task<string> GetAuthTokenAsync()
        {
            var apiKey = _config["Paymob:APIKey"];
            var payload = new { api_key = apiKey };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _http.PostAsync("https://accept.paymob.com/api/auth/tokens", content);
            var result = await response.Content.ReadAsStringAsync();

            var json = JsonDocument.Parse(result);
            return json.RootElement.GetProperty("token").GetString();
        }

        public async Task<string> CreateOrderAsync(string authToken, int amountCents, List<AppointmentItem> items)
        {
            var paymobItems = items.Select(item => new
            {
                name = item.Name,
                amount_cents = item.Amount,
                description = item.Description,
                quantity = item.Quantity
            }).ToList();

            var payload = new
            {
                auth_token = authToken,
                delivery_needed = false,
                amount_cents = amountCents,
                currency = "EGP",
                items = paymobItems
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _http.PostAsync("https://accept.paymob.com/api/ecommerce/orders", content);
            var result = await response.Content.ReadAsStringAsync();

            var json = JsonDocument.Parse(result);
            return json.RootElement.GetProperty("id").GetRawText();
        }

        public async Task<string> GetPaymentKeyAsync(string authToken, string orderId, AppointmentPaymentRequest request)
        {
            var billingData = new
            {
                apartment = request.Apartment,
                email = request.Email,
                floor = request.Floor,
                first_name = request.FirstName,
                street = request.Street,
                building = request.Building,
                phone_number = request.PhoneNumber,
                shipping_method = "NA",
                postal_code = request.PostalCode,
                city = request.City,
                country = request.Country,
                last_name = request.LastName,
                state = request.State
            };

            var payload = new
            {
                auth_token = authToken,
                amount_cents = request.Amount,
                expiration = 3600,
                order_id = orderId,
                billing_data = billingData,
                currency = "EGP",
                integration_id = int.Parse(_config["Paymob:IntegrationId"]),
                lock_order_when_paid = true
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _http.PostAsync("https://accept.paymob.com/api/acceptance/payment_keys", content);
            var result = await response.Content.ReadAsStringAsync();

            var json = JsonDocument.Parse(result);
            return json.RootElement.GetProperty("token").GetString();
        }

        public string GetIframeUrl(string paymentToken)
        {
            var iframeId = _config["Paymob:IframeId"];
            return $"https://accept.paymob.com/api/acceptance/iframes/{iframeId}?payment_token={paymentToken}";
        }
 

        public async Task<JsonDocument> RefundAsync(int transactionId, int amountCents)
        {
            var authToken = await GetAuthTokenAsync();

            var payload = new
            {
                auth_token = authToken,
                transaction_id = transactionId,
                amount_cents = amountCents
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _http.PostAsync("https://accept.paymob.com/api/acceptance/void_refund/refund", content);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"paymob refund error: {result}");

            return JsonDocument.Parse(result);
        }

    

    }
}

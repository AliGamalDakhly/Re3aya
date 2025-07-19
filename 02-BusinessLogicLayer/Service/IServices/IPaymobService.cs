using _02_BusinessLogicLayer.DTOs.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IPaymobService
    {
        Task<string> GetAuthTokenAsync();
        Task<string> CreateOrderAsync(string authToken, int amountCents, List<AppointmentItem> items);
        Task<string> GetPaymentKeyAsync(string authToken, string orderId, AppointmentPaymentRequest request);
        string GetIframeUrl(string paymentToken);
        Task<JsonDocument> RefundAsync(int transactionId, int amountCents);

    

    }
}

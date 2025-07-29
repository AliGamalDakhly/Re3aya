using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _02_BusinessLogicLayer.DTOs.PaymentDTOs
{

    public class Payment
    {
        public int PaymentId { get; set; }
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public int TransactionId { get; set; }
        public string Currency { get; set; }
        public string CardType { get; set; }
        public string TxnResponseCode { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Appointment Appointment { get; set; }
    }

    public class PaymobCallbackDTO
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public bool Success { get; set; }
        public int AmountCents { get; set; }
        public string Message { get; set; }
        public string CardType { get; set; }
        public string Pan { get; set; }
        public string Hmac { get; set; }
        public DateTime CreatedAt { get; set; }
    }


    public class PaymentDtoForUser
    {
        public int PaymentId { get; set; }
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public int TransactionId { get; set; }
        public DateTime CreatedAt { get; set; }
    }





}
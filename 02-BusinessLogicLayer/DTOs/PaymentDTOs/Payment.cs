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
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public int TransactionId { get; set; }
    public string Currency { get; set; }
    public string CardType { get; set; }   
    public string TxnResponseCode { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Appointment Appointment { get; set; }
}

 

}
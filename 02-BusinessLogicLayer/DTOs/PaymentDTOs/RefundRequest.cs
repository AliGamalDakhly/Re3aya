namespace _02_BusinessLogicLayer.DTOs.PaymentDTOs
{
    public class RefundRequest
    {
        public int TransactionId { get; set; }
        public int AmountCents { get; set; }

    }
}

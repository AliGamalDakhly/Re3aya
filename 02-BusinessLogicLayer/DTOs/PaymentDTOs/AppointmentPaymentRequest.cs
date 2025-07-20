namespace _02_BusinessLogicLayer.DTOs.PaymentDTOs
{
    public class AppointmentPaymentRequest
    {
        public int Amount { get; set; } 

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Street { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Apartment { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public List<AppointmentItem> Items { get; set; }
    }

    public class AppointmentItem
    {
        public string Name { get; set; }
        public string ServiceId { get; set; }
        public string ServiceCatogry { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int Quantity { get; set; }
    }


}

namespace _02_BusinessLogicLayer.DTOs.AddressDTOs
{
    public class AddressDTO
    {
        public string Location { get; set; }
        public string DetailedAddress { get; set; }

        public int CityId { get; set; }
        public int DoctorId { get; set; }
    }
}

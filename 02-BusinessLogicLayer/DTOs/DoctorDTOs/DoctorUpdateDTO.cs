using _01_DataAccessLayer.Enums;

namespace _02_BusinessLogicLayer.DTOs.DoctorDTOs
{
    public class DoctorUpdateDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        // public double Balance { get; set; }
        public int? ExpYears { get; set; }
        public string AboutMe { get; set; }
        //public float RatingValue { get; set; }
        public double Fees { get; set; }
        public int SpecializationId { get; set; }
        // public DoctorAccountStatus Status { get; set; } = DoctorAccountStatus.Pending;
        public DoctorService Service { get; set; }
        public string location { get; set; } // Assuming this is a string representation of the location
        public string DetailedAddress { get; set; }

        public int CityId { get; set; } // Assuming this is a string representation of the city
        public string Gender { get; set; } // Assuming this is a string representation


    }
}

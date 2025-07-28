using _01_DataAccessLayer.Enums;

namespace _02_BusinessLogicLayer.DTOs.DoctorDTOs
{
    public class DoctorGetDTO
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }  // from AppUser
        public string Email { get; set; }     // from AppUser
        public string PhoneNumber { get; set; }  // from AppUser
        public double Balance { get; set; }
        public int? ExpYears { get; set; }
        public string AboutMe { get; set; }
        public float RatingValue { get; set; }
        public double Fees { get; set; }
        public string Service { get; set; }
        public DoctorService ServiceId { get; set; } // Enum for service type
        public DoctorAccountStatus Status { get; set; }
        public string Specialization { get; set; }
        public int? SpecializationId { get; set; }

    }
}

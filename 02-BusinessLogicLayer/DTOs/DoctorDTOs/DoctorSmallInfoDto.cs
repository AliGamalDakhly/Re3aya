using _01_DataAccessLayer.Enums;

namespace _02_BusinessLogicLayer.DTOs.DoctorDTOs
{
    public class DoctorSmallInfoDto
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; } // from AppUser
        public string ProfilePictureUrl { get; set; } // URL to the doctor's profile picture
        public DoctorAccountStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } // Date when the doctor was created in the system

    }
}

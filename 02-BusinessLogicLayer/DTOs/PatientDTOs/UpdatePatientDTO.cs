using _01_DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class UpdatePatientDTO
    {
        public string UserId { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }
    }
}

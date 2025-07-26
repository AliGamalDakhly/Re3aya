using _01_DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.AccountDTOs
{
    public class DoctorRegisterDTO
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public double Balance { get; set; }
        public int? ExpYears { get; set; }
        public string AboutMe { get; set; }
        public float RatingValue { get; set; }
        public double Fees { get; set; }
        public DoctorService Service { get; set; }
        public string NationalId { get; set; }
        public int? SpecializationId { get; set; } = null;
        public string AppUserId { get; set; }
    }
}

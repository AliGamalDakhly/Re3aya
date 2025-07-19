using _01_DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DoctorAccountStatus Status { get; set; }
        public string Specialization { get; set; }

    }
}

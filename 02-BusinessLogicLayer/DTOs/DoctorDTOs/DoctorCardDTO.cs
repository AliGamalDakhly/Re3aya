using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.DoctorDTOs
{
    public class DoctorCardDTO
    {
        public int DoctorId { get; set; }

        // fetch this From AppUser
        public string FullName { get; set; }  
        public int? ExpYears { get; set; }   
        public float RatingValue { get; set; }
        public double Fees { get; set; }
        public string Service { get; set; }
        public int WatingTime { get; set; } = 30;  // in minutes
        public string ProfileImageUrl { get; set; } // URL to the doctor's profile image


        // Related Entites
        public string Specialization { get; set; }
        public List<String> Addresses { get; set; }
        
        
    }
}

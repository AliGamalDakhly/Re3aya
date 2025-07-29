using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Enums;

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
        public string DoctorService { get; set; }
        public string Gender { get; set; }
        public int WatingTime { get; set; } = 30;  // in minutes


        // Related Entites
        public string Specialization { get; set; }
        public List<String> Addresses { get; set; }
        public string ProfilePictureUrl { get; set; } // URL to the doctor's profile picture

        public int GovernemntId { get; set; } // ID of the government where the doctor is located
        public int SpecializationId { get; set; } // ID of the doctor's specialization
        

    }
}

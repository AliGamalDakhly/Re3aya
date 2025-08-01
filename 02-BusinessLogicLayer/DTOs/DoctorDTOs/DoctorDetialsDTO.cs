using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.DoctorDTOs
{
    public class DoctorDetialsDTO
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public int? ExpYears { get; set; }
        public float RatingValue { get; set; }
        public double Fees { get; set; }
        public string Service { get; set; }
        public string About { get; set; }
        public int WaitingTime { get; set; } = 30;  // in minutes
        // Related Entites
        public string Specialization { get; set; }
        public List<String> Addresses { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Location { get; set; }
    }
}

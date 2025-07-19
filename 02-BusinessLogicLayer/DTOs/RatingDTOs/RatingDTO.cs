using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.RatingDTOs
{
    public class RatingDTO
    {
        public float RatingValue { get; set; }
        public string? Comment { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}

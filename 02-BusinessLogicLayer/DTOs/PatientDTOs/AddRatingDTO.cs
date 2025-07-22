using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class AddRatingDTO
    {
        //public int PatientId { get; set; }   //remove from Dto becouse security to prevent any one edit or add rating
        public int DoctorId { get; set; }
        public float RatingValue { get; set; }
        public string? Comment { get; set; }
    }
}

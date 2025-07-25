using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class UpdateRatingDTO
    {
        public int RatingId { get; set; }
        public float RatingValue { get; set; }
        public string? Comment { get; set; }
    }
}

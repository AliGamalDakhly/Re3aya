using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DataAccessLayer.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public float RatingValue { get; set; }
        public string? Comment { get; set; }


        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }    
    }
}

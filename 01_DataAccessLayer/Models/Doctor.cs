using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Enums;

namespace _01_DataAccessLayer.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        
        public double Balance { get; set; }
        public int? ExpYears { get; set; }
        public string AboutMe { get; set; }
        public float RatingValue { get; set; }
        public double Fees { get; set; }
        public DoctorAccountStatus Status { get; set; } = DoctorAccountStatus.Pending;
        public DoctorService Service { get; set; }
        public string NationalId { get; set; }


        public int SpecializationId { get; set; }
        public string AppUserId { get; set; }



        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        [ForeignKey("SpecializationId")]
        public Specialization Specialization { get; set; }
        public virtual List<Address> Addresses { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public virtual List<Document> Documents { get; set; } = new List<Document>();
        public virtual List<DoctorTimeSlot> DoctorTimeSlots { get; set; } 

    }
}

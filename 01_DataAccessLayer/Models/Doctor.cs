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

        public double Balance { get; set; } = 0;
        public int? ExpYears { get; set; } = 0;
        public string? AboutMe { get; set; } = null;
        public float RatingValue { get; set; } = 0;
        public double Fees { get; set; } = 100;
        public DoctorAccountStatus Status { get; set; } = DoctorAccountStatus.Pending;
        public DoctorService Service { get; set; } = DoctorService.OnlineConsultion;
        public string? NationalId { get; set; } = null;


        public int? SpecializationId { get; set; } = null;
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

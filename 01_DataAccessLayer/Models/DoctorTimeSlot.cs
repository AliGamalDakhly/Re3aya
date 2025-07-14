using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DataAccessLayer.Models
{
    public class DoctorTimeSlot
    {
        public int DoctorTimeSlotId { get; set; }
        public int DoctorId { get; set; }
        public int TimeSlotId { get; set; }



        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("TimeSlotId")]
        public virtual TimeSlot TimeSlot { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}

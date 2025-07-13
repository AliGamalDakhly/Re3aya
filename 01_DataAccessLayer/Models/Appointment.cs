using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Enums;

namespace _01_DataAccessLayer.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Booked;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; }


        public int PatientId { get; set; }
        public int DoctorTimeSlotId { get; set; }
        public int PaymentId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [ForeignKey("DoctorTimeSlotId")]
        public DoctorTimeSlot DoctorTimeSlot { get; set; }

        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }

    }
}

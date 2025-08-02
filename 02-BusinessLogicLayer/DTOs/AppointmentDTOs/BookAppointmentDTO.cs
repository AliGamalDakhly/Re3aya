using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Enums;

namespace _02_BusinessLogicLayer.DTOs.AppointmentDTOs
{
    public class BookAppointment
    {

        public int PatientId { get; set; }

        public AppointmentStatus Status { get; set; }
        public int PaymentId { get; set; }
        public int DoctorId { get; set; }
        public int TimeSlotId { get; set; }
        public string Notes { get; set; }
        public int AppointmentId { get; set; }
        public int DoctorTimeSlotId { get; set; }

        public string? VedioCallUrl { get; set; }


    }
}

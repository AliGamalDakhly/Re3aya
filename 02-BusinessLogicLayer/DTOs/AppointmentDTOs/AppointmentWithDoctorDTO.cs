using _01_DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.AppointmentDTOs
{
    public class AppointmentWithDoctorDTO
    {
        public int AppointmentId { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Notes { get; set; }
        public string? VideoCallUrl { get; set; }

        // Patient Info
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }

        // Time Slot Info
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public WeekDays DayOfWeek { get; set; }
    }
}

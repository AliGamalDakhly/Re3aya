using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class AppointmentResponseDTO
    {
        public int AppointmentId { get; set; }

        public string DoctorName { get; set; }

        public string SpecializationName { get; set; } 

        public string StartTime { get; set; } 
        public string EndTime { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; } 
    }
}

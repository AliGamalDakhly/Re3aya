using _01_DataAccessLayer.Enums;
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
        public DateTime CreatedAt { get; set; }
        public AppointmentStatus Status { get; set; }
        public string DoctorName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Notes { get; set; }
    }
}

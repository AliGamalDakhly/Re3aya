using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class BookAppointmentDTO
    {
        //public int PatientId { get; set; }     //I will take it from his login to security
        public int DoctorTimeSlotId { get; set; }
        public string? Notes { get; set; }
    }
}

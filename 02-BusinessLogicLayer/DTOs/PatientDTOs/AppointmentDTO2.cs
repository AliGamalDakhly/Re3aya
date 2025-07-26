using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class AppointmentDTO2
    {
        public string Status { get; set; }= "Booked"; 
        public int PaymentId { get; set; }
        public int DoctorTimeSlotId { get; set; }
        public string Notes { get; set; }
    }
}

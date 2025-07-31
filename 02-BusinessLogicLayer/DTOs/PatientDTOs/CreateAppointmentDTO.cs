using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class CreateAppointmentDTO
    {
        public int DoctorTimeSlotId { get; set; }
        public string Notes { get; set; }
    }
}

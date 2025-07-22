using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class CancelAppointmentDTO
    {
        public int AppointmentId { get; set; }
        //public int PatientId { get; set; }    //will take from Patient Login
    }
}

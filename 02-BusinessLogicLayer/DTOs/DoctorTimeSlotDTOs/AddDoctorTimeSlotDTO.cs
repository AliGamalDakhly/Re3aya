using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.DoctorTimeSlotDTOs
{
    public class AddDoctorTimeSlotDTO
    {
        public int DoctorTimeSlotId { get; set; }
        public int DoctorId { get; set; }
        public int TimeSlotId { get; set; }
        public bool IsAvailable = true;
    }
}

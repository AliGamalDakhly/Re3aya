using _01_DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.DoctorTimeSlot
{
    public class DoctorTimeSlotDTO
    {
        public int DoctorTimeSlotId { get; set; }
        public int DoctorId { get; set; }
        public int TimeSlotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DayOfWeek { get; set; }
        public bool IsAvailable { get; set; }
    }
}

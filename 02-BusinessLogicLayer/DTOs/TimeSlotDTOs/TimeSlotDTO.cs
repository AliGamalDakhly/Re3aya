using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.TimeSlotDTOs
{

    //DTO for transferring time slot data
    public class TimeSlotDTO
    {
        public int TimeSlotId { get; set; }     
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DayOfWeek { get; set; } // Assuming DayOfWeek is a string representing the day of the week
    }
}

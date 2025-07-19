using _01_DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.TimeSlotDTOs
{
    //DTO for editing time slot data
    public class EditTimeSlotDTO
    {
        public int TimeSlotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public WeekDays DayOfWeek { get; set; } 
    }
}

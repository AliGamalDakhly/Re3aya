using _01_DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.AppointmentDTOs
{
    public class UpdateAppointmentStatusDTO
    {
        public AppointmentStatus Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlotDTOs;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IDoctorTimeSlotService
    {
        Task<List<AvailableDoctorTImeSlotDTO>> AvailableDoctorTimeSlots(int doctorId, DateOnly date);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlotDTOs;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IDoctorTimeSlotService
    {
        Task<AddDoctorTimeSlotDTO> AddDoctorTimeSlot(AddDoctorTimeSlotDTO doctorTimeSlotDTO);
        Task<List<AvailableDoctorTImeSlotDTO>> AvailableDoctorTimeSlots(int doctorId, DateOnly date);
        Task<List<AddDoctorTimeSlotDTO>> GetAllAsync(QueryOptions<DoctorTimeSlot>? options = null);
    }
}

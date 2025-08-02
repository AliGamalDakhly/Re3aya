using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlot;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlotDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IDoctorTimeSlotService
    {
        Task<DoctorTimeSlotDTO> AddDoctorTimeSlotAsync(DoctorTimeSlotCreateDTO dto);
        Task<bool> DeleteDoctorTimeSlotAsync(int doctorTimeSlotId);
        Task<bool> ActivateDoctorTimeSlotAsyncAsync(int doctorTimeSlotId);
        Task<bool> DeactivateDoctorTimeSlotAsync(int doctorTimeSlotId);
        Task<List<DoctorTimeSlotDTO>> GetAllDoctorTimeSlotsAsync(int doctorId);
        Task<List<DoctorTimeSlotDTO>> GetAllAsync(QueryOptions<DoctorTimeSlot>? options = null);
        Task<bool> UpadateDoctorTimeSlotAsync(DoctorTimeSlotCreateDTO doctorTimeSlotDTO, int id);
        Task<List<AvailableDoctorTImeSlotDTO>> AvailableDoctorTimeSlots(int doctorId, DateOnly date);
        Task<bool> HasAvailableTimeSlots(int doctorId, DateTime date);
        Task<bool> HasAvailableTimeSlots(int doctorId);
        Task<bool> UpadateDoctorTimeSlot(DoctorTimeSlotDTO doctorTimeSlotDTO, int id);


    }
}

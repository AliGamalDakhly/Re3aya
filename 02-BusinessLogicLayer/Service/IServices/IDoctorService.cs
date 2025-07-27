using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IDoctorService
    {
        //Task<DoctorGetDTO> AddDoctorAsync(DoctorRegisterDTO doctorDto);
        Task<bool> DeleteDoctorByIdAsync(int id);
        //Task<bool> DeleteDoctorAsync(Doctor doctor);
        Task<bool> ActivateDoctorAccountAsync(int doctorId);
        Task<bool> DeActivateDoctorAccountAsync(int doctorId);
        Task<DoctorGetDTO> UpdateDoctorAsync(int id, DoctorUpdateDTO doctorDto);
        Task<List<DoctorCardDTO>> GetAllAsync();
        Task<DoctorDetialsDTO> GetDoctorByIdAsync(int id);
        Task<int> CountDoctorsAsync();
        Task<bool> ExistsDoctorAsync(int id);

        Task<DoctorTimeSlot> AddDoctorTimeSlotAsync(DoctorTimeSlotDTO dto);
        Task<bool> DeleteDoctorTimeSlotAsync(int doctorTimeSlotId);
        Task<bool> DeactivateDoctorTimeSlotAsync(int doctorTimeSlotId);

        Task<string?> GetDoctorFullNameByIdAsync(int doctorId);


    }
}

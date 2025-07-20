using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
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
        Task<List<DoctorGetDTO>> GetAllAsync();
        Task<DoctorGetDTO> GetDoctorByIdAsync(int id);
        Task<int> CountDoctorsAsync();
        Task<bool> ExistsDoctorAsync(int id);

    }
}

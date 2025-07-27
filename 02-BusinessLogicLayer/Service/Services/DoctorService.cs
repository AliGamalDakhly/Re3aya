using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.AddressDTOs;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlot;
using _02_BusinessLogicLayer.DTOs.SpecailzationDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;
        private readonly ISpecializationService _specialzationService;

        public DoctorService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager,
            IMapper mapper, IAddressService addressService, ISpecializationService specializationService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _addressService = addressService;
            _specialzationService = specializationService;
        }




        public async Task<bool> ActivateDoctorAccountAsync(int doctorId)
        {
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(doctorId);
            if (doctor == null) return false;

            doctor.Status = DoctorAccountStatus.Approved;

            await _unitOfWork.Repository<Doctor, int>().UpdateAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }


        public async Task<int> CountDoctorsAsync()
        {
            return await _unitOfWork.Repository<Doctor, int>().CountAsync();
        }

        public async Task<bool> DeActivateDoctorAccountAsync(int doctorId)
        {
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(doctorId);
            if (doctor == null) return false;

            doctor.Status = DoctorAccountStatus.Deactivated;

            await _unitOfWork.Repository<Doctor, int>().UpdateAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteDoctorByIdAsync(int id)
        {
            var result = await _unitOfWork.Repository<Doctor, int>().DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> ExistsDoctorAsync(int id)
        {
            return await _unitOfWork.Repository<Doctor, int>().ExistsAsync(d => d.DoctorId == id);
        }

        public async Task<List<DoctorCardDTO>> GetAllAsync()
        {
            List<Doctor> doctors = await _unitOfWork.Repository<Doctor, int>().GetAllAsync(
                new QueryOptions<Doctor> { Includes = [d => d.Addresses, d => d.Specialization, d => d.AppUser] });


            List<DoctorCardDTO> doctorsDtos = _mapper.Map<List<DoctorCardDTO>>(doctors);

            return doctorsDtos;
        }

        //public async Task<List<DoctorGetDTO>> GetAllAsync()
        //{
        //    var doctors = await _unitOfWork.Repository<Doctor, int>().GetAllAsync();
        //    var result = new List<DoctorGetDTO>();

        //    foreach (var doctor in doctors)
        //    {
        //        var user = await _userManager.FindByIdAsync(doctor.AppUserId);
        //        var dto = _mapper.Map<DoctorGetDTO>(doctor);
        //        dto.FullName = user.FullName;
        //        dto.Email = user.Email;
        //        dto.PhoneNumber = user.PhoneNumber;
        //        result.Add(dto);
        //    }

        //    return result;
        //}


        public async Task<DoctorDetialsDTO> GetDoctorDetailsByIdAsync(int id)
        {
            List<Doctor> doctors = await _unitOfWork.Repository<Doctor, int>().
                GetAllAsync(new QueryOptions<Doctor>
                {
                    Filter = d => d.DoctorId == id,
                    Includes = [d => d.Addresses, d => d.Specialization, d => d.AppUser]
                });

            Doctor doctor = doctors.FirstOrDefault();

            if (doctor == null) return null;
 
            return _mapper.Map<DoctorDetialsDTO>(doctor);
        }

        public async Task<DoctorGetDTO> GetDoctorByIdAsync(int id)
        {
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(id);
            if (doctor == null) return null;

            var user = await _userManager.FindByIdAsync(doctor.AppUserId);

            var dto = _mapper.Map<DoctorGetDTO>(doctor);
            dto.FullName = user.FullName;
            dto.Email = user.Email;
            dto.PhoneNumber = user.PhoneNumber;

            return dto;
        }

        public async Task<DoctorGetDTO> UpdateDoctorAsync(int id, DoctorUpdateDTO doctorDto)
        {
            // Get the doctor entity by ID
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(id);
            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }

            // Map updated fields
            _mapper.Map(doctorDto, doctor); // only non-null fields overwrite entity props

            // Save changes
            await _unitOfWork.Repository<Doctor, int>().UpdateAsync(doctor);
            await _unitOfWork.CompleteAsync();

            // Get related AppUser for extra data
            var appUser = await _userManager.FindByIdAsync(doctor.AppUserId);

            // Map to output DTO
            var output = _mapper.Map<DoctorGetDTO>(doctor);
            output.FullName = appUser.FullName;
            output.Email = appUser.Email;
            output.PhoneNumber = appUser.PhoneNumber;

            return output;
        }

        public async Task<DoctorTimeSlot> AddDoctorTimeSlotAsync(DoctorTimeSlotDTO dto)
        {
            var timeSlot = new TimeSlot
            {
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                DayOfWeek = dto.DayOfWeek
            };

            await _unitOfWork.Repository<TimeSlot, int>().AddAsync(timeSlot);
            await _unitOfWork.CompleteAsync();

            var doctorTimeSlot = new DoctorTimeSlot
            {
                DoctorId = dto.DoctorId,
                TimeSlotId = timeSlot.TimeSlotId,
                IsAvailable = true
            };

            await _unitOfWork.Repository<DoctorTimeSlot, int>().AddAsync(doctorTimeSlot);
            await _unitOfWork.CompleteAsync();

            return doctorTimeSlot;
        }
        public async Task<bool> DeleteDoctorTimeSlotAsync(int doctorTimeSlotId)
        {
            var slot = await _unitOfWork.Repository<DoctorTimeSlot, int>().GetByIdAsync(doctorTimeSlotId);
            if (slot == null) return false;

            await _unitOfWork.Repository<DoctorTimeSlot, int>().DeleteAsync(slot);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> DeactivateDoctorTimeSlotAsync(int doctorTimeSlotId)
        {
            var slot = await _unitOfWork.Repository<DoctorTimeSlot, int>().GetByIdAsync(doctorTimeSlotId);
            if (slot == null) return false;

            slot.IsAvailable = false;
            await _unitOfWork.Repository<DoctorTimeSlot, int>().UpdateAsync(slot);
            await _unitOfWork.CompleteAsync();
            return true;
        }



        public async Task<string?> GetDoctorFullNameByIdAsync(int doctorId)
        {
            var doctor = await _unitOfWork.Repository<Doctor, int>().GetByIdAsync(doctorId);
            if (doctor == null)
                return null;

            var appUser = await _userManager.FindByIdAsync(doctor.AppUserId);
            if (appUser == null)
                return null;

            return appUser.FullName;
        }



    }
}

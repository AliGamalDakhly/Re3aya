﻿using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
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

        public DoctorService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
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

        public async Task<DoctorGetDTO> AddDoctorAsync(DoctorRegisterDTO doctorDto)
        {
            var user = new AppUser
            {
                UserName = doctorDto.UserName,
                FullName = doctorDto.FullName,
                Email = doctorDto.Email,
                PhoneNumber = doctorDto.PhoneNumber,
                Gender = doctorDto.Gender,
                DateOfBirth = doctorDto.DateOfBirth
            };

            var result = await _userManager.CreateAsync(user, doctorDto.Password);
            if (!result.Succeeded)
                throw new Exception("Failed to create AppUser: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            var doctor = _mapper.Map<Doctor>(doctorDto);
            doctor.AppUserId = user.Id;

            await _unitOfWork.Repository<Doctor, int>().AddAsync(doctor);
            await _unitOfWork.CompleteAsync();

            // Map output DTO
            var output = _mapper.Map<DoctorGetDTO>(doctor);
            output.FullName = user.FullName;
            output.Email = user.Email;
            output.PhoneNumber = user.PhoneNumber;

            return output;
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

        public async Task<List<DoctorGetDTO>> GetAllAsync()
        {
            var doctors = await _unitOfWork.Repository<Doctor, int>().GetAllAsync();
            var result = new List<DoctorGetDTO>();

            foreach (var doctor in doctors)
            {
                var user = await _userManager.FindByIdAsync(doctor.AppUserId);
                var dto = _mapper.Map<DoctorGetDTO>(doctor);
                dto.FullName = user.FullName;
                dto.Email = user.Email;
                dto.PhoneNumber = user.PhoneNumber;
                result.Add(dto);
            }

            return result;
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
    }
}

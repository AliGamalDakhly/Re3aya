using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class PatientService : IPatientService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Patient, int> _patientRepo;
        private readonly IMapper _mapper;

        // Constructor injection for UnitOfWork and Mapper
        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _patientRepo = _unitOfWork.Repository<Patient, int>();
        }


        //add a new patient
        public async Task<PatientDTO> AddPatientAsync(PatientDTO patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            await _patientRepo.AddAsync(patient);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<PatientDTO>(patient);
        }
        //public async Task<PatientDTO> AddPatientAsync(PatientCreateDTO dto)
        //{
        //    var patient = _mapper.Map<Patient>(dto);
        //    await _unitOfWork.Repository<Patient, int>().AddAsync(patient);
        //    await _unitOfWork.CompleteAsync();

        //    return _mapper.Map<PatientDTO>(patient);
        //}


        // Update an existing patient by ID
        public async Task<PatientDTO> UpdatePatientAsync(int id, PatientDTO patientDto)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
            if (patient == null)
                throw new Exception("Patient not found");

            _mapper.Map(patientDto, patient); 
            _patientRepo.Update(patient);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<PatientDTO>(patient);
        }


        // Get a patient by ID
        public async Task<PatientDTO> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
            if (patient == null)
                throw new Exception("Patient not found");

            return _mapper.Map<PatientDTO>(patient);
        }


        // Get all patients
        public async Task<List<PatientDTO>> GetAllPatientsAsync()
        {
            var patients = await _patientRepo.GetAllAsync();
            return _mapper.Map<List<PatientDTO>>(patients);
        }


        // Get a patient by name
        public async Task<PatientDTO> GetPatientByNameAsync(string name)
        {
            var result = await _patientRepo.GetAllAsync(new()
            {
                Filter = p => p.AppUser.FullName.Contains(name)
            });

            var patient = result.FirstOrDefault();
            if (patient == null)
                throw new Exception("Patient not found");

            return _mapper.Map<PatientDTO>(patient);
        }


        // Get a patient by user ID this from APP User Not from Patient
        public async Task<PatientDTO> GetPatientByUserIdAsync(string userId)
        {
            var result = await _patientRepo.GetAllAsync(new()
            {
                Filter = p => p.AppUserId == userId
            });

            var patient = result.FirstOrDefault();
            if (patient == null)
                throw new Exception("Patient not found");

            return _mapper.Map<PatientDTO>(patient);
        }

        // Delete a patient by ID
        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
            if (patient == null)
                return false;

            _patientRepo.Delete(patient);
            await _unitOfWork.CompleteAsync();
            return true;
        }


        // Check if a patient exists by user id
        public async Task<bool> IsPatientExist(string userId)
        {
            return await _patientRepo.ExistsAsync(p => p.AppUserId == userId);
        }
    }



    //*************************************************************************//

    //we may add more methods in the future

}


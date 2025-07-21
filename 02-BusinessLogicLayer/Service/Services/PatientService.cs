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
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Patient, int> _patientRepository;
        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _patientRepository = _unitOfWork.Repository<Patient, int>();
        }

        public async Task<int> CountAsync(Expression<Func<Patient, bool>>? filter = null)
        {
            return await _patientRepository.CountAsync(filter);
        }

        public async Task<bool> DeleteAsync(int patientId)
        {
            var patient = await _patientRepository.GetByIdAsync(patientId);
            if (patient == null)
            {
                return false; // Patient not found
            }
            _patientRepository.Delete(patient);
            await _unitOfWork.CompleteAsync();
            return true; // Patient deleted successfully

        }

        public async Task<bool> ExistsAsync(Expression<Func<Patient, bool>> predicate)
        {
            return await _patientRepository.ExistsAsync(predicate);
        }

        public async Task<List<PatientDTO>> GetAllAsync(QueryOptions<Patient> options)
        {
            var patients = _patientRepository.GetAll(options);
            var patientDtos = _mapper.Map<List<PatientDTO>>(patients);
            return patientDtos;



        }

        public async Task<PatientDTO> GetByIdAsync(int patientId)
        {
            var patient = await _patientRepository.GetByIdAsync(patientId);
            return _mapper.Map<PatientDTO>(patient);
          
        }

        //not need this 
        //public async Task<PatientDTO> GetPatientProfileAsync(int userId)
        //{
        //    var patient = await _patientRepository.GetByIdAsync(userId);
        //    return _mapper.Map<PatientDTO>(patient);

        //}

   
        //add
        public async Task<PatientDTO> RegisterAsync(PatientDTO dto)
        {
           var patient = _mapper.Map<Patient>(dto);
            _patientRepository.Add(patient);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task<bool> UpdateProfileAsync(UpdatePatientDTO dto, int Id)
        {

            var patient = await _patientRepository.GetByIdAsync(Id);
            if (patient == null)
            {
                return false; // Patient not found
            }
            
            _mapper.Map(dto, patient);
            _patientRepository.Update(patient);
            await _unitOfWork.CompleteAsync();
            return true; 


        }
    }
}

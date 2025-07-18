using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.SpecailzationDTOs;
using _02_BusinessLogicLayer.Service.IServices;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class SpecialzationService : ISpecializationService
    {
        // this will be injected by DI container
        private readonly IUnitOfWork _unitOfWork; 
        // this will be used to access the repository methods
        private readonly IGenericRepository<Specialization, int> _context; 

        public SpecialzationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = _unitOfWork.Repository<Specialization, int>();
        }

        public async Task<SpecializationDTO> AddAsync(SpecializationDTO specializationDTO)
        {
            // Use Auto Mapper to map DTO to Entity

            // add your required logic here
            //...
            var result =  await _context.AddAsync(specialization);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"

            // Use Auto Mapper to map Entity to DTO
            return result;
        }

        // Be Careful Here pls
        public async Task<bool> UpdateAsync(SpecializationDTO specializationDTO, int id)
        {
            // add your required logic here
            // ...
            // we first get the entity from DB.
            var existingSpecialization = await _context.GetByIdAsync(id);

            if (existingSpecialization == null)
                return false;
            //-------- dont forget to update all fields of your entity ---------
            existingSpecialization.Name = specializationDTO.Name;
            existingSpecialization.Description = specializationDTO.Description;

            await _context.UpdateAsync(existingSpecialization);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<SpecializationDTO> GetByIdAsync(int id)
        {
            Specialization specialization =  await _context.GetByIdAsync(id);
            // Use Auto Mapper
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            bool res = await _context.DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return res;
        }

        public async Task<bool> DeleteAsync(SpecializationDTO specializationDTO)
        {
            // Use Auto Mapper to map DTO to Entity
            bool res = await _context.DeleteAsync(specialization);
            await _unitOfWork.CompleteAsync();
            return res;
        }

        public async Task<int> CountAsync(Expression<Func<Specialization, bool>>? filter = null)
        {
            return await _context.CountAsync(filter);
        }

        public async Task<bool> ExistsAsync(Expression<Func<Specialization, bool>> predicate)
        {
            return await _context.ExistsAsync(predicate);
           
        }

        public async Task<List<SpecializationDTO>> GetAllAsync(QueryOptions<SpecializationDTO> options)
        {
            // Use Auto Mapper to map Entity to DTO
            return await _context.GetAllAsync(options);
        }
    }
}

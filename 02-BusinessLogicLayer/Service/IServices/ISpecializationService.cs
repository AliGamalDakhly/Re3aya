using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.SpecailzationDTOs;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface ISpecializationService
    {
        Task<SpecializationDTO> AddAsync(SpecializationDTO specializationDTO);
        Task<bool> UpdateAsync(SpecializationDTO specializationDTO, int id);
        Task<SpecializationDTO> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);// change int with primary key type of your entity
        Task<bool> DeleteAsync(SpecializationDTO specializationDTO);
        Task<int> CountAsync(Expression<Func<Specialization, bool>>? filter = null);
        Task<bool> ExistsAsync(Expression<Func<Specialization, bool>> predicate);
        Task<List<SpecializationDTO>> GetAllAsync(QueryOptions<SpecializationDTO>? options = null);
    }
}

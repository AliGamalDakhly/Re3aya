using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface ISpecializationService
    {
        Task<Specialization> AddAsync(Specialization specialization);
        Task<bool> UpdateAsync(Specialization specialization);
        Task<Specialization> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);// change int with primary key type of your entity
        Task<bool> DeleteAsync(Specialization specialization);
        Task<int> CountAsync(Expression<Func<Specialization, bool>>? filter = null);
        Task<bool> ExistsAsync(Expression<Func<Specialization, bool>> predicate);
        Task<List<Specialization>> GetAllAsync(QueryOptions<Specialization>? options = null);
    }
}

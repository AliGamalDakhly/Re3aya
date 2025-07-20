using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.TimeSlotDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface ITimeSlotService
    {
        Task<TimeSlotDTO> AddAsync(CreateTimeSlotDTO dto);       // I will use CreateDTO
        Task<bool> UpdateAsync(EditTimeSlotDTO dto);             // using EditDTO
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> DeleteAsync(EditTimeSlotDTO dto);
        Task<TimeSlotDTO> GetByIdAsync(int id);                  // using TimeSloteDTO for View
        Task<List<TimeSlotDTO>> GetAllAsync(QueryOptions<TimeSlot> options);
        Task<bool> ExistsAsync(Expression<Func<TimeSlot, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TimeSlot, bool>>? filter = null);
    }
}

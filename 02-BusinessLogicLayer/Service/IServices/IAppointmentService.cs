using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.AppointmentDTOs;
using System.Linq.Expressions;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IAppointmentService
    {
        Task<AppointmentDTO> AddAppointmentAsync(AppointmentDTO appointmentDto);
        Task<AppointmentDTO> GetAppointmentByIdAsync(int appointmentId);

        Task<bool> ConfirmAppointmentAsync(int appointmentId);
        Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync();

        Task<bool> UpdateAppointmentAsync(AppointmentDTO appointment, int appointmentId);

        Task<bool> DeleteAppointmentByIdAsync(int appointmentId);
        Task<bool> DeleteAppointmentAsync(AppointmentDTO appointmentDTO, int id);

        Task<int> CountAsync(Expression<Func<Appointment, bool>>? filter = null);
        Task<bool> ExistsAsync(Expression<Func<Appointment, bool>> predicate);
    }
}

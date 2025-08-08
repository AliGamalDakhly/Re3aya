using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.AppointmentDTOs;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using System.Linq.Expressions;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IAppointmentService
    {
        Task<string> CreateRoomForAppointment(int appointmentId);
        Task<string> JoinRoom(int appointmentId, string appUserId);
        Task<AppointmentDTO> AddAppointmentAsync(AppointmentDTO appointmentDto);
        Task<BookAppointment> BookAppointmentAsync(BookAppointment bookAppointmentDto);
        Task<AppointmentDTO> GetAppointmentByIdAsync(int appointmentId);

        Task<bool> ConfirmAppointmentAsync(int appointmentId);
        Task<List<AppointmentDTO>> GetAllAppointmentsAsync(QueryOptions<Appointment>? options = null);

        Task<bool> UpdateAppointmentAsync(AppointmentDTO appointment, int appointmentId);

        Task<bool> DeleteAppointmentByIdAsync(int appointmentId);
        Task<bool> DeleteAppointmentAsync(AppointmentDTO appointmentDTO, int id);

        Task<int> CountAsync(Expression<Func<Appointment, bool>>? filter = null);
        Task<bool> ExistsAsync(Expression<Func<Appointment, bool>> predicate);
        Task<string> AddNotesAsync(string notes, int appointmentId, string appUserId);


        Task<List<AppointmentWithPatientDTO>> GetAppointmentsByPatientIdAsync(int patientId);

    }
}

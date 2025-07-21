using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.AppointmentDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using System.Linq.Expressions;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class AppointmentService : IAppointmentService
    {

        private readonly IUnitOfWork _unitOfWork;
        // this will be used to access the repository methods
        private readonly IGenericRepository<Appointment, int> _context;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _context = _unitOfWork.Repository<Appointment, int>();
            _mapper = mapper;

        }
        public async Task<AppointmentDTO> AddAppointmentAsync(AppointmentDTO appointmentDto)
        {

            var appointmentEntity = _mapper.Map<Appointment>(appointmentDto);

            var addedAppointment = await _context.AddAsync(appointmentEntity);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"

            return _mapper.Map<AppointmentDTO>(addedAppointment);
        }

        public async Task<bool> ConfirmAppointmentAsync(int appointmentId)
        {

            var appointment = await _context.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return false; // or throw an exception
            }

            appointment.Status = AppointmentStatus.Confirmed;
            var updatedAppointment = await _context.UpdateAsync(appointment);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"
            return true;


        }

        public async Task<int> CountAsync(Expression<Func<Appointment, bool>>? filter = null)
        {

            return await _context.CountAsync(filter);

        }

        public async Task<bool> DeleteAppointmentAsync(AppointmentDTO appointmentDTO, int id)
        {
            var appointment = await _context.GetByIdAsync(id);
            if (appointment == null)
            {
                return false; // or throw an exception
            }

            var result = await _context.DeleteAsync(appointment);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"
            return result;
        }

        public async Task<bool> DeleteAppointmentByIdAsync(int appointmentId)
        {

            var appointment = await _context.GetByIdAsync(appointmentId);

            var result = await _context.DeleteAsync(appointment);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"
            return result;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Appointment, bool>> predicate)
        {
            return await _context.ExistsAsync(predicate);

        }

        public async Task<List<AppointmentDTO>> GetAllAppointmentsAsync(QueryOptions<Appointment>? options)
        {
            List<Appointment> appointments = await _context.GetAllAsync(options);
            List<AppointmentDTO> appointmentDTOs = _mapper.Map<List<AppointmentDTO>>(appointments);
            return appointmentDTOs;
        }

        public async Task<AppointmentDTO> GetAppointmentByIdAsync(int appointmentId)
        {

            var appointment = await _context.GetByIdAsync(appointmentId);

            return _mapper.Map<AppointmentDTO>(appointment);

        }

        public async Task<bool> UpdateAppointmentAsync(AppointmentDTO appointment, int appointmentId)
        {
            var existingAppointment = await _context.GetByIdAsync(appointmentId);
            if (existingAppointment == null)
            {
                return false; // or throw an exception
            }
            // Map the updated properties from the DTO to the existing entity
            _mapper.Map(appointment, existingAppointment);
            var updatedAppointment = await _context.UpdateAsync(existingAppointment);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"
            return updatedAppointment != null;

        }
    }
}

using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using System.Linq.Expressions;
using CancelAppointmentDTO = _02_BusinessLogicLayer.DTOs.PatientDTOs.CancelAppointmentDTO;
using AppointmentDTO = _02_BusinessLogicLayer.DTOs.AppointmentDTOs.AppointmentDTO;
using _01_DataAccessLayer.Repository;

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
            if (patient == null) return false;

            _patientRepository.Delete(patient);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteProfileAsync(string appUserId)
        {
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == appUserId);
            if (patient == null) return false;
            _patientRepository.Delete(patient);
            return await _unitOfWork.CompleteAsync() > 0;
        }
        public async Task<bool> ExistsAsync(Expression<Func<Patient, bool>> predicate)
        {
            return await _patientRepository.ExistsAsync(predicate);
        }

        public async Task<List<PatientDTO>> GetAllAsync(QueryOptions<Patient> options)
        {
            var patients = _patientRepository.GetAll(options);
            return _mapper.Map<List<PatientDTO>>(patients);
        }

        public async Task<PatientDTO> GetByIdAsync(int patientId)
        {
            var patient = await _patientRepository.GetByIdAsync(patientId);
            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task<PatientDTO> RegisterAsync(PatientDTO dto)
        {
            var patient = _mapper.Map<Patient>(dto);
            _patientRepository.Add(patient);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task<PatientDetailsDTO?> GetProfileByIdAsync(string appUserId)
        {
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == appUserId);
            return patient == null ? null : _mapper.Map<PatientDetailsDTO>(patient);
        }



        public async Task<bool> UpdateProfileAsync(UpdatePatientDTO dto, string userId)
        {

            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == userId);
            if (patient == null) return false;
            _mapper.Map(dto, patient);
            _patientRepository.Update(patient);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> AddRatingAsync(AddRatingDTO dto, string userId)
        {
            var ratingRepo = _unitOfWork.Repository<Rating, int>();
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == userId);
            if (patient == null) return false;

            var rating = _mapper.Map<Rating>(dto);
            rating.PatientId = patient.PatientId;
            ratingRepo.Add(rating);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> UpdateRatingAsync(UpdateRatingDTO dto, string userId)
        {
            var ratingRepo = _unitOfWork.Repository<Rating, int>();
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == userId);
            if (patient == null) return false;

            var rating = await ratingRepo.GetByIdAsync(dto.RatingId);
            if (rating == null || rating.PatientId != patient.PatientId) return false;

            _mapper.Map(dto, rating);
            ratingRepo.Update(rating);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> BookAppointmentAsync(BookAppointmentDTO dto, string appUserId)
        {
            // AppUserId
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == appUserId);
            if (patient == null) return false;

            // PaymentDTO
            var payment = _mapper.Map<Payment>(dto.Payment);
            var paymentRepo = _unitOfWork.Repository<Payment, int>();
            paymentRepo.Add(payment);

            await _unitOfWork.CompleteAsync();

             
            var appointment = _mapper.Map<Appointment>(dto);
            appointment.PatientId = patient.PatientId;
            appointment.PaymentId = payment.PaymentId;

            var appointmentRepo = _unitOfWork.Repository<Appointment, int>();
            appointmentRepo.Add(appointment);

            // 
            return await _unitOfWork.CompleteAsync() > 0;
        }


        public async Task<bool> CancelAppointmentAsync(CancelAppointmentDTO dto, string appUserId)
        {
            var appointmentRepo = _unitOfWork.Repository<Appointment, int>();
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == appUserId);
            if (patient == null) return false;

            var appointment = await appointmentRepo.GetFirstOrDefaultAsync(
                a => a.AppointmentId == dto.AppointmentId && a.PatientId == patient.PatientId,
                a => a.DoctorTimeSlot,
                a => a.DoctorTimeSlot.TimeSlot
            );

            if (appointment == null) return false;

            var now = DateTime.UtcNow;
            var startTime = appointment.DoctorTimeSlot.TimeSlot.StartTime;

            if (startTime <= now || (startTime - now).TotalHours < 24)
                return false;

            appointment.Status = AppointmentStatus.Cancelled;
            appointmentRepo.Update(appointment);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<List<AppointmentDTO>> GetAppointmentsAsync(string appUserId)
        {
            var appointmentRepo = _unitOfWork.Repository<Appointment, int>();
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == appUserId);
            if (patient == null) return new List<AppointmentDTO>();

            var appointments = await appointmentRepo.GetFirstOrDefaultAsync (
                a => a.PatientId == patient.PatientId,
                a => a.DoctorTimeSlot,
                a => a.DoctorTimeSlot.TimeSlot,
                a => a.DoctorTimeSlot.Doctor
            );

            return _mapper.Map<List<AppointmentDTO>>(appointments);
        }
    }
}

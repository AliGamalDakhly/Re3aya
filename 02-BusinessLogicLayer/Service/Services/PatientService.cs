using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using _02_BusinessLogicLayer.DTOs.RatingDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using AppointmentDTO = _02_BusinessLogicLayer.DTOs.AppointmentDTOs.AppointmentDTO;
using CancelAppointmentDTO = _02_BusinessLogicLayer.DTOs.PatientDTOs.CancelAppointmentDTO;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Patient, int> _patientRepository;
        private readonly IRatingService _ratingService;
        private readonly IDoctorService _doctorService;
        private readonly UserManager<AppUser> _userManager;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper,
                UserManager<AppUser> userManager,
                IRatingService ratingService, IDoctorService doctorService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _patientRepository = _unitOfWork.Repository<Patient, int>();
            _ratingService = ratingService;
            _doctorService = doctorService;
            _userManager = userManager;
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

        public async Task<List<PatientDTO>> GetAllAsync()
        {
            List<Patient> patients = await _patientRepository.GetAllAsync(new QueryOptions<Patient>
            {
                Includes = [p => p.AppUser]
            });

            return _mapper.Map<List<PatientDTO>>(patients);
        }

        public async Task<PatientDTO> GetByIdAsync(int patientId)
        {
            List<Patient> patients = await _patientRepository.GetAllAsync(new QueryOptions<Patient>
            {
                Filter = p => p.PatientId == patientId,
                Includes = [p => p.AppUser]
            });

            Patient? patient = patients?.FirstOrDefault();

            if (patient == null)
                throw new KeyNotFoundException($"No patient found with ID {patientId}");

            var patientDTO = _mapper.Map<PatientDTO>(patient);
            patientDTO.UserId = patient.AppUser?.Id; // Ensure UserId is set from AppUser if available
            return patientDTO;
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

            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == userId, p => p.AppUser);
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

            bool exist =  await _ratingService.ExistsAsync(r => r.PatientId == rating.PatientId
                            && r.DoctorId == rating.DoctorId);


            if (exist) 
            {
                List<Rating> ratings = await ratingRepo.GetAllAsync(
                    new QueryOptions<Rating> {
                        Filter = r => r.PatientId == rating.PatientId 
                            &&  r.DoctorId == rating.DoctorId
                    });

                ratings.FirstOrDefault().RatingValue = rating.RatingValue;
                ratings.FirstOrDefault().Comment = rating.Comment;
                ratingRepo.Update(ratings.FirstOrDefault());
            }
            else
            {
                ratingRepo.Add(rating);
            }
            await _unitOfWork.CompleteAsync();

            await _doctorService.UpdateDoctorRating(rating.DoctorId);
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
            var appointmentRepo = _unitOfWork.Repository<Appointment, int>();
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == appUserId);
            if (patient == null) return false;

            var appointment = _mapper.Map<Appointment>(dto);
            appointment.PatientId = patient.PatientId;
            appointmentRepo.Add(appointment);
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
            appointment.DoctorTimeSlot.IsAvailable = true; 
            appointmentRepo.Update(appointment);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<List<AppointmentResponseDTO>> GetAppointmentsAsync(string appUserId)
        {
           
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == appUserId);

            if (patient == null)
                return new List<AppointmentResponseDTO>();

            var options = new QueryOptions<Appointment>
            {
                Filter = a => a.PatientId == patient.PatientId,
                Includes = new Expression<Func<Appointment, object>>[]
                {
                    a => a.DoctorTimeSlot,
                    a => a.DoctorTimeSlot.TimeSlot,
                    a => a.DoctorTimeSlot.Doctor,
                    a => a.DoctorTimeSlot.Doctor.Addresses,
                    a => a.DoctorTimeSlot.Doctor.AppUser,
                    a => a.DoctorTimeSlot.Doctor.Specialization
                }
            };

        
            var appointmentRepo = _unitOfWork.Repository<Appointment, int>();
            var appointments = await appointmentRepo.GetAllAsync(options);

        
            return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
        }



        public async Task<List<AppointmentResponseDTO>> GetUpcomingAppointmentsAsync(string appUserId)
        {
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == appUserId);
            if (patient == null) return new List<AppointmentResponseDTO>();


            var options = new QueryOptions<Appointment>
            {
                Filter = a =>
                    a.PatientId == patient.PatientId &&
                    a.DoctorTimeSlot.TimeSlot.StartTime > DateTime.UtcNow.Date &&
                    a.Status == AppointmentStatus.Confirmed,
                Includes = new Expression<Func<Appointment, object>>[]
                {
                     a => a.DoctorTimeSlot,
                     a => a.DoctorTimeSlot.TimeSlot,
                     a => a.DoctorTimeSlot.Doctor,
                     
                     a => a.DoctorTimeSlot.Doctor.AppUser,
                     a => a.DoctorTimeSlot.Doctor.Specialization
                }
            };


            var appointments = await _unitOfWork.Repository<Appointment, int>().GetAllAsync(options);
            return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
        }

        public async Task<List<AppointmentResponseDTO>> GetPastAppointmentsAsync(string appUserId)
        {
            var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.AppUserId == appUserId);
            if (patient == null) return new List<AppointmentResponseDTO>();

            var options = new QueryOptions<Appointment>
            {
                Filter = a =>
                    a.PatientId == patient.PatientId &&
                    (a.DoctorTimeSlot.TimeSlot.StartTime <= DateTime.UtcNow.Date ||
                     a.Status == AppointmentStatus.Finished ||
                     a.Status == AppointmentStatus.Cancelled),
                Includes = new Expression<Func<Appointment, object>>[]
                {
                   a => a.DoctorTimeSlot,
                   a => a.DoctorTimeSlot.TimeSlot,
                   a => a.DoctorTimeSlot.Doctor,
                   a => a.DoctorTimeSlot.Doctor.AppUser,
                   a => a.DoctorTimeSlot.Doctor.Specialization
                }
            };

            var appointments = await _unitOfWork.Repository<Appointment, int>().GetAllAsync(options);
            return _mapper.Map<List<AppointmentResponseDTO>>(appointments);
        }

        public async Task<bool> ToggleAccountLock(int patientId)
        {
            Patient patient = await _patientRepository.GetByIdAsync(patientId);
            AppUser? appUser = await _userManager.FindByIdAsync(patient.AppUserId);

            if (appUser == null)
                throw new ArgumentException("User Not FOund");

            if (appUser.LockoutEnd != null && appUser.LockoutEnd > DateTime.UtcNow)
            {
                // Unlock
                appUser.LockoutEnd = null;
            }
            else
            {
                // Lock for 100 years
                appUser.LockoutEnd = DateTime.UtcNow.AddYears(100);
            }

            var result = await _userManager.UpdateAsync(appUser);

            return result.Succeeded;
        }

    }
}

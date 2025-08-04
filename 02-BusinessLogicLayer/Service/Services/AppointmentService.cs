using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.AppointmentDTOs;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlotDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using System.Linq.Expressions;
using _01_DataAccessLayer.Enums;
using _02_BusinessLogicLayer.DTOs.TimeSlotDTOs;
using Microsoft.AspNetCore.Identity;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class AppointmentService : IAppointmentService
    {

        private readonly IUnitOfWork _unitOfWork;
        // this will be used to access the repository methods
        private readonly IGenericRepository<Appointment, int> _context;
        private readonly IDoctorTimeSlotService _doctorTimeSlotService;
        private readonly IDoctorService _doctorService;
        private readonly ITimeSlotService _timeSlotService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork,UserManager<AppUser> userManager,
            IDoctorService doctorService,ITimeSlotService timeSlotService,
            IDoctorTimeSlotService doctorTimeSlotService, IMapper mapper)

        {

            _unitOfWork = unitOfWork;
            _context = _unitOfWork.Repository<Appointment, int>();
            _mapper = mapper;
            _doctorTimeSlotService = doctorTimeSlotService;
            _doctorService = doctorService;
            _timeSlotService = timeSlotService;
            _userManager = userManager;
        }
        public async Task<AppointmentDTO> AddAppointmentAsync(AppointmentDTO appointmentDto)
        {

            var appointmentEntity = _mapper.Map<Appointment>(appointmentDto);

            var addedAppointment = await _context.AddAsync(appointmentEntity);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"

            return _mapper.Map<AppointmentDTO>(addedAppointment);
        }

        //public async Task<BookAppointment> BookAppointmentAsync(BookAppointment bookAppointmentDto)
        //{

        //    Appointment appointment = _mapper.Map<Appointment>(bookAppointmentDto);

        //    // i want to get the id of doctor timeslot where timeslotid and doctorid equals coming from appointment dto
        //    List<AddDoctorTimeSlotDTO> doctorTimeSlots = await _doctorTimeSlotService
        //        .GetAllAsync(new QueryOptions<DoctorTimeSlot>
        //        {
        //            Filter = dts => (dts.TimeSlotId == bookAppointmentDto.TimeSlotId
        //                    && dts.DoctorId == bookAppointmentDto.DoctorId
        //                    && dts.IsAvailable == true )
        //        });

        //    AddDoctorTimeSlotDTO doctorTimeSlotDTO = doctorTimeSlots.FirstOrDefault();
        //    if (doctorTimeSlotDTO == null)
        //        throw new Exception("This Appointment is not Available now"); 


        //    appointment.DoctorTimeSlotId = doctorTimeSlotDTO.DoctorTimeSlotId;

        //    var addedAppointment = await _context.AddAsync(appointment);

        //    // after confirm and creat of appointment
        //    // disable the timeslot.
        //    doctorTimeSlotDTO.IsAvailable = false;
        //    await _doctorTimeSlotService.UpadateDoctorTimeSlot(doctorTimeSlotDTO, doctorTimeSlotDTO.DoctorTimeSlotId);

        //    await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"

        //    return _mapper.Map<BookAppointment>(addedAppointment);
        //}

        //public async Task<BookAppointment> BookAppointmentAsync(BookAppointment bookAppointmentDto)
        //{
        //    // 1. Map to entity
        //    var appointment = _mapper.Map<Appointment>(bookAppointmentDto);

        //    // 2. Get the exact DoctorTimeSlot entity using domain model service
        //    var doctorTimeSlot = (await _unitOfWork.Repository<DoctorTimeSlot, int>()
        //        .GetAllAsync(new QueryOptions<DoctorTimeSlot>
        //        {
        //            Filter = dts => dts.TimeSlotId == bookAppointmentDto.TimeSlotId
        //                         && dts.DoctorId == bookAppointmentDto.DoctorId
        //                         && dts.IsAvailable,
        //        }))
        //        .FirstOrDefault();

        //    if (doctorTimeSlot == null)
        //        throw new Exception("This appointment is not available right now.");

        //    // 3. Assign the DoctorTimeSlotId
        //    appointment.DoctorTimeSlotId = doctorTimeSlot.DoctorTimeSlotId;

        //    // 4. Add the appointment
        //    var addedAppointment = await _context.AddAsync(appointment);

        //    // 5. Mark the DoctorTimeSlot as unavailable
        //    doctorTimeSlot.IsAvailable = false;
        //    await _unitOfWork.Repository<DoctorTimeSlot, int>().UpdateAsync(doctorTimeSlot);

        //    // 6. Save all changes
        //    await _unitOfWork.CompleteAsync();

        //    // 7. Return result mapped to DTO
        //    return _mapper.Map<BookAppointment>(addedAppointment);
        //}


        public async Task<BookAppointment> BookAppointmentAsync(BookAppointment bookAppointmentDto)
        {
            Appointment appointment = _mapper.Map<Appointment>(bookAppointmentDto);

            var doctorTimeSlots = await _doctorTimeSlotService.GetAllAsync(new QueryOptions<DoctorTimeSlot>
            {


                Filter = dts => dts.TimeSlotId == bookAppointmentDto.TimeSlotId


                             && dts.DoctorId == bookAppointmentDto.DoctorId
                             && dts.IsAvailable == true
            });

            var doctorTimeSlotDTO = doctorTimeSlots.FirstOrDefault();
            if (doctorTimeSlotDTO == null)
                throw new Exception($"No available slot found for DoctorId {bookAppointmentDto.DoctorId} and TimeSlotId {bookAppointmentDto.TimeSlotId}");

            appointment.DoctorTimeSlotId = doctorTimeSlotDTO.DoctorTimeSlotId;

            var result = await _context.AddAsync(appointment);

            doctorTimeSlotDTO.IsAvailable = false;

            await _doctorTimeSlotService.UpadateDoctorTimeSlot(doctorTimeSlotDTO, doctorTimeSlotDTO.DoctorTimeSlotId);
            await _unitOfWork.CompleteAsync();


            var doctor = await _doctorService.GetDoctorByIdAsync(doctorTimeSlotDTO.DoctorId);

            if (doctor.Service == _01_DataAccessLayer.Enums.DoctorService.OnlineConsultion.ToString())
            {
                await CreateRoomForAppointment(result.AppointmentId);
            }



            return new BookAppointment
            {
                AppointmentId = result.AppointmentId,
                DoctorId = bookAppointmentDto.DoctorId,
                TimeSlotId = bookAppointmentDto.TimeSlotId,
                DoctorTimeSlotId = doctorTimeSlotDTO.DoctorTimeSlotId,
                PaymentId = appointment.PaymentId,
                Notes = appointment.Notes,
            };
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


        public async Task<string> CreateRoomForAppointment(int appointmentId)
        {
            var appointment = await _context.GetByIdAsync(appointmentId);
            if (appointment == null || appointment.VedioCallUrl != null)
                throw new ArgumentException("Invalid appointment or already has a room");

            string roomName = $"appointment-{appointmentId}";
            string roomUrl = $"https://meet.jit.si/{roomName}";

            appointment.VedioCallUrl = roomUrl;
            await _unitOfWork.CompleteAsync();

            return roomUrl;

        }

        public async Task<string> JoinRoom(int appointmentId, string appUserId)
        {
            List<AppUser> users = await _unitOfWork.Repository<AppUser, string>().GetAllAsync(new QueryOptions<AppUser>
            {
                Includes = [a => a.Doctor, a => a.Patient],
                Filter = a => a.Id == appUserId
            });

            AppUser appUser = users.FirstOrDefault();
            if (appUser == null)
                throw new Exception("Not Permitted to Join Meeting now");

           

            List<Appointment> appointments = await _context.GetAllAsync(new QueryOptions<Appointment>
            {
                Filter = a => a.AppointmentId == appointmentId,
                Includes = [a => a.DoctorTimeSlot]
            });

            Appointment? appointment = appointments.FirstOrDefault();

            if(appointment != null)
            {
                bool permitted = false;
                if (appUser.Doctor != null && appointment.DoctorTimeSlot.DoctorId == appUser.Doctor.DoctorId)
                    permitted = true;

                if (appUser.Patient != null && appointment.PatientId == appUser.Patient.PatientId)
                    permitted = true;

                TimeSlotDTO timeslot = await _timeSlotService.GetByIdAsync(appointment.DoctorTimeSlot.TimeSlotId);

                if(DateTime.Now >= timeslot.StartTime && DateTime.Now <= timeslot.EndTime && permitted)
                    return appointment.VedioCallUrl;
            }

            throw new Exception("Not Permitted to Join Meeting now");
        }


        public async Task<string> AddNotesAsync(string notes, int appointmentId, string appUserId)
        {
            AppUser? user = await _userManager.FindByIdAsync(appUserId);
            if (user == null)
                throw new UnauthorizedAccessException();

            bool isAuth = await _userManager.IsInRoleAsync(user, "Doctor");
            if(!isAuth)
                throw new UnauthorizedAccessException();

            var existingAppointment = await _context.GetByIdAsync(appointmentId);
            if (existingAppointment == null)
                throw new ArgumentException("Appointment Not Found");

            existingAppointment.Notes = notes;
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"
            return notes;
        }

    }
}

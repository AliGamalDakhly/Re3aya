using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlot;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlotDTOs;
using _02_BusinessLogicLayer.DTOs.SpecailzationDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class DoctorTimeSlotService : IDoctorTimeSlotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<DoctorTimeSlot, int> _context;
        private readonly IMapper _mapper;

        public DoctorTimeSlotService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = _unitOfWork.Repository<DoctorTimeSlot, int>();
        }

           public async Task<bool> UpadateDoctorTimeSlot(DoctorTimeSlotDTO doctorTimeSlotDTO, int id)
        {
            // we first get the entity from DB.
            var existingDoctorTimeSlot = await _context.GetByIdAsync(id);

            if (existingDoctorTimeSlot == null)
                return false;
            //-------- dont forget to update all fields of your entity ---------
            existingDoctorTimeSlot.IsAvailable = doctorTimeSlotDTO.IsAvailable;

            await _context.UpdateAsync(existingDoctorTimeSlot);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        
        public async Task<DoctorTimeSlotDTO> AddDoctorTimeSlotAsync(DoctorTimeSlotCreateDTO dto)
        {
            // ✅ Validate time logic before proceeding
            if (dto.EndTime <= dto.StartTime)
                throw new ArgumentException("EndTime must be greater than StartTime.");

            
            var timeSlot = new TimeSlot
            {
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                DayOfWeek = dto.DayOfWeek
            };

            await _unitOfWork.Repository<TimeSlot, int>().AddAsync(timeSlot);
            await _unitOfWork.CompleteAsync();

            var doctorTimeSlot = new DoctorTimeSlot
            {
                DoctorId = dto.DoctorId,
                TimeSlotId = timeSlot.TimeSlotId,
                IsAvailable = true
            };

            await _unitOfWork.Repository<DoctorTimeSlot, int>().AddAsync(doctorTimeSlot);
            await _unitOfWork.CompleteAsync();

            var result = await _unitOfWork.Repository<DoctorTimeSlot, int>().GetByIdAsync(doctorTimeSlot.DoctorTimeSlotId);
            return _mapper.Map<DoctorTimeSlotDTO>(result);
        }

        public async Task<bool> UpadateDoctorTimeSlotAsync(DoctorTimeSlotCreateDTO doctorTimeSlotDTO, int id)
        {
            var existing = await _context.GetByIdAsync(id);
            if (existing == null)
                return false;

            // ✅ Validate time logic before proceeding
            if (doctorTimeSlotDTO.EndTime <= doctorTimeSlotDTO.StartTime)
                throw new ArgumentException("EndTime must be greater than StartTime.");

            var timeSlot = await _unitOfWork.Repository<TimeSlot, int>()
                .GetByIdAsync(existing.TimeSlotId);

            if (timeSlot == null)
                return false;

            timeSlot.DayOfWeek = doctorTimeSlotDTO.DayOfWeek;
            timeSlot.StartTime = doctorTimeSlotDTO.StartTime;
            timeSlot.EndTime = doctorTimeSlotDTO.EndTime;

            await _unitOfWork.Repository<TimeSlot, int>().UpdateAsync(timeSlot);
            await _unitOfWork.CompleteAsync();

            return true;
        }
        


        public async Task<bool> DeleteDoctorTimeSlotAsync(int doctorTimeSlotId)
        {
            var slot = await _unitOfWork.Repository<DoctorTimeSlot, int>().GetByIdAsync(doctorTimeSlotId);
            if (slot == null) return false;

            await _unitOfWork.Repository<DoctorTimeSlot, int>().DeleteAsync(slot);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<DoctorTimeSlotDTO>> GetAllAsync(QueryOptions<DoctorTimeSlot>? options = null)
        {
            List<DoctorTimeSlot> doctorTimeSlots = await _context.GetAllAsync(options);

            return _mapper.Map<List<DoctorTimeSlotDTO>>(doctorTimeSlots);
        }

        public async Task<List<DoctorTimeSlotDTO>> GetAllDoctorTimeSlotsAsync(int doctorId)
        {
            var slots = await _unitOfWork.Repository<DoctorTimeSlot, int>().GetAllAsync(new QueryOptions<DoctorTimeSlot>
            {
                Includes = [dts => dts.TimeSlot],
                Filter = dts =>
                    dts.DoctorId == doctorId &&
                    dts.TimeSlot.StartTime >= DateTime.Now
            });

            return _mapper.Map<List<DoctorTimeSlotDTO>>(slots);
        }

        public async Task<bool> ActivateDoctorTimeSlotAsyncAsync(int doctorTimeSlotId)
        {
            var slot = await _unitOfWork.Repository<DoctorTimeSlot, int>().GetByIdAsync(doctorTimeSlotId);
            if (slot == null) return false;

            slot.IsAvailable = true;
            await _unitOfWork.Repository<DoctorTimeSlot, int>().UpdateAsync(slot);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeactivateDoctorTimeSlotAsync(int doctorTimeSlotId)
        {
            var slot = await _unitOfWork.Repository<DoctorTimeSlot, int>().GetByIdAsync(doctorTimeSlotId);
            if (slot == null) return false;

            slot.IsAvailable = false;
            await _unitOfWork.Repository<DoctorTimeSlot, int>().UpdateAsync(slot);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<AvailableDoctorTImeSlotDTO>> AvailableDoctorTimeSlots(int doctorId, DateOnly date)
        {
            DateTime dayStart;
            if (DateTime.Now.Date == date.ToDateTime(TimeOnly.MinValue).Date)
                dayStart = DateTime.Now;
            else
                dayStart = date.ToDateTime(TimeOnly.MinValue); // 00:00

            DateTime dayEnd = date.ToDateTime(TimeOnly.MaxValue);   // 23:59:59.9999999

            List<DoctorTimeSlot> availableTimeSlots = await _context.GetAllAsync(new QueryOptions<DoctorTimeSlot>
            {
                Filter = ts =>
                    ts.DoctorId == doctorId &&
                    ts.TimeSlot.StartTime >= dayStart &&
                    ts.TimeSlot.StartTime <= dayEnd &&
                    ts.IsAvailable,
                Includes = [ts => ts.TimeSlot]
            });

            return _mapper.Map<List<AvailableDoctorTImeSlotDTO>>(availableTimeSlots);
        }

        public async Task<bool> HasAvailableTimeSlots(int doctorId, DateTime fromDateTime)
        {

            var date = fromDateTime.Date; // Get only the date part
            var dayEnd = date.AddDays(1).AddTicks(-1); // End of the same day (23:59:59.9999999)

            var doctorTimeSlots = await _context.GetAllAsync(new QueryOptions<DoctorTimeSlot>
            {
                Includes = [dts => dts.TimeSlot],
                Filter = dts =>
                    dts.DoctorId == doctorId &&
                    dts.IsAvailable &&
                    dts.TimeSlot.StartTime >= fromDateTime &&
                    dts.TimeSlot.StartTime <= dayEnd
            });

            if (doctorTimeSlots?.Count == 0 || doctorTimeSlots == null)
                return false;

            return true;

        }

        public async Task<bool> HasAvailableTimeSlots(int doctorId)
        {

            DateTime dayStart = DateTime.Now; // 00:00

            List<DoctorTimeSlot> doctorTimeSlots = await _context.GetAllAsync(new QueryOptions<DoctorTimeSlot>
            {
                Includes = [dts => dts.TimeSlot],
                Filter = ts => (ts.DoctorId == doctorId &&
                    ts.TimeSlot.StartTime >= dayStart &&
                    ts.IsAvailable)
            });

            if (doctorTimeSlots?.Count == 0 || doctorTimeSlots == null)
                return false;

            return true;

        }
    }
}

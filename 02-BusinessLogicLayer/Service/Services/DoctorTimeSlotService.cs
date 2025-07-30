using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlotDTOs;
using _02_BusinessLogicLayer.DTOs.SpecailzationDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
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

        public async Task<AddDoctorTimeSlotDTO> AddDoctorTimeSlot(AddDoctorTimeSlotDTO doctorTimeSlotDTO)
        {
            DoctorTimeSlot doctorTimeSlot = _mapper.Map<DoctorTimeSlot>(doctorTimeSlotDTO);
            var  addedDoctorTimeSlot = await _context.AddAsync(doctorTimeSlot);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AddDoctorTimeSlotDTO>(addedDoctorTimeSlot);
        }

        public async Task<bool> UpadateDoctorTimeSlot(AddDoctorTimeSlotDTO doctorTimeSlotDTO, int id)
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
        public async Task<List<AddDoctorTimeSlotDTO>> GetAllAsync(QueryOptions<DoctorTimeSlot>? options = null)
        {
            List<DoctorTimeSlot> doctorTimeSlots = await _context.GetAllAsync(options);

            return _mapper.Map<List<AddDoctorTimeSlotDTO>>(doctorTimeSlots);
        }

        public async Task<List<AvailableDoctorTImeSlotDTO>> AvailableDoctorTimeSlots(int doctorId, DateOnly date)
        {
            DateTime dayStart = date.ToDateTime(TimeOnly.MinValue); // 00:00
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

            if (doctorTimeSlots?.Count == 0  || doctorTimeSlots == null)
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

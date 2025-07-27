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
    }
}

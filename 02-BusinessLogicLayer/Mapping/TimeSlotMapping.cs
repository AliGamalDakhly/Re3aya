using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlotDTOs;
using AutoMapper;

namespace _02_BusinessLogicLayer.Mapping
{
    public class TimeSlotMapping : Profile
    {
        public TimeSlotMapping()
        {
            CreateMap<DoctorTimeSlot, AvailableDoctorTImeSlotDTO>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.TimeSlot.DayOfWeek.ToString()))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.TimeSlot.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.TimeSlot.EndTime))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.TimeSlotId, opt => opt.MapFrom(src => src.TimeSlotId));
        }
    }
}
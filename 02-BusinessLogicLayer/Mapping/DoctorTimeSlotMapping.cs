using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using _02_BusinessLogicLayer.DTOs.DoctorTimeSlotDTOs;
using AutoMapper;

namespace _02_BusinessLogicLayer.Mapping
{
    public class DoctorTimeSlotMapping: Profile
    {
        public DoctorTimeSlotMapping() {

            CreateMap<AddDoctorTimeSlotDTO, DoctorTimeSlot>()
                .ForMember(dest => dest.DoctorTimeSlotId, opt => opt.MapFrom(src => src.DoctorTimeSlotId))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.TimeSlotId, opt => opt.MapFrom(src => src.TimeSlotId))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable));

            CreateMap<DoctorTimeSlot, AddDoctorTimeSlotDTO>()
                .ForMember(dest => dest.DoctorTimeSlotId, opt => opt.MapFrom(src => src.DoctorTimeSlotId))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.TimeSlotId, opt => opt.MapFrom(src => src.TimeSlotId))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable));

        }
    }
}

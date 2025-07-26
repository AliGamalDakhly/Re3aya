using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.AccountDTOs;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Mapping
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<DoctorRegisterDTO, Doctor>();
            CreateMap<Doctor, DoctorGetDTO>();
            CreateMap<DoctorUpdateDTO, Doctor>();

            CreateMap<Doctor, DoctorCardDTO>()
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Service, opt => opt.MapFrom(src => src.Service == DoctorService.InClinic ? "الكشف بالعيادة" : "استشارة اونلاين"))
                .ForMember(dest => dest.Fees, opt => opt.MapFrom(src => src.Fees))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization.Name))
                .ForMember(dest => dest.ExpYears, opt => opt.MapFrom(src => src.ExpYears))
                .ForMember(dest => dest.RatingValue, opt => opt.MapFrom(src => src.RatingValue))
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses.Select(a => a.DetailedAddress)));


            CreateMap<Doctor, DoctorDetialsDTO>()
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Service, opt => opt.MapFrom(src => src.Service == DoctorService.InClinic ? "الكشف بالعيادة" : "استشارة اونلاين"))
                .ForMember(dest => dest.Fees, opt => opt.MapFrom(src => src.Fees))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization.Name))
                .ForMember(dest => dest.ExpYears, opt => opt.MapFrom(src => src.ExpYears))
                .ForMember(dest => dest.RatingValue, opt => opt.MapFrom(src => src.RatingValue))
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses.Select(a => a.DetailedAddress)));
        }
    }
}

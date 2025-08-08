using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.AccountDTOs;
using _02_BusinessLogicLayer.DTOs.AdminDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Mapping
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminRegisterDTO, AppUser>();
            CreateMap<Admin, AdminDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.AppUser.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.AppUser.Gender));
            CreateMap<Admin, AdminUpdateDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.AppUser.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.AppUser.Gender));
        }
    }
}

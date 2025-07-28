using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.AddressDTOs;
using AutoMapper;

namespace _02_BusinessLogicLayer.Mapping
{
    public class AddressMapping: Profile
    {
        public AddressMapping() {
            CreateMap<Government, GovernmentDTO>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.GovernmentId, opt => opt.MapFrom(src => src.GovernmentId));


            CreateMap<GovernmentDTO, Government>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.GovernmentId, opt => opt.MapFrom(src => src.GovernmentId));

            CreateMap<City, CityDTO>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.GovernmentId, opt => opt.MapFrom(src => src.GovernmentId));

            CreateMap<CityDTO, City>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.GovernmentId, opt => opt.MapFrom(src => src.GovernmentId));

            CreateMap<Address, AddressDTO>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.DetailedAddress, opt => opt.MapFrom(src => src.DetailedAddress))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId));

            CreateMap<AddressDTO, Address>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.DetailedAddress, opt => opt.MapFrom(src => src.DetailedAddress))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId));
        }
    }
}

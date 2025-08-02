using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.SpecailzationDTOs;
using AutoMapper;

namespace _02_BusinessLogicLayer.Mapping
{
    public class SpecialziationMapping: Profile
    {
        public SpecialziationMapping() {

            CreateMap<Specialization, SpecializationDTO>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<SpecializationDTO, Specialization>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}

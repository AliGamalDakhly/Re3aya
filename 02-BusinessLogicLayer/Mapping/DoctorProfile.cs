using _01_DataAccessLayer.Models;
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
        }
    }
}

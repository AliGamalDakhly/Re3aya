using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.AccountDTOs;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using _02_BusinessLogicLayer.DTOs.SystemInfoDTOs;
using AutoMapper;

namespace _02_BusinessLogicLayer.Mapping
{
    public class SystmeInfoMapping: Profile
    {
        public SystmeInfoMapping() {
            CreateMap<SystemInfoDTO, SystemInfo>();
            CreateMap<SystemInfo, SystemInfoDTO>();
        }
    }
}

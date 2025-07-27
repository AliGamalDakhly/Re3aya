using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.AppointmentDTOs;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using AutoMapper;

namespace _02_BusinessLogicLayer.Mapping
{
    public class AppointmentMapping: Profile
    {
        public AppointmentMapping() {

            // Appointment
            CreateMap<BookAppointmentDTO, Appointment>();
            CreateMap<CancelAppointmentDTO, Appointment>();

            CreateMap<Appointment, AppointmentDTO>()
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId))
                .ForMember(dest => dest.DoctorTimeSlotId, opt => opt.MapFrom(src => src.DoctorTimeSlot))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));


            CreateMap<AppointmentDTO, Appointment>()
                 .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId))
                .ForMember(dest => dest.DoctorTimeSlotId, opt => opt.MapFrom(src => src.DoctorTimeSlotId))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}

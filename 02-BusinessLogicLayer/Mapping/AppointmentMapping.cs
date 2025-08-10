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
    public class AppointmentMapping : Profile
    {
        public AppointmentMapping()
        {

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


            CreateMap<BookAppointment, Appointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId));

            CreateMap<Appointment, BookAppointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId));




            CreateMap<Appointment, AppointmentWithPatientDTO>()
                .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.AppointmentId))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorTimeSlot.Doctor.DoctorId))  
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.DoctorTimeSlot.Doctor.AppUser.FullName))
                .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src => src.DoctorTimeSlot.Doctor.Specialization.Name))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.DoctorTimeSlot.TimeSlot.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.DoctorTimeSlot.TimeSlot.EndTime))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.DoctorTimeSlot.TimeSlot.StartTime.Date))
                .ForMember(dest => dest.VedioCallUrl, opt => opt.MapFrom(src => src.VedioCallUrl))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Patient.PatientId))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.AppUser.FullName))
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payment.PaymentId))
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Payment.TransactionId))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Payment.Amount))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.Payment.Status.ToString()));







        }
    }
}

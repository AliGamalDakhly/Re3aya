using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using AutoMapper;

namespace _02_BusinessLogicLayer.Mapping
{
    public class PatientMapping : Profile
    {
        public PatientMapping()
        {


            CreateMap<PatientDTO, Patient>()
                .ForPath(dest => dest.AppUser.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForPath(dest => dest.AppUser.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.AppUser.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.AppUser.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForPath(dest => dest.AppUser.Gender, opt => opt.MapFrom(src => src.Gender));


            //for Get 
            CreateMap<Patient, PatientDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.AppUser.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.AppUser.Gender.ToString()));


            //for patient details
            CreateMap<Patient, PatientDetailsDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.AppUser.DateOfBirth))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.AppUser.DateOfBirth.ToDateTime(new TimeOnly(0, 0)))))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.AppUser.Gender.ToString()));

            //for update
            CreateMap<UpdatePatientDTO, Patient>()
            .ForPath(dest => dest.AppUser.FullName, opt => opt.MapFrom(src => src.FullName))
             .ForPath(dest => dest.AppUser.Email, opt => opt.MapFrom(src => src.Email))
             .ForPath(dest => dest.AppUser.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.AppUser.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForPath(dest => dest.AppUser.Gender, opt => opt.MapFrom(src => src.Gender));
        }

        #region helpfull methods
        public int CalculateAge(DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
        #endregion
    }
}

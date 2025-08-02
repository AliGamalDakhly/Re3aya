using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.AccountDTOs;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using AutoMapper;

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
                .ForMember(dest => dest.DoctorService, opt => opt.MapFrom(src => src.Service))
                .ForMember(dest => dest.Fees, opt => opt.MapFrom(src => src.Fees))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization.Name))
                .ForMember(dest => dest.ExpYears, opt => opt.MapFrom(src => src.ExpYears))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.AppUser.Gender.ToString()))
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src => src.SpecializationId))
                .ForMember(dest => dest.RatingValue, opt => opt.MapFrom(src => src.RatingValue))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.Documents

                .FirstOrDefault(d => d.DocumentType == DocumentType.ProfileImage).FilePath))

                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses.Select(a => a.DetailedAddress)));



            CreateMap<Doctor, DoctorDetialsDTO>()
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Service, opt => opt.MapFrom(src => src.Service == DoctorService.InClinic ? "الكشف بالعيادة" : "استشارة اونلاين"))
                .ForMember(dest => dest.Fees, opt => opt.MapFrom(src => src.Fees))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization.Name))
                .ForMember(dest => dest.ExpYears, opt => opt.MapFrom(src => src.ExpYears))
                .ForMember(dest => dest.RatingValue, opt => opt.MapFrom(src => src.RatingValue))
                .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.AboutMe))

                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Addresses.FirstOrDefault().Location))

                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.Documents
                            .FirstOrDefault(d => d.DocumentType == DocumentType.ProfileImage).FilePath))
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses.Select(a => a.DetailedAddress)));
        }
    }
}

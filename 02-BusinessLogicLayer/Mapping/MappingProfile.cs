using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.AddressDTOs;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using _02_BusinessLogicLayer.DTOs.RatingDTOs;
using _02_BusinessLogicLayer.DTOs.SpecailzationDTOs;
using AutoMapper;

namespace _02_BusinessLogicLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Patient Dto mapping 

            #region  First way for mapping   (we will not use this way)
            //// this way convert from DTO to Entity
            //// not recmmended for reading data it use to Create but also not recommended because it not achive single responsibility
            //// can not password hashing and other things

            CreateMap<PatientDTO, Patient>()
               .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => new AppUser
               {
                   FullName = src.FullName,
                   Email = src.Email,
                   PasswordHash = src.Password, // Assuming you want to map Password to PasswordHash
                   UserName = src.UserName,
                   PhoneNumber = src.PhoneNumber,
                   DateOfBirth = src.DateOfBirth,
                   //Gender = src.Gender,    //this is error because it is enum in the model and string in DTO
                   //convert to enum to solve this error
                   Gender = Enum.Parse<Gender>(src.Gender)

               }));

            #endregion

            #region secand way for mapping this is the best for our case
            // this way convert from Entity to DTO

            CreateMap<Patient, PatientDTO>()
                       .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                       .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                       .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.AppUser.PasswordHash))
                       .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                       .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                       .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.AppUser.DateOfBirth))
                       .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.AppUser.Gender))
                       ;

            //CreateMap<PatientCreateDTO, Patient>();

            #endregion


            // if you would like using Auto mapping for other entity
            // add your mapping blow like the previes DTO Mapping
            //add your mapping here
            //......
            //......

            #region Mapping of Specialization Entity (Ali)
            CreateMap<Specialization, SpecializationDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<SpecializationDTO, Specialization>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            #endregion

            #region Mapping of Rating Entity (Ali)
            CreateMap<Rating, RatingDTO>()
                .ForMember(dest => dest.RatingValue, opt => opt.MapFrom(src => src.RatingValue))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId));

            CreateMap<RatingDTO, Rating>()
                .ForMember(dest => dest.RatingValue, opt => opt.MapFrom(src => src.RatingValue))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId));
            #endregion

            #region government mapping
            CreateMap<Government, GovernmentDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<GovernmentDTO, Government>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region city mapping
            CreateMap<City, CityDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.GovernmentId, opt => opt.MapFrom(src => src.GovernmentId));

            CreateMap<CityDTO, City>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.GovernmentId, opt => opt.MapFrom(src => src.GovernmentId));
            #endregion
            #region Address Mapping

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
            #endregion
        }
    }

}

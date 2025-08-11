using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.AddressDTOs;
using _02_BusinessLogicLayer.DTOs.AppointmentDTOs;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using _02_BusinessLogicLayer.DTOs.RatingDTOs;
using _02_BusinessLogicLayer.DTOs.SpecailzationDTOs;
using _02_BusinessLogicLayer.DTOs.TimeSlotDTOs;
using AutoMapper;
using CancelAppointmentDTO = _02_BusinessLogicLayer.DTOs.PatientDTOs.CancelAppointmentDTO;

namespace _02_BusinessLogicLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //********************** Patient Dto Mapping **********************//


            #region  First way for mapping   (we will not use this way)
            //// this way convert from DTO to Entity
            //// not recmmended for reading data it use to Create but also not recommended because it not achive single responsibility
            //// can not password hashing and other things

            CreateMap<PatientDTO, Patient>()
               .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => new AppUser
               {
                   FullName = src.FullName,
                   Email = src.Email,
                   //PasswordHash = src.Password, // Assuming you want to map Password to PasswordHash
                   //UserName = src.UserName,
                   PhoneNumber = src.PhoneNumber,
                   DateOfBirth = src.DateOfBirth,
                   Gender = src.Gender,    //this is error because it is enum in the model and string in DTO
                                           //convert to enum to solve this error
                                           // Gender = Enum.Parse<Gender>(src.Gender)

               }));

            #endregion

            #region secand way for mapping this is the best for our case
            // this way convert from Entity to DTO


            //for Get 
            CreateMap<Patient, PatientDTO>()
                       //Patient Id wil Automatically map because it is the same name in both models
                       .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                       .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                       .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                       .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.AppUser.DateOfBirth))
                       .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.AppUser.Gender.ToString()));


            //for patient details
            CreateMap<Patient, PatientDetailsDTO>()
                //patient id atumaticlay map
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.AppUser.DateOfBirth))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.AppUser.DateOfBirth.ToDateTime(new TimeOnly(0, 0)))))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.AppUser.Gender.ToString()));

            //for update
            CreateMap<UpdatePatientDTO, AppUser>();


            //CreateMap<Patient, UpdatePatientDTO>()
            //    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
            //    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber));
            // .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments));

            //CreateMap<LoginDTO,>
            #endregion


            // if you would like using Auto mapping for other entity
            // add your mapping blow like the previes DTO Mapping
            //add your mapping here
            //......
            //......
            //********************** Patient Rating Dto Mapping **********************//
            #region Mapping  of Rating and Appointment DTOs
            // Rating
            CreateMap<AddRatingDTO, Rating>();
            CreateMap<UpdateRatingDTO, Rating>();

            // Appointment
            CreateMap<BookAppointmentDTO, Appointment>();
            CreateMap<CancelAppointmentDTO, Appointment>();

            // CreateMap<Appointment, AppointmentResponseDTO>();
            CreateMap<Appointment, AppointmentResponseDTO>()
               .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.AppointmentId))
               .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.DoctorTimeSlot.Doctor.AppUser.FullName))
               .ForMember(dest => dest.DoctorLocation, opt => opt.MapFrom(src => src.DoctorTimeSlot.Doctor.Addresses[0].Location))
               .ForMember(dest => dest.DoctorDetailedAddress, opt => opt.MapFrom(src => src.DoctorTimeSlot.Doctor.Addresses[0].DetailedAddress))
               .ForMember(dest => dest.DoctorPhoneNumber, opt => opt.MapFrom(src => src.DoctorTimeSlot.Doctor.AppUser.PhoneNumber))



               //.ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.AppUser.FullName))
               .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src => src.DoctorTimeSlot.Doctor.Specialization.Name))
               .ForMember(dest => dest.VideoCallUrl, opt => opt.MapFrom(src => src.VedioCallUrl))
               .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
               //.ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.DoctorTimeSlot.TimeSlot.StartTime.ToString(@"hh\:mm")))
               //.ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.DoctorTimeSlot.TimeSlot.EndTime.ToString(@"hh\:mm")))
               .ForMember(dest => dest.StartTime,
                     opt => opt.MapFrom(src =>
                    src.DoctorTimeSlot.TimeSlot.StartTime.ToString("yyyy-MM-dd HH:mm")))

                 .ForMember(dest => dest.EndTime,
                opt => opt.MapFrom(src =>
                    src.DoctorTimeSlot.TimeSlot.EndTime.ToString("yyyy-MM-dd HH:mm")))
               .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));


            #endregion

            //********************** Specialization Dto Mapping **********************//
            #region Mapping of Specialization Entity (Ali)
            CreateMap<Specialization, SpecializationDTO>()
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src => src.SpecializationId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<SpecializationDTO, Specialization>()
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src => src.SpecializationId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            //********************** Time Slot Mapping **********************//

            #region     TimeSlot DTOs Mapping 

            //for view
            CreateMap<TimeSlot, TimeSlotDTO>()
              .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek.ToString()))
              .ReverseMap()
             .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => Enum.Parse<WeekDays>(src.DayOfWeek)));

            //for edit
            CreateMap<TimeSlot, EditTimeSlotDTO>().ReverseMap();

            //for create will not pass id
            CreateMap<TimeSlot, CreateTimeSlotDTO>().ReverseMap();

            #endregion    

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
            #region  Appointment

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

            #endregion
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

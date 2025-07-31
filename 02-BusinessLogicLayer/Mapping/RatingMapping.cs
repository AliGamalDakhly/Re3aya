using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using _02_BusinessLogicLayer.DTOs.RatingDTOs;
using AutoMapper;

namespace _02_BusinessLogicLayer.Mapping
{
    public class RatingMapping: Profile
    {
        public RatingMapping() {


            CreateMap<Rating, RatingDTO>()
                .ForMember(dest => dest.RatingId, opt => opt.MapFrom(src => src.RatingId))
                .ForMember(dest => dest.RatingValue, opt => opt.MapFrom(src => src.RatingValue))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId));

            CreateMap<RatingDTO, Rating>()
                .ForMember(dest => dest.RatingId, opt => opt.MapFrom(src => src.RatingId))
                .ForMember(dest => dest.RatingValue, opt => opt.MapFrom(src => src.RatingValue))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId));


            CreateMap<DoctorRatingDTO, Rating>()
               .ForMember(dest => dest.RatingValue, opt => opt.MapFrom(src => src.RatingValue))
               .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
               .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
               .ForMember(dest => dest.RatingId, opt => opt.MapFrom(src => src.RatingId));

            CreateMap<Rating, DoctorRatingDTO>()
               .ForMember(dest => dest.RatingValue, opt => opt.MapFrom(src => src.RatingValue))
               .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
               .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
               .ForMember(dest => dest.RatingId, opt => opt.MapFrom(src => src.RatingId));

            // Rating
            CreateMap<AddRatingDTO, Rating>();
            CreateMap<UpdateRatingDTO, Rating>();
        }
    }
}

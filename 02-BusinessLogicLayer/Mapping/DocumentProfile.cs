using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using _02_BusinessLogicLayer.DTOs.DocumentDTO;
using AutoMapper;

namespace _02_BusinessLogicLayer.Mapping
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentDTO>().ReverseMap();

            CreateMap<Document, DoctorDocumentsDTO>()
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId))
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath));

            CreateMap<DoctorDocumentsDTO, Document>()
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId))
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath));

        }
    }
}

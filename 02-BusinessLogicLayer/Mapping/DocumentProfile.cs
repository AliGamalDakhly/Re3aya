using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.DocumentDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Mapping
{
    public class DocumentProfile :Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentDTO>().ReverseMap();
        }
    }
}

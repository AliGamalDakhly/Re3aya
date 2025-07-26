using _01_DataAccessLayer.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.DocumentDTO
{
    public class DocumentUploadDTO
    {
        public DocumentType DocumentType { get; set; }
        public int DoctorId { get; set; }
    }

    public class DocumentEditDTO
    {
        public int DocumentId { get; set; }
        public DocumentType DocumentType { get; set; }
        public bool? IsVerified { get; set; }
        public string? FilePath { get; set; }


    }

 

}

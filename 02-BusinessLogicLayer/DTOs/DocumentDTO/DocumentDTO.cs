using _01_DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.DocumentDTO
{
    public class DocumentDTO
    {
        public int DocumentId { get; set; }
        public DocumentType DocumentType { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
        public bool IsVerified { get; set; }

        public int DoctorId { get; set; }
    }
}

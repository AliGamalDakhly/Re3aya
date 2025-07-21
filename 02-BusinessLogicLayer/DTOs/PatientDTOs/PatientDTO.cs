using _01_DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class PatientDTO
    {

        #region  Patient DTO
        public int PatientId { get; set; } // PatientId from Patient model
        public string UserId { get; set; } // UserId from AppUser model
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        //public string Password { get; set; }   will not be used in DTOs, only in RegisterDTOs
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string UserName { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);


       
        #endregion
    }
}


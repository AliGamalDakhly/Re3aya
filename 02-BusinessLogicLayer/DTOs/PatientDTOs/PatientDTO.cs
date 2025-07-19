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
        public string PatientId { get; set; }       //PatientId from Patient model, we will use string to be compatible with AppUserId in AppUser model
        public string AppUserId { get; set; }       //AppUserId from Patient model, this is the foreign key to AppUser
        public string UserName { get; set; }        //from iedentityUser
        public string FullName { get; set; }        //from AppUser
        public string PhoneNumber { get; set; }     //from IdentityUser

        public string Email { get; set; }           //from IdentityUser
        public string Password { get; set; }        //from IdentityUser

        public DateOnly DateOfBirth { get; set; }  //from AppUser

        public string Gender { get; set; }         //from AppUser

        //// for Get
        //public int PatientId { get; set; }

        //public string FullName { get; set; }
        //public string PhoneNumber { get; set; }
        //public string Email { get; set; }

        //public Gender Gender { get; set; }

        //public DateTime DateOfBirth { get; set; }
        #endregion
    }
}


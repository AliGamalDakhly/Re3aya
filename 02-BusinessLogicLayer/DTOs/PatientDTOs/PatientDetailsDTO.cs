﻿using _01_DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class PatientDetailsDTO
    {
        public int PatientId { get; set; } // PatientId from Patient model
        public string FullName { get; set; }
        public string Email {  get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }

    }
}

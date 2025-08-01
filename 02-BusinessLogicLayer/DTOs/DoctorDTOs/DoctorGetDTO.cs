﻿using _01_DataAccessLayer.Enums;

namespace _02_BusinessLogicLayer.DTOs.DoctorDTOs
{
    public class DoctorGetDTO
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }  // from AppUser
        public string Email { get; set; }     // from AppUser
        public string PhoneNumber { get; set; }  // from AppUser
        public double Balance { get; set; }
        public int? ExpYears { get; set; }
        public string AboutMe { get; set; }
        public float RatingValue { get; set; }
        public double Fees { get; set; }
        public string Service { get; set; }
        public DoctorService ServiceId { get; set; } // Enum for service type
        public DoctorAccountStatus Status { get; set; }
        public string Specialization { get; set; }
        public int? SpecializationId { get; set; }
        public string location { get; set; } // Assuming this is a string representation of the location
        public string DetailedAddress { get; set; } // Assuming this is a string representation of the detailed address

        public int CityId { get; set; } // Assuming this is a string representation of the city
        public string CityName { get; set; } // Name of the city
        public int GovernmentId { get; set; } // Assuming this is a string representation of the government
        public string GovernmentName { get; set; } // Name of the government
        public string MedicalLicenseUrl { get; set; } // URL to the medical license document

        public string NationalIdUrl { get; set; } // URL to the national ID document
        public string GraduationCertificateUrl { get; set; } // URL to the graduation certificate document
        public string ExperienceCertificateUrl { get; set; }
        public string ProfilePictureUrl { get; set; } // URL to the doctor's profile picture
        public string Gender { get; set; } // Assuming this is a string representation

    }
}

using _01_DataAccessLayer.Enums;
using System;

namespace _02_BusinessLogicLayer.DTOs.PatientDTOs
{
    public class AppointmentWithPatientDTO
    {
        public int AppointmentId { get; set; }

        public string DoctorName { get; set; }
        public string DoctorId { get; set; }

        public string SpecializationName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime Date { get; set; }
        public string? VedioCallUrl { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int PaymentId { get; set; }
        public string TransactionId { get; set; }
        public double Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }



    }
}

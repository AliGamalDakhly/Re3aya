namespace _02_BusinessLogicLayer.DTOs.AppointmentDTOs
{
    public class AppointmentDTO
    {

        public int PatientId { get; set; }

        public string Status { get; set; }
        public int PaymentId { get; set; }
        public int DoctorTimeSlotId { get; set; }
        public string Notes { get; set; }

    }
}

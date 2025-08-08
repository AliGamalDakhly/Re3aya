using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IDoctorService
    {
        //Task<DoctorGetDTO> AddDoctorAsync(DoctorRegisterDTO doctorDto);
        Task<bool> DeleteDoctorByIdAsync(int id);
        //Task<bool> DeleteDoctorAsync(Doctor doctor);
        Task<bool> ApproveDoctorAccountAsync(int doctorId);
        Task<bool> SuspendDoctorAccountAsync(int doctorId);
        Task<bool> RejectDoctorAccountAsync(int doctorId);

        Task<bool> PendingDoctorAccountAsync(int doctorId);


        Task<bool> DeactivatedDoctorAccountAsync(int doctorId);
        Task<DoctorGetDTO> UpdateDoctorAsync(int id, DoctorUpdateDTO doctorDto);
        Task<List<DoctorCardDTO>> GetAllAsync();
        Task<DoctorGetDTO> GetDoctorByIdAsync(int id);
        Task<DoctorDetialsDTO> GetDoctorDetailsByIdAsync(int id);
        Task<int> CountDoctorsAsync();
        Task<bool> ExistsDoctorAsync(int id);
        Task<string?> GetDoctorFullNameByIdAsync(int doctorId);

        Task UpdateDoctorRating(int doctorId);

        Task<List<DoctorSmallInfoDto>> GetAllDoctorsAsync();

        Task<bool> UpdateDoctorBalanceAsync(int doctorId, double amountToAdd);
        Task<bool> DeductDoctorBalanceAsync(int doctorId, double amountToDeduct);

        Task<List<AppointmentWithPatientDTO>> GetAppointmentsByDoctorIdAsync(int doctorId);


    }
}

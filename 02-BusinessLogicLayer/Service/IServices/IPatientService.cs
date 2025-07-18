using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IPatientService
    {

        Task<PatientDTO> AddPatientAsync(PatientDTO patientDto);
        Task<PatientDTO> UpdatePatientAsync(int id, PatientDTO patientDto);
        Task<PatientDTO> GetPatientByIdAsync(int id);
        Task<List<PatientDTO>> GetAllPatientsAsync();
        Task<PatientDTO> GetPatientByNameAsync(string name);
        Task<PatientDTO> GetPatientByUserIdAsync(string userId);
        Task<bool> DeletePatientAsync(int id);
        Task<bool> IsPatientExist(string userId);



        #region
      

        //we will handle this after add AppointmentDTO
        //Task<List<AppointmentDTO>> GetAppointmentForPatientAsync(int patientId); //get all appointments for a specific patient


        //we will handle this after add RatingDTO
        //Task AddRatingAsync(RatingDTO rating)                                    //this allow patient add Rating to his doctor after tratment 

        //we may need this in future 
        //Task<PatientDTO> GetPatientProfileAsync(int userId);



        //others that we may use

        //Task<bool> RegisterPatientAsync(PatientDTO patientDto);

        //Task<bool> UnregisterPatientAsync(PatientDTO patientDto);

        //Task<bool> UpdatePatientAsync(PatientDTO patientDto);

        //Task<bool> DeletePatientAsync(PatientDTO patientDto);

        #endregion
    }
}

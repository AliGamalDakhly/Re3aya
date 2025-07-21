using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.AccountDTOs;
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

        Task<PatientDTO> RegisterAsync(PatientDTO dto);                  // registr from patient side
        //Task<PatientDetailsDTO> GetProfileAsync(string appUserId);       // Detailed View by identity
        Task<bool> UpdateProfileAsync(UpdatePatientDTO dto, int Id); 
        Task<bool> DeleteAsync(int patientId);

        Task<PatientDTO> GetByIdAsync(int patientId);                 
        Task<List<PatientDTO>> GetAllAsync(QueryOptions<Patient> options);
        Task<bool> ExistsAsync(Expression<Func<Patient, bool>> predicate);
        Task<int> CountAsync(Expression<Func<Patient, bool>>? filter = null);



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

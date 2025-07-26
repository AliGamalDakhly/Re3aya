using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.AccountDTOs;
using _02_BusinessLogicLayer.DTOs.AppointmentDTOs;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CancelAppointmentDTO = _02_BusinessLogicLayer.DTOs.PatientDTOs.CancelAppointmentDTO;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IPatientService
    {

        Task<PatientDTO> RegisterAsync(PatientDTO dto);                  // registr from patient side
        Task<PatientDetailsDTO> GetProfileByIdAsync(string appUserId);       // Detailed View by identity
        Task<bool> UpdateProfileAsync(UpdatePatientDTO dto, string userId); 
        Task<bool> DeleteAsync(int patientId);

        Task<PatientDTO> GetByIdAsync(int patientId);                 
        Task<List<PatientDTO>> GetAllAsync(QueryOptions<Patient> options);
        Task<bool> ExistsAsync(Expression<Func<Patient, bool>> predicate);
        Task<int> CountAsync(Expression<Func<Patient, bool>>? filter = null);

        //delete my provile only patient can delete his profile 
        Task<bool> DeleteProfileAsync(string appUserId); //delete his profile by his identity

        //Add Rating only after treatment
        Task<bool> AddRatingAsync(AddRatingDTO rating, string appUserId); //this allow patient add Rating to his doctor
        Task<bool> UpdateRatingAsync(UpdateRatingDTO rating,string appUserId); //this allow patient update his Rating 

        //book appointment only patient can book appointment when he is logged in
        Task<bool> BookAppointmentAsync(BookAppointmentDTO dto,string appUserId);
        Task<bool> CancelAppointmentAsync(CancelAppointmentDTO dto, string appUserId);

        //get all appointments for a specific patient
        Task<List<AppointmentDTO>> GetAppointmentsAsync(string appUserId); // by his identity


        #region


        //we will handle this after add AppointmentDTO
        //Task<List<AppointmentDTO>> GetAppointmentForPatientAsync(int patientId); //get all appointments for a specific patient




        //we will handle this after add RatingDTO
        //Task AddRatingAsync(RatingDTO rating)                                    //this allow patient add Rating to his doctor after tratment 




       

        #endregion
    }
}

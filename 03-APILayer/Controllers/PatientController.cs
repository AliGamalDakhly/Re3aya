using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }


        // Add a new patient.
        [HttpPost("Register Patient")]
        public async Task<IActionResult> AddPatient([FromBody] PatientDTO patientDto)
        {
            try
            {
                var createdPatient = await _patientService.RegisterAsync(patientDto);
                //if (createdPatient == null)
                //    return BadRequest("Failed to add patient.");
                return CreatedAtAction(nameof(GetPatientById), new { id = createdPatient.PatientId }, createdPatient);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while Registeration {ex.Message}");
            }

        }

        /// Get a patient by ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            try
            {
                var patient = await _patientService.GetByIdAsync(id);
                if (patient == null)
                    return NotFound($"No Patient Found with Id {id}");
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while Retrieving Patient: {ex.Message}");
            }
        }


        //Update Profile
        [HttpPut("UpdateProfile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdatePatientDTO updatePatientDto)
        {
            try
            {
                var result = await _patientService.UpdateProfileAsync(updatePatientDto, id);
                if (!result)
                    return NotFound($"No Patient Found with Id {id}");
                return Ok("Profile updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while updating profile: {ex.Message}");
            }
        }


        // Delete a patient by ID.
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                var result = await _patientService.DeleteAsync(id);
                if (!result)
                    return NotFound($"No Patient Found with Id {id}");
                return Ok("Patient deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while deleting patient: {ex.Message}");
            }
        }

        // Get all patients 
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPatients([FromQuery] QueryOptions<Patient> options)
        {
            try
            {
                var patients = await _patientService.GetAllAsync(options);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while retrieving patients: {ex.Message}");
            }
        }

        // Check if a patient exists by a specific condition.
        [HttpGet("Exists")]
        public async Task<IActionResult> PatientExists([FromQuery] string condition)
        {
            try
            {
                var exists = await _patientService.ExistsAsync(p => p.AppUser.FullName.Contains(condition) || p.AppUser.Email.Contains(condition));
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while checking patient existence: {ex.Message}");
            }
        }

        // Count patients based on a filter.
        [HttpGet("Count")]
        public async Task<IActionResult> CountPatients([FromQuery] string filter = null)
        {
            try
            {
                Expression<Func<Patient, bool>> predicate = null;
                if (!string.IsNullOrEmpty(filter))
                {
                    predicate = p => p.AppUser.FullName.Contains(filter) || p.AppUser.Email.Contains(filter);
                }
                var count = await _patientService.CountAsync(predicate);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while counting patients: {ex.Message}");
            }


        }

      
    }
}
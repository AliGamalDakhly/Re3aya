//using _01_DataAccessLayer.Models;
//using _01_DataAccessLayer.Repository;
//using _02_BusinessLogicLayer.DTOs.PatientDTOs;
//using _02_BusinessLogicLayer.Service.IServices;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Linq.Expressions;
//using System.Security.Claims;

//namespace _03_APILayer.Controllers
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PatientController : ControllerBase
//    {
//        private readonly IPatientService _patientService;

//        public PatientController(IPatientService patientService)
//        {
//            _patientService = patientService;
//        }

//        private string GetAppUserId() =>
//            User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

//        // Register a new patient
//        [AllowAnonymous]
//        [HttpPost("Register")]
//        public async Task<IActionResult> AddPatient([FromBody] PatientDTO patientDto)
//        {
//            try
//            {
//                var createdPatient = await _patientService.RegisterAsync(patientDto);
//                return CreatedAtAction(nameof(GetPatientById), new { id = createdPatient.PatientId }, createdPatient);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error while registration: {ex.Message}");
//            }
//        }

//        // Get a patient by ID
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetPatientById(int id)
//        {
//            try
//            {
//                var patient = await _patientService.GetByIdAsync(id);
//                if (patient == null)
//                    return NotFound($"No patient found with ID {id}");
//                return Ok(patient);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error while retrieving patient: {ex.Message}");
//            }
//        }

//        // Update profile of logged-in patient
//        [HttpPut("UpdateProfile")]
//        public async Task<IActionResult> UpdateProfile([FromBody] UpdatePatientDTO updatePatientDto)
//        {
//            try
//            {
//                var userId = GetAppUserId();
//                var result = await _patientService.UpdateProfileAsync(updatePatientDto, userId);
//                if (!result)
//                    return NotFound("Patient not found or unauthorized");
//                return Ok("Profile updated successfully.");
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error while updating profile: {ex.Message}");
//            }
//        }

//        // Delete a patient by ID
//        [HttpDelete("Delete/{id}")]
//        public async Task<IActionResult> DeletePatient(int id)
//        {
//            try
//            {
//                var result = await _patientService.DeleteAsync(id);
//                if (!result)
//                    return NotFound($"No patient found with ID {id}");
//                return Ok("Patient deleted successfully.");
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error while deleting patient: {ex.Message}");
//            }
//        }

//        // Get all patients
//        [HttpGet("GetAll")]
//        public async Task<IActionResult> GetAllPatients([FromQuery] QueryOptions<Patient> options)
//        {
//            try
//            {
//                var patients = await _patientService.GetAllAsync(options);
//                return Ok(patients);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error while retrieving patients: {ex.Message}");
//            }
//        }

//        // Check if a patient exists by a search term
//        [HttpGet("Exists")]
//        public async Task<IActionResult> PatientExists([FromQuery] string condition)
//        {
//            try
//            {
//                var exists = await _patientService.ExistsAsync(p =>
//                    p.AppUser.FullName.Contains(condition) || p.AppUser.Email.Contains(condition));
//                return Ok(exists);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error while checking patient existence: {ex.Message}");
//            }
//        }

//        // Count patients 
//        [HttpGet("Count")]
//        public async Task<IActionResult> CountPatients([FromQuery] string filter = null)
//        {
//            try
//            {
//                Expression<Func<Patient, bool>> predicate = null;
//                if (!string.IsNullOrEmpty(filter))
//                {
//                    predicate = p =>
//                        p.AppUser.FullName.Contains(filter) ||
//                        p.AppUser.Email.Contains(filter);
//                }

//                var count = await _patientService.CountAsync(predicate);
//                return Ok(count);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error while counting patients: {ex.Message}");
//            }
//        }

//        // Add rating to a doctor by logged-in patient
//        [HttpPost("AddRating")]
//        public async Task<IActionResult> AddRating([FromBody] AddRatingDTO dto)
//        {
//            try
//            {
//                string userId = GetAppUserId();
//                var result = await _patientService.AddRatingAsync(dto, userId);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred while adding rating: {ex.Message}");
//            }
//        }

//        [HttpPut("UpdateRating")]
//        public async Task<IActionResult> UpdateRating([FromBody] UpdateRatingDTO dto)
//        {
//            try
//            {
//                var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
//                if (userId == null)
//                    return Unauthorized("User ID not found in token.");

//                var result = await _patientService.UpdateRatingAsync(dto, userId);
//                if (!result)
//                    return NotFound($"No rating found with ID {dto.RatingId} or you're not authorized to update it.");

//                return Ok("Rating updated successfully.");
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred while updating rating: {ex.Message}");
//            }
//        }


//        // Book an appointment
//        [HttpPost("BookAppointment")]
//        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentDTO dto)
//        {
//            try
//            {
//                var userId = GetAppUserId();
//                var result = await _patientService.BookAppointmentAsync(dto, userId);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred while booking appointment: {ex.Message}");
//            }
//        }

//        // Cancel an appointment
//        [HttpPost("CancelAppointment")]
//        public async Task<IActionResult> CancelAppointment([FromBody] _02_BusinessLogicLayer.DTOs.PatientDTOs.CancelAppointmentDTO dto)
//        {
//            try
//            {
//                var userId = GetAppUserId();
//                var result = await _patientService.CancelAppointmentAsync(dto, userId);
//                if (!result)
//                    return BadRequest("Failed to cancel appointment.");
//                return Ok("Appointment cancelled successfully.");
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred while cancelling appointment: {ex.Message}");
//            }
//        }
//    }
//}

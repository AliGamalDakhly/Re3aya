using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] PatientDTO patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPatient = await _patientService.AddPatientAsync(patientDto);
            return CreatedAtAction(nameof(GetPatientById), new { id = createdPatient.PatientId }, createdPatient);
        }

        
        /// Update an existing patient by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientDTO patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedPatient = await _patientService.UpdatePatientAsync(id, patientDto);
            return Ok(updatedPatient);
        }

     
        /// Get a patient by ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        
        // Get all patients.
        
        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

       
        // Get a patient by name (query).
        [HttpGet("by-name")]
        public async Task<IActionResult> GetPatientByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required.");

            var patient = await _patientService.GetPatientByNameAsync(name);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        
        // Get a patient by user ID (query).
       
        [HttpGet("by-userid")]
        public async Task<IActionResult> GetPatientByUserId([FromQuery] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest("UserId is required.");

            var patient = await _patientService.GetPatientByUserIdAsync(userId);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        
        /// Delete a patient by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var deleted = await _patientService.DeletePatientAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

 
        // Check if patient exists by user ID
        [HttpGet("exists/{userId}")]
        public async Task<IActionResult> IsPatientExist(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest("UserId is required.");

            var exists = await _patientService.IsPatientExist(userId);
            return Ok(exists);
        }
    }
}

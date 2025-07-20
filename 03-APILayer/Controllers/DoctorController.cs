using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }



        /// <summary>
        /// Get all doctors
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllAsync();
            return Ok(doctors);
        }

        /// <summary>
        /// Get a doctor by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound($"Doctor with ID {id} not found.");
            return Ok(doctor);
        }

        /// <summary>
        /// Update a doctor's info
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] DoctorUpdateDTO dto)
        {
            if (dto == null) return BadRequest("Invalid data.");

            var updated = await _doctorService.UpdateDoctorAsync(id, dto);
            return Ok(updated);
        }

        /// <summary>
        /// Delete a doctor by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var deleted = await _doctorService.DeleteDoctorByIdAsync(id);
            if (!deleted) return NotFound($"Doctor with ID {id} not found.");
            return NoContent();
        }

        /// <summary>
        /// Activate a doctor account
        /// </summary>
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateDoctor(int id)
        {
            var success = await _doctorService.ActivateDoctorAccountAsync(id);
            if (!success) return NotFound($"Doctor with ID {id} not found.");
            return NoContent();
        }

        /// <summary>
        /// Deactivate a doctor account
        /// </summary>
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateDoctor(int id)
        {
            var success = await _doctorService.DeActivateDoctorAccountAsync(id);
            if (!success) return NotFound($"Doctor with ID {id} not found.");
            return NoContent();
        }

        /// <summary>
        /// Count doctors
        /// </summary>
        [HttpGet("count")]
        public async Task<IActionResult> CountDoctors()
        {
            var count = await _doctorService.CountDoctorsAsync();
            return Ok(count);
        }

        /// <summary>
        /// Check if a doctor exists
        /// </summary>
        [HttpGet("{id}/exists")]
        public async Task<IActionResult> ExistsDoctor(int id)
        {
            var exists = await _doctorService.ExistsDoctorAsync(id);
            return Ok(exists);
        }
    }
}

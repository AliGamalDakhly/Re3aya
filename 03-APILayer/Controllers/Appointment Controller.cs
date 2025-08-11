using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.AppointmentDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Appointment_Controller : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public Appointment_Controller(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        private string GetAppUserId()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User ID not found in token.");
            return userId;
        }

        [HttpGet("get-all-appointments")]
        public async Task<IActionResult> GetAllAppointments()
        {

            try
            {
                List<AppointmentDTO> appointments = await _appointmentService.GetAllAppointmentsAsync();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get-appointment-by-id/{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("add-appointment")]
        public async Task<IActionResult> AddAppointment(AppointmentDTO appointmentDto)
        {
            try
            {
                if (appointmentDto == null)
                {
                    return BadRequest("Appointment data is null.");
                }
                var addedAppointment = await _appointmentService.AddAppointmentAsync(appointmentDto);
                return Ok(addedAppointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("book-appointment")]
        public async Task<IActionResult> BookAppointment(BookAppointment appointmentDto)
        {
            try
            {
                if (appointmentDto == null)
                {
                    return BadRequest("Appointment data is null.");
                }
                var addedAppointment = await _appointmentService.BookAppointmentAsync(appointmentDto);
                return Ok(addedAppointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("update-appointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDTO appointmentDto)
        {
            try
            {
                if (appointmentDto == null)
                {
                    return BadRequest("Invalid appointment data.");
                }
                var updated = await _appointmentService.UpdateAppointmentAsync(appointmentDto, id);
                if (!updated)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete-appointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                var deleted = await _appointmentService.DeleteAppointmentByIdAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete-appointment-by-object/{id}")]
        public async Task<IActionResult> DeleteAppointmentByObject(int id, [FromBody] AppointmentDTO appointmentDto)
        {
            try
            {
                if (appointmentDto == null)
                {
                    return BadRequest("Invalid appointment data.");
                }
                var deleted = await _appointmentService.DeleteAppointmentAsync(appointmentDto, id);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("appointments/{id}/create-room")]
        public async Task<IActionResult> CreateRoomForAppointment(int id)
        {
            try
            {
                string roomUrl = await _appointmentService.CreateRoomForAppointment(id);
                if (roomUrl == null)
                    return BadRequest();

                return Ok(roomUrl);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("appointment/JoinRoom/{id}")]
        public async Task<IActionResult> JoinRoom(int id) // appointmentId
        {
            try
            {
                string appUserId = GetAppUserId();
                if (appUserId == null)
                    return Unauthorized();

                string meetingLink = await _appointmentService.JoinRoom(id, appUserId);
                return Ok(new { url = meetingLink }); // important: return object with "url"
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("appointment/AddNotes/{id}")]
        public async Task<IActionResult> AddNotes(int id, [FromBody] AddNotesDTO notesDto) // appointmentId
        {
            try
            {
                string appUserId = GetAppUserId();
                if (appUserId == null)
                    return Unauthorized();

                string meetingLink = await _appointmentService.AddNotesAsync(notesDto.Notes, id, appUserId);
                return Ok(new { url = meetingLink }); // important: return object with "url"
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }




        [HttpGet("patient/{id}/appointments")]
        public async Task<IActionResult> GetAppointmentsByPatientId(int id)
        {
            var appointments = await _appointmentService.GetAppointmentsByPatientIdAsync(id);
            return Ok(appointments);
        }


        // Doctor's side of appointments

        
        //[Authorize(Roles = "Doctor")]
        [HttpGet("doctor/{doctorId}/appointments")]
        public async Task<IActionResult> GetDoctorAppointments(int doctorId)
        {
            var appointments = await _appointmentService.GetAppointmentsByDoctorIdAsync(doctorId);

            if (appointments == null || !appointments.Any())
                return NotFound("No appointments found for this doctor.");

            return Ok(appointments);
        }

        //[Authorize]
        [HttpPut("doctor/appointments/{id}/status")]
        public async Task<IActionResult> UpdateAppointmentStatus(int id, [FromBody] UpdateAppointmentStatusDTO dto)
        {
            try
            {
                var success = await _appointmentService.UpdateAppointmentStatusAsync(id, dto.Status, dto.DoctorId);
                if (!success)
                    return NotFound();

                return Ok(new { message = "Status updated" });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



    }
}

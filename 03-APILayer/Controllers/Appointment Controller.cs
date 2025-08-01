using _02_BusinessLogicLayer.DTOs.AppointmentDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}

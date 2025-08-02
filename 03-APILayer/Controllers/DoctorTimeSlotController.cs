using _02_BusinessLogicLayer.DTOs.DoctorTimeSlotDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorTimeSlotController : ControllerBase
    {
        private readonly IDoctorTimeSlotService _doctorTimeSlotService;

        public DoctorTimeSlotController(IDoctorTimeSlotService doctorTimeSlotService)
        {
            _doctorTimeSlotService = doctorTimeSlotService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorTimeSlotCreateDTO dto)
        {
            var created = await _doctorTimeSlotService.AddDoctorTimeSlotAsync(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DoctorTimeSlotCreateDTO dto)
        {
            bool result = await _doctorTimeSlotService.UpadateDoctorTimeSlotAsync(dto, id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorTimeSlotService.DeleteDoctorTimeSlotAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(int doctorId)
        {
            var result = await _doctorTimeSlotService.GetAllDoctorTimeSlotsAsync(doctorId);
            return Ok(result);
        }

        [HttpGet("available/{doctorId}")]
        public async Task<IActionResult> GetAvailable(int doctorId, [FromQuery] DateOnly date)
        {
            var result = await _doctorTimeSlotService.AvailableDoctorTimeSlots(doctorId, date);
            return Ok(result);
        }

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> Activate(int id)
        {
            var result = await _doctorTimeSlotService.ActivateDoctorTimeSlotAsyncAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _doctorTimeSlotService.DeactivateDoctorTimeSlotAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("has-available/{doctorId}")]
        public async Task<IActionResult> HasAvailable(int doctorId)
        {
            var result = await _doctorTimeSlotService.HasAvailableTimeSlots(doctorId);
            return Ok(result);
        }

        [HttpGet("has-available-by-date/{doctorId}")]
        public async Task<IActionResult> HasAvailableByDate(int doctorId, [FromQuery] DateTime date)
        {
            var result = await _doctorTimeSlotService.HasAvailableTimeSlots(doctorId, date);
            return Ok(result);
        }
    }
}

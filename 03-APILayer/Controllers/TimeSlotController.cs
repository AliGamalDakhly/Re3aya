using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.TimeSlotDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotService _timeSlotService;
        private readonly IDoctorTimeSlotService _doctorTimeSlotService;
        public TimeSlotController(ITimeSlotService timeSlotService,
            IDoctorTimeSlotService doctorTimeSlotService)
        {
            _timeSlotService = timeSlotService;
            _doctorTimeSlotService = doctorTimeSlotService;
        }

        //Create TimeSlot
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateTimeSlotDTO dto)
        {
            try
            {
                var Created = await _timeSlotService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = Created.TimeSlotId }, Created);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error while Creating TimeSlot : {ex.Message}");
            }
        }

        //Get timeSlot By Id
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var entity = await _timeSlotService.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound($"No TimmeSlot Found whith {id}");
                }
                return Ok(entity);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error while Retriving TimeSlot {ex.Message}");
            }

        }


        //delete TimeSlot by id
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _timeSlotService.DeleteByIdAsync(id);
                if (!deleted)
                {
                    return NotFound($"No TimeSlot Found With {id}");
                }
                else
                {
                    return Ok("TimeSlote Deleted Succrssfully");
                }

            }
            catch(Exception ex)
            {
                return BadRequest($"Error with delete TimeSlot{ex.Message}");

            }
        }


        //Update TimeSlote
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateTimeSlot([FromBody] EditTimeSlotDTO dto)
        {
            try
            {
                var Updated = await _timeSlotService.UpdateAsync(dto);
                if(!Updated)
                {
                    return NotFound($"No TimeSlot Found with {dto.TimeSlotId}");
                }
                else
                {
                    return Ok("TimeSlot Updated Successfully ");
                }


            }catch(Exception ex)
            {
                return BadRequest($"Error on Updating TimeSlot {ex.Message}");
            }


        }

        //Get All
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _timeSlotService.GetAllAsync(new QueryOptions<TimeSlot>
                {
                    Filter = ts => ts.StartTime >= DateTime.Now
                });
                return Ok(result);

            }catch(Exception ex)
            {
                return BadRequest($"Error on Retriving All TimeSlots {ex.Message}");
            }
        }

        //Get Available for specific Doctor with doctorId
        //[HttpGet("available/{id}")]
        //public async Task<IActionResult> GetAvailableTimeSlotsForDoctor(int id)
        //{
        //    try
        //    {
        //        var result = await _doctorTimeSlotService.AvailableDoctorTimeSlots(id);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error on Retriving All TimeSlots {ex.Message}");
        //    }
        //}

        [HttpGet("available/{id}")]
        public async Task<IActionResult> GetAvailableTimeSlotsForDoctor(int id, DateOnly date)
        {
            try
            {
                var result = await _doctorTimeSlotService.AvailableDoctorTimeSlots(id, date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error on Retriving All TimeSlots {ex.Message}");
            }
        }



        [HttpGet("isAvailable/{id}")]
        public async Task<IActionResult> HasAvailableTImeSlots(int id, DateTime date)
        {
            try
            {
                var result = await _doctorTimeSlotService.HasAvailableTimeSlots(id, date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error on Retriving All TimeSlots {ex.Message}");
            }
        }


    }
}
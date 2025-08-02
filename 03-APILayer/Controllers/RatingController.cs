using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.RatingDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("doctor/{id}")]
        public async Task<IActionResult> GetAllDoctorRatings(int id)
        {
            try
            {
               var ratings = await _ratingService.GetAllDoctorRatings(id);

                if (ratings == null)
                {
                    return NotFound($"No ratings found for doctor with ID {id}.");
                }
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving ratings: {ex.Message}");
            }
        }
    }
}

using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.AddressDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("governments")]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                List<Government> governments = await _addressService.GetAllGovernmentsAsync();

                // Assuming you want to map the Government entities to GovernmentDTOs.
                List<GovernmentDTO> governmentDTOs = governments.Select(g => new GovernmentDTO
                {
                    Name = g.Name
                }).ToList();

                return Ok(governmentDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }

        [HttpPost("CreateGovernment")]
        public async Task<IActionResult> CreateGovernment(GovernmentDTO governmentFromReq)
        {
            try
            {
                // Map the DTO to the entity
                Government government = new Government
                {
                    Name = governmentFromReq.Name

                };
                // Call the service to add the government
                Government addedGovernment = await _addressService.AddGovernmentAsync(government);
                // Return the added government as a DTO
                return CreatedAtAction(nameof(GetAll), new GovernmentDTO { Name = addedGovernment.Name });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }

        [HttpPut("government/{id}")]
        public async Task<IActionResult> UpdateGovernment(GovernmentDTO governmentFromReq, int id)
        {
            try
            {
                // Map the DTO to the entity
                Government government = new Government
                {
                    GovernmentId = id,
                    Name = governmentFromReq.Name
                };
                // Call the service to update the government
                bool isUpdated = await _addressService.UpdateGovernmentAsync(government);
                if (isUpdated)
                    return NoContent(); // 204 No Content
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetAllCities()
        {
            try
            {
                List<City> cities = await _addressService.GetAllCitiesAsync();
                // Assuming you want to map the City entities to CityDTOs.
                List<CityDTO> cityDTOs = cities.Select(c => new CityDTO
                {
                    Name = c.Name,
                    GovernmentId = c.GovernmentId
                }).ToList();
                return Ok(cityDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }

        [HttpPost("CreateCity")]
        public async Task<IActionResult> CreateCity(CityDTO cityFromReq)
        {
            try
            {
                // Map the DTO to the entity
                City city = new City
                {
                    Name = cityFromReq.Name,
                    GovernmentId = cityFromReq.GovernmentId
                };
                // Call the service to add the city
                City addedCity = await _addressService.AddCityAsync(city);
                // Return the added city as a DTO
                return CreatedAtAction(nameof(GetAllCities), new CityDTO { Name = addedCity.Name, GovernmentId = addedCity.GovernmentId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }

        [HttpPut("city/{id}")]
        public async Task<IActionResult> UpdateCity(CityDTO cityFromReq, int id)
        {
            try
            {
                // Map the DTO to the entity
                City city = new City
                {
                    CityId = id,
                    Name = cityFromReq.Name,
                    GovernmentId = cityFromReq.GovernmentId
                };
                // Call the service to update the city
                bool isUpdated = await _addressService.UpdateCityAsync(city);
                if (isUpdated)
                    return NoContent(); // 204 No Content
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }
    }
}

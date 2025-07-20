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

                List<GovernmentDTO> governmentDTOs = await _addressService.GetAllGovernmentsAsync();

                // Assuming you want to map the Government entities to GovernmentDTOs.


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

                // Call the service to add the government
                GovernmentDTO addedGovernment = await _addressService.AddGovernmentAsync(governmentFromReq);
                // Return the added government as a DTO
                return Ok(addedGovernment);
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

                // Call the service to update the government
                bool isUpdated = await _addressService.UpdateGovernmentAsync(governmentFromReq, id);
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
                List<CityDTO> cityDTOs = await _addressService.GetAllCitiesAsync();
                // Assuming you want to map the City entities to CityDTOs.

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

                // Call the service to add the city
                CityDTO addedCity = await _addressService.AddCityAsync(cityFromReq);
                // Return the added city as a DTO
                return Ok(addedCity);
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

                // Call the service to update the city
                bool isUpdated = await _addressService.UpdateCityAsync(cityFromReq, id);
                if (isUpdated)
                    return NoContent(); // 204 No Content
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }

        [HttpGet("addresses")]
        public async Task<IActionResult> GetAllAddresses()
        {
            try
            {
                List<AddressDTO> addressDTOs = await _addressService.GetAllAddressesAsync();
                // Assuming you want to map the Address entities to AddressDTOs.
                return Ok(addressDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }

        [HttpPost("CreateAddress")]
        public async Task<IActionResult> CreateAddress(AddressDTO addressFromReq)
        {
            try
            {
                // Call the service to add the address
                AddressDTO addedAddress = await _addressService.AddAddressAsync(addressFromReq);
                // Return the added address as a DTO
                return Ok(addedAddress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }
        [HttpPut("address/{id}")]
        public async Task<IActionResult> UpdateAddress(AddressDTO addressFromReq, int id)
        {
            try
            {
                // Call the service to update the address
                bool isUpdated = await _addressService.UpdateAddressAsync(addressFromReq, id);
                if (isUpdated)
                    return NoContent(); // 204 No Content
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }
        [HttpDelete("address/{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                // Call the service to delete the address by ID
                bool isDeleted = await _addressService.DeleteAddressByIdAsync(id);
                if (isDeleted)
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

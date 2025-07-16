using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _02_BusinessLogicLayer.DTOs.SpecailzationDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        // this is Essential Service For Specialization CRUD Operations
        private readonly ISpecializationService _specializationService;

        #region Ctor
        // Constructor Injection of ISpecializationService
        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }
        #endregion

        #region GetAllSpecializations
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Create Empty List Of DTOs 
                List<SpecializationDTO> SpecilizationDTOs = new List<SpecializationDTO>();

                // Get All Specializations From DB
                List<Specialization> specializations =
                    await _specializationService.GetAllAsync();

                // Map Each Specialization To SpecializationDTO
                foreach (Specialization specialization in specializations)
                {
                    SpecializationDTO SpecilizationDTO = new SpecializationDTO
                    {
                        Name = specialization.Name,
                        Description = specialization.Description
                    };

                    SpecilizationDTOs.Add(SpecilizationDTO);
                }

                // Return List Of SpecializationDTOs
                return Ok(SpecilizationDTOs);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
            
        }
        #endregion

        #region Create Specialization
        [HttpPost]
        public async Task<IActionResult> Create(SpecializationDTO specializationFromReq)
        {
            try
            {
                // Create a new Specialization object and map the properties from the DTO
                Specialization specialization = new Specialization
                {
                    Name = specializationFromReq.Name,
                    Description = specializationFromReq.Description
                };

                // Call the service to add the specialization int DB
                Specialization addedSpecialization =
                    await _specializationService.AddAsync(specialization);

                // Check if the addition was successful
                if (addedSpecialization != null)
                    return Ok(addedSpecialization);

                // If the addition failed, return a BadRequest response
                return BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }      
        }
        #endregion

        #region Update Specialization
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(SpecializationDTO specializationFromReq, int id)
        {
            try
            {
                // Map the properties from the DTO to a new Specialization object with ID sent in URL
                Specialization specialization = new Specialization
                {
                    SpecializationId = id,
                    Name = specializationFromReq.Name,
                    Description = specializationFromReq.Description
                };

                // Call the service to update the specialization in DB
                bool isUpdated =
                    await _specializationService.UpdateAsync(specialization);

                // Check if the update was successful
                if (isUpdated)
                    return Ok(isUpdated);

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }
        #endregion

        #region Delete Specialization
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Call the service to delete the specialization by ID
                bool isDeleted = await _specializationService.DeleteByIdAsync(id);

                // Check if the deletion was successful
                if (isDeleted)
                    return Ok(isDeleted);
                return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }
        }
        #endregion

        #region Get Specialization By ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                // Call the service to get the specialization by ID
                Specialization specialization = await _specializationService.GetByIdAsync(id);

                // Check if the specialization exists
                if (specialization != null)
                {
                    // Map the properties to a DTO
                    SpecializationDTO specializationDTO = new SpecializationDTO
                    {
                        Name = specialization.Name,
                        Description = specialization.Description
                    };

                    return Ok(specializationDTO);
                }

                return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Unexpected error: " + ex.Message);
            }         
        }
        #endregion
    }
}

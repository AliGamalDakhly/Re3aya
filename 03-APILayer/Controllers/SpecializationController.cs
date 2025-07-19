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
                // Get All Specializations From Service
                List<SpecializationDTO> SpecilizationDTOs = 
                    await _specializationService.GetAllAsync();

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
                // Call the service to add the specialization int DB
                SpecializationDTO addedSpecialization =
                    await _specializationService.AddAsync(specializationFromReq);

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

                // Call the service to update the specialization in DB
                bool isUpdated =
                    await _specializationService.UpdateAsync(specializationFromReq, id);

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
                SpecializationDTO specializationDTO = 
                    await _specializationService.GetByIdAsync(id);

                // Check if the specialization exists
                if (specializationDTO != null)
                    return Ok(specializationDTO);

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

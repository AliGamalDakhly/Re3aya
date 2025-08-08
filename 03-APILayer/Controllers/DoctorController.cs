using _02_BusinessLogicLayer.DTOs.DoctorDTOs;
using _02_BusinessLogicLayer.DTOs.DocumentDTO;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private static readonly Dictionary<int, int> _doctorViewCounts = new Dictionary<int, int>();

        private readonly IDoctorService _doctorService;
        private readonly IDocumentService _documentService;



        public DoctorController(IDoctorService doctorService, IDocumentService documentService)
        {
            _doctorService = doctorService;
            _documentService = documentService;
        }

        #region Doctor Endpoints

        /// <summary>
        /// Get all  active doctors
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllAsync();
            return Ok(doctors);
        }

        [HttpGet("allDoctors")]
        public async Task<IActionResult> GetAllDoctorsWithDetails()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
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

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetDoctorDetailsById(int id)
        {
            var doctor = await _doctorService.GetDoctorDetailsByIdAsync(id);
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
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ActivateDoctor(int id)
        {
            var success = await _doctorService.ApproveDoctorAccountAsync(id);
            if (!success) return NotFound($"Doctor with ID {id} not found.");
            return NoContent();
        }
        /// <summary>
        /// Activate a doctor account
        /// </summary>
        [HttpPut("{id}/suspend")]
        public async Task<IActionResult> SuspendDoctor(int id)
        {
            var success = await _doctorService.SuspendDoctorAccountAsync(id);
            if (!success) return NotFound($"Doctor with ID {id} not found.");
            return NoContent();
        }
        /// <summary>
        /// Activate a doctor account
        /// </summary>
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectDoctor(int id)
        {
            var success = await _doctorService.RejectDoctorAccountAsync(id);
            if (!success) return NotFound($"Doctor with ID {id} not found.");
            return NoContent();
        }
        /// <summary>
        /// Activate a doctor account
        /// </summary>
        [HttpPut("{id}/pending")]
        public async Task<IActionResult> PendingDoctor(int id)
        {
            var success = await _doctorService.PendingDoctorAccountAsync(id);
            if (!success) return NotFound($"Doctor with ID {id} not found.");
            return NoContent();
        }

        /// <summary>
        /// Deactivate a doctor account
        /// </summary>
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateDoctor(int id)
        {
            var success = await _doctorService.DeactivatedDoctorAccountAsync(id);
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



        /// <summary>
        /// update balance for doctor after every appointment
        /// </summary>
        [HttpPut("{doctorId}/balance")]
        public async Task<IActionResult> UpdateDoctorBalance(int doctorId, [FromQuery] double amount)
        {
            if (amount <= 0)
                return BadRequest("amount must be greater than zero");

            // deducted 10%  
            double companyShare = amount * 0.10;
            double doctorAmount = amount - companyShare;

            var success = await _doctorService.UpdateDoctorBalanceAsync(doctorId, doctorAmount);
            if (!success)
                return NotFound($"doctor with ID {doctorId} not found");

            return Ok(new
            {
                Message = $"doctor balance increased by {doctorAmount} EGP, 10% deducted for care|Re3aya company",
                DoctorId = doctorId,
                OriginalAmount = amount,
                DoctorReceived = doctorAmount,
                CompanyShare = companyShare
            });
        }


        [HttpPost("{doctorId}/deduct-balance")]
        public async Task<IActionResult> DeductDoctorBalance(int doctorId, [FromBody] double amount)
        {
            // deducted 10%  
            double discountedAmount = amount * 0.9;

            var result = await _doctorService.DeductDoctorBalanceAsync(doctorId, discountedAmount);
            if (!result)
                return BadRequest("insufficient funds or doctor not available");

            return Ok(new { message = $"deducted {discountedAmount} EGP - 10% deducted applied for care|Re3aya company" });
        }





        [HttpGet("{id}/view")]
        public IActionResult TrackDoctorView(int id)
        {
            if (_doctorViewCounts.ContainsKey(id))
                _doctorViewCounts[id]++;
            else
                _doctorViewCounts[id] = 1;

            return Ok(new { doctorId = id, views = _doctorViewCounts[id] });
        }


        [HttpGet("{id}/view-v2")]
        public async Task<IActionResult> GetDoctorByIdWithViewCount(int id)
        {

            if (_doctorViewCounts.ContainsKey(id))
                _doctorViewCounts[id]++;
            else
                _doctorViewCounts[id] = 1;

            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound();

            return Ok(new
            {
                doctor,
                ViewCount = _doctorViewCounts[id]
            });
        }




        [HttpGet("doctor/{doctorId}/appointments")]
        public async Task<IActionResult> GetDoctorAppointments(int doctorId)
        {
            var appointments = await _doctorService.GetAppointmentsByDoctorIdAsync(doctorId);
            return Ok(appointments);
        }



        #endregion




        #region Document Endpoints

        /// <summary>
        /// Add ONE document for Doctor
        /// </summary>
        [HttpPost("{doctorId}/document")]
        public async Task<IActionResult> AddDocumentForDoctor(int doctorId, [FromBody] DocumentDTO dto)
        {
            try
            {
                dto.DoctorId = doctorId;
                var result = await _documentService.AddDocumentAsync(dto);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Add MULTIPLE documents for Doctor
        /// </summary>
        [HttpPost("{doctorId}/documents")]
        public async Task<IActionResult> AddDocumentsForDoctor(int doctorId, [FromBody] List<DocumentDTO> dtos)
        {
            try
            {
                foreach (var dto in dtos) dto.DoctorId = doctorId;

                foreach (var dto in dtos)
                    await _documentService.AddDocumentAsync(dto);

                return Ok("Documents Added");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        /// <summary>
        /// Update Document
        /// </summary>
        [HttpPut("document/{documentId}")]
        public async Task<IActionResult> UpdateDocument(int documentId, [FromBody] DocumentDTO dto)
        {
            try
            {
                dto.DocumentId = documentId;
                var result = await _documentService.UpdateDocumentAsync(dto);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        // <summary>
        /// Delete Document
        /// </summary>
        [HttpDelete("document/{documentId}")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            try
            {
                var result = await _documentService.DeleteDocumentByIdAsync(documentId);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        #endregion





    }
}

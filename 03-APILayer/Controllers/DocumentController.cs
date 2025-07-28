using _02_BusinessLogicLayer.DTOs.Common;
using _02_BusinessLogicLayer.DTOs.DocumentDTO;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IDoctorService _doctorService;


        public DocumentController(
            IDocumentService documentService,
            ICloudinaryService cloudinaryService,
            IDoctorService doctorService
        )
        {
            _documentService = documentService;
            _cloudinaryService = cloudinaryService;
            _doctorService = doctorService;

        }

        /// <summary>
        /// Add a new document
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddDocument([FromBody] DocumentDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _documentService.AddDocumentAsync(dto);
            return CreatedAtAction(nameof(GetDocumentById), new { id = result.DocumentId }, result);
        }

        /// <summary>
        /// Get all documents
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var result = await _documentService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get a document by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var result = await _documentService.GetDocumentByIdAsync(id);
            if (result == null) return NotFound($"Document with ID {id} not found.");
            return Ok(result);
        }

        /// <summary>
        /// Update an existing document
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateDocument([FromBody] DocumentDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _documentService.UpdateDocumentAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Delete a document by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentById(int id)
        {
            var deleted = await _documentService.DeleteDocumentByIdAsync(id);
            if (!deleted) return NotFound($"Document with ID {id} not found.");
            return NoContent();
        }

        /// <summary>
        /// Delete a document by DTO (optional)
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteDocument([FromBody] DocumentDTO dto)
        {
            var deleted = await _documentService.DeleteDocumentAsync(dto);
            if (!deleted) return NotFound($"Document with ID {dto.DocumentId} not found.");
            return NoContent();
        }

        /// <summary>
        /// Verify a document
        /// </summary>
        [HttpPut("{id}/verify")]
        public async Task<IActionResult> VerifyDocument(int id)
        {
            var verified = await _documentService.VerifyDocumentAsync(id);
            if (!verified) return NotFound($"Document with ID {id} not found.");
            return Ok($"Document with ID {id} verified successfully.");
        }

        /// <summary>
        /// Count total documents
        /// </summary>
        [HttpGet("count")]
        public async Task<IActionResult> CountDocuments()
        {
            var count = await _documentService.CountDocumentAsync();
            return Ok(count);
        }

        /// <summary>
        /// Check if a document exists
        /// </summary>
        [HttpGet("{id}/exists")]
        public async Task<IActionResult> ExistsDocument(int id)
        {
            var exists = await _documentService.ExistsDocumentAsync(id);
            return Ok(exists);
        }




        //// ****************************************** cloudinary uploading system ***************************************************//// 
        ///
        /// 
        /// <summary>
        /// Upload document or img file to cloudinary and save to care db
        /// </summary>
        /// 

        [HttpPost("upload")]
        public async Task<IActionResult> AddDocumentWithFile([FromForm] DocumentUploadDTO dto, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new GeneralResponse<string>
                {
                    Status = "failed",
                    StatusCode = 400,
                    IsSuccess = false,
                    StatusDetails = "no file uploaded",
                    Data = null
                });
            }

            string fileUrl = await _cloudinaryService.UploadFileAsync(file);

            var documentDto = new DocumentDTO
            {
                FilePath = fileUrl,
                DocumentType = dto.DocumentType,
                DoctorId = dto.DoctorId,
                UploadedAt = DateTime.UtcNow,
                IsVerified = false
            };

            var result = await _documentService.AddDocumentAsync(documentDto);

            // get doctor full name
            string? doctorName = await _doctorService.GetDoctorFullNameByIdAsync(dto.DoctorId);

            var response = new GeneralResponse<object>
            {
                Status = "success",
                StatusCode = 200,
                IsSuccess = true,
                StatusDetails = "document uploaded successfully to cloudinary and care db",
                Data = new
                {
                    result.DocumentId,
                    result.DocumentType,
                    result.IsVerified,
                    result.FilePath,
                    result.UploadedAt,
                    result.DoctorId,
                    DoctorFullName = doctorName,

                },
                ApiUsage = "this api endpoint is under development for Re3aya|care by Abdallah Mokarb"
            };

            return CreatedAtAction(nameof(GetDocumentById), new { id = result.DocumentId }, response);
        }



        [HttpPut("upload/edit")]
        public async Task<IActionResult> EditDocumentWithNewFile([FromForm] DocumentEditDTO dto, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new GeneralResponse<string>
                {
                    Status = "Failed",
                    StatusCode = 400,
                    IsSuccess = false,
                    StatusDetails = "No file uploaded",
                    Data = null
                });
            }

            // upload new file to cloudinary
            string newFileUrl = await _cloudinaryService.UploadFileAsync(file);

            // update the link and doc type in db 
            var updatedDoc = await _documentService.UpdateLinkOnlyAsync(dto.DocumentId, newFileUrl, dto.DocumentType);


            return Ok(new GeneralResponse<object>
            {
                Status = "success",
                StatusCode = 200,
                IsSuccess = true,
                StatusDetails = "document link updated successfully in DB",
                Data = updatedDoc,
                ApiUsage = "this api endpoint is under development for Re3aya|care by Abdallah Mokarb"
            });
        }








    }
}

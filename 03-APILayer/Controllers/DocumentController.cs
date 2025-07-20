using _02_BusinessLogicLayer.DTOs.DocumentDTO;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
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
    }
}

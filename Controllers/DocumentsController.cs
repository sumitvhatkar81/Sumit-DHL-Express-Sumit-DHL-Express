
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DHL_Document_App.Models;
using DHL_Document_App.Services;
using System;
using System.IO;
using System.Linq;

namespace DHL_Document_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            return Ok(await _documentService.GetAllDocumentsAsync());
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        // POST: api/Documents
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(Document document)
        {
            await _documentService.AddDocumentAsync(document);
            return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, document);
        }

        // POST: api/Documents/upload
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Document>> UploadDocument(IFormFile file, [FromForm] string name = null)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { error = "No file uploaded or file is empty" });
            }

            // Validate file type - accept common image formats
            var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/bmp", "image/webp" };
            if (!allowedTypes.Contains(file.ContentType.ToLower()))
            {
                return BadRequest(new { error = $"Unsupported file type: {file.ContentType}. Allowed types: {string.Join(", ", allowedTypes)}" });
            }

            // Validate file size (limit to 10MB)
            const int maxFileSize = 10 * 1024 * 1024; // 10MB
            if (file.Length > maxFileSize)
            {
                return BadRequest(new { error = $"File size exceeds maximum limit of {maxFileSize / (1024 * 1024)}MB" });
            }

            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                var document = new Document
                {
                    Name = name ?? file.FileName,
                    Content = memoryStream.ToArray(),
                    UploadedAt = DateTime.UtcNow
                };

                await _documentService.AddDocumentAsync(document);
                return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, new 
                {
                    id = document.Id,
                    name = document.Name,
                    uploadedAt = document.UploadedAt,
                    size = document.Content.Length,
                    contentType = file.ContentType
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while uploading the file", details = ex.Message });
            }
        }

        // PUT: api/Documents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            await _documentService.UpdateDocumentAsync(document);

            return NoContent();
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            await _documentService.DeleteDocumentAsync(id);
            return NoContent();
        }
    }
}

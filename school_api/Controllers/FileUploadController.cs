using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using System.Security.Cryptography;
using System.Text;

namespace school_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileUploadController : ControllerBase
    {
        private readonly IMainRepoistory<FileUpload> _fileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        public FileUploadController(
            IMainRepoistory<FileUpload> fileRepository,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment environment)
        {
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No file uploaded");

            // Validate file size (max 10MB)
            if (dto.File.Length > 10 * 1024 * 1024)
                return BadRequest("File size cannot exceed 10MB");

            // Validate file type
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "application/pdf", "text/plain" };
            if (!allowedTypes.Contains(dto.File.ContentType))
                return BadRequest("File type not allowed");

            try
            {
                // Create uploads directory if it doesn't exist
                var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                // Generate unique filename
                var fileExtension = Path.GetExtension(dto.File.FileName);
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadsPath, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.File.CopyToAsync(stream);
                }

                // Save file info to database
                var userId = int.Parse(User.FindFirst("NameIdentifier")?.Value ?? "0");
                var fileUpload = new FileUpload
                {
                    FileName = dto.File.FileName,
                    FilePath = $"/uploads/{fileName}",
                    ContentType = dto.File.ContentType,
                    FileSize = dto.File.Length,
                    Description = dto.Description,
                    Category = dto.Category,
                    UploadedBy = userId,
                    UploadedAt = DateTime.Now
                };

                await _fileRepository.CreateAsync(fileUpload);
                await _unitOfWork.SaveChangesAsync();

                return Ok(new FileResponseDto
                {
                    Id = fileUpload.Id,
                    FileName = fileUpload.FileName,
                    FilePath = fileUpload.FilePath,
                    ContentType = fileUpload.ContentType,
                    FileSize = fileUpload.FileSize,
                    Description = fileUpload.Description,
                    Category = fileUpload.Category,
                    UploadedAt = fileUpload.UploadedAt,
                    UploadedBy = fileUpload.UploadedBy
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var file = await _fileRepository.GetByIdAsync(id);
            if (file == null)
                return NotFound();

            var filePath = Path.Combine(_environment.WebRootPath, file.FilePath.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found on disk");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, file.ContentType, file.FileName);
        }

        [HttpGet]
        public async Task<IActionResult> GetFiles([FromQuery] string? category = null)
        {
            var files = await _fileRepository.GetAllAsync();
            
            if (!string.IsNullOrEmpty(category))
                files = files.Where(f => f.Category == category);

            var fileDtos = files.Select(f => new FileResponseDto
            {
                Id = f.Id,
                FileName = f.FileName,
                FilePath = f.FilePath,
                ContentType = f.ContentType,
                FileSize = f.FileSize,
                Description = f.Description,
                Category = f.Category,
                UploadedAt = f.UploadedAt,
                UploadedBy = f.UploadedBy
            }).ToList();

            return Ok(fileDtos);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var file = await _fileRepository.GetByIdAsync(id);
            if (file == null)
                return NotFound();

            try
            {
                // Delete file from disk
                var filePath = Path.Combine(_environment.WebRootPath, file.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                // Delete from database
                await _fileRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

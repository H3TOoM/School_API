using System.ComponentModel.DataAnnotations;

namespace school_api.DTOs
{
    public class FileUploadDto
    {
        [Required(ErrorMessage = "File is required")]
        public IFormFile File { get; set; } = null!;
        
        public string? Description { get; set; }
        public string? Category { get; set; }
    }

    public class FileResponseDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public DateTime UploadedAt { get; set; }
        public int UploadedBy { get; set; }
    }
}

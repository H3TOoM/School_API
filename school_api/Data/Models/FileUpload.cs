using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class FileUpload
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ContentType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Category { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;

        [Required]
        public int UploadedBy { get; set; }

        // Navigation property
        public User? Uploader { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Action { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string EntityType { get; set; } = string.Empty;

        public int? EntityId { get; set; }

        [StringLength(500)]
        public string? OldValues { get; set; }

        [StringLength(500)]
        public string? NewValues { get; set; }

        [Required]
        public int UserId { get; set; }

        [StringLength(45)]
        public string? IpAddress { get; set; }

        [StringLength(500)]
        public string? UserAgent { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Navigation property
        public User? User { get; set; }
    }
}

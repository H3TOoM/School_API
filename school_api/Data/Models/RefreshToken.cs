using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; } 

        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRevoked { get; set; } = false;

        // Navigation property
        public User? User { get; set; }
    }
}

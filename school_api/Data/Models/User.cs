using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength( 100 )]
        public string Name { get; set; }

        [Required]
        [StringLength( 255 )]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength( 100 )]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength( 50 )]
        public string Role { get; set; } // Admin, Teacher, Student, Parent, Staff

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }

}

using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength( 100 )]
        public string Name { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        [Required, StringLength( 100 )]
        public string Address { get; set; } = string.Empty;


        [Required]
        public int UserId { get; set; }   // 🔑 Foreign Key

        public User User { get; set; } = null!;

        public ICollection<Student> Students { get; set; } = new List<Student>();

    }
}

using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength( 100 )]
        public string Name { get; set; }

        public string? PhoneNumber { get; set; }

        [Required, StringLength( 100 )]
        public string Address { get; set; }


        [Required]
        public int UserId { get; set; }   // 🔑 Foreign Key

        public User User { get; set; }

        public ICollection<Student> Students { get; set; }

    }
}

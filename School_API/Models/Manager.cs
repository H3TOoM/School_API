using System.ComponentModel.DataAnnotations;

namespace School_API.Models
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; }

        public ICollection<Teacher>? Teachers { get; set; } = new List<Teacher>();
        public ICollection<Staff>? StaffMembers { get; set; } = new List<Staff>();
    }
}
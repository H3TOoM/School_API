using System.ComponentModel.DataAnnotations;

namespace School_API.Models
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; }

        [Required, StringLength(15)]
        public string PhoneNumber { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace School_API.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string Role { get; set; }

        [Required]
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}
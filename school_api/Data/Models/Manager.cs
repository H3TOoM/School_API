using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }


        [Required]
        public int UserId { get; set; }   // 🔑 Foreign Key

        public User User { get; set; }
    }
}

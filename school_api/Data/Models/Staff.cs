using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength( 100 )]
        public string Name { get; set; }

        [Required]
        [StringLength( 50 )]
        public string Position { get; set; }   

        [Required]
        public double Salary { get; set; }

        [StringLength( 20 )]
        public string? PhoneNumber { get; set; }

        [Required]
        public int UserId { get; set; }   // 🔑 Foreign Key

        public User User { get; set; }


        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }

}

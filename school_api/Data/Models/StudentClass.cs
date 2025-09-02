using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class StudentClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength( 50 )]
        public string Name { get; set; }  

        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }

}

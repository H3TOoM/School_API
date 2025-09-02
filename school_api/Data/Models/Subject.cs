using System.ComponentModel.DataAnnotations;

namespace school_api.Data.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength( 100 )]
        public string Name { get; set; }  

        [StringLength( 250 )]
        public string? Description { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
    }

}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace School_API.Models
{
    public class User : IdentityUser<int>

    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }

       
    }
}
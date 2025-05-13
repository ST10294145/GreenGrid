using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using GreenGrid.Models;

namespace GreenGrid.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }  

        [Required]
        public string Province { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Role { get; set; } // "Farmer" or "Employee"
    }
}

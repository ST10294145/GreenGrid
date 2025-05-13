using System.ComponentModel.DataAnnotations;

namespace GreenGrid.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Role { get; set; } // "Farmer" or "Employee"
    }
}

using System.ComponentModel.DataAnnotations;

namespace GreenGrid.Models
{
    public class EnergyProvider
    {
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string ServiceType { get; set; } // e.g., Solar, Wind, Biogas

        [Required]
        public string Province { get; set; }

        [Required]
        public string ContactEmail { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }
    }

}

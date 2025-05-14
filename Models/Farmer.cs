using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenGrid.Models
{
    public class Farmer
    {
        [Key]
        public int FarmerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

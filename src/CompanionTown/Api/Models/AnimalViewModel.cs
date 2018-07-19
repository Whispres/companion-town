using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class AnimalViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public AnimalType Type { get; set; }
    }
}
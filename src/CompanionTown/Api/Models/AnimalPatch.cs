using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class AnimalPatch
    {
        [Required]
        public PropertyName Name { get; set; }

        [Required]
        public int PropertyValue { get; set; }

        public enum PropertyName
        {
            Hungry = 1,
            Happiness = 2
        }
    }
}
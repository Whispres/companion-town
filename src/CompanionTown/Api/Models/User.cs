using System;
using System.ComponentModel.DataAnnotations;
using Api.Extensions;

namespace Api.Models
{
    public class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Identifier
        {
            get
            {
                return this.Name.RemoveSpecialCharacters();
            }
        }
    }
}
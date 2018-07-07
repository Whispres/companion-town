using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
                return RemoveSpecialCharacters(this.Name);
            }
        }

        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
    }
}
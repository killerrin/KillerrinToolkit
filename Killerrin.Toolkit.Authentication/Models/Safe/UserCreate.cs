using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Killerrin.Toolkit.Authentication.Models.Safe
{
    public class UserCreate
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Checks if any of the fields are Empty
        /// </summary>
        /// <returns>Whether any of the fields are Empty</returns>
        public bool IsEmpty() { return string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password); }

    }
}

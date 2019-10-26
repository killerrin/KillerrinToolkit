using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Killerrin.Toolkit.Authentication.Models.Safe
{
    public class UserLoginUsername
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Checks if any of the fields are Empty
        /// </summary>
        /// <returns>Whether any of the fields are Empty</returns>
        public bool IsEmpty() { return string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password); }
    }
}

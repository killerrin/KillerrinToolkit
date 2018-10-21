using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KillerrinToolkit.Authentication.Models.Safe
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
    }
}

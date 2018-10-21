using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KillerrinToolkit.Authentication.Models.Safe
{
    public class UserLoginEmail
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

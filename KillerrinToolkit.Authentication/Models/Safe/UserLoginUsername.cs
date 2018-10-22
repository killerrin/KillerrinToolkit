using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KillerrinToolkit.Authentication.Models.Safe
{
    public class UserLoginUsername
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsEmpty() { return string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password); }
    }
}

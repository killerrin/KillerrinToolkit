using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KillerrinToolkit.Authentication.Models.Safe
{
    public class UserGet
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

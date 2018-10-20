using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KillerrinToolkit.Authentication.Models
{
    public class UserEdit
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        public string NewUsername { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string NewEmail { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}

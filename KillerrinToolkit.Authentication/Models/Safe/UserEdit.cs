using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Killerrin.Toolkit.Authentication.Models.Safe
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

        public bool HasCurrentPassword() { return !string.IsNullOrWhiteSpace(CurrentPassword); }
        public bool HasNewUsername() { return HasCurrentPassword() && !string.IsNullOrWhiteSpace(NewUsername); }
        public bool HasNewEmail() { return HasCurrentPassword() && !string.IsNullOrWhiteSpace(NewEmail); }
        public bool HasNewPassword() { return HasCurrentPassword() && !string.IsNullOrWhiteSpace(NewPassword); }
    }
}

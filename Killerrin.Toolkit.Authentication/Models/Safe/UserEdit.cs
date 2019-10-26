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

        /// <summary>
        /// Checks if there is a Current Password
        /// </summary>
        /// <returns>Whether data exists in the field</returns>
        public bool HasCurrentPassword() { return !string.IsNullOrWhiteSpace(CurrentPassword); }

        /// <summary>
        /// Checks if there is a New Username
        /// </summary>
        /// <returns>Whether data exists in the field</returns>
        public bool HasNewUsername() { return HasCurrentPassword() && !string.IsNullOrWhiteSpace(NewUsername); }

        /// <summary>
        /// Checks if there is a New Email
        /// </summary>
        /// <returns>Whether data exists in the field</returns>
        public bool HasNewEmail() { return HasCurrentPassword() && !string.IsNullOrWhiteSpace(NewEmail); }

        /// <summary>
        /// Checks if there is a New Password
        /// </summary>
        /// <returns>Whether data exists in the field</returns>
        public bool HasNewPassword() { return HasCurrentPassword() && !string.IsNullOrWhiteSpace(NewPassword); }
    }
}

using Killerrin.Toolkit.Authentication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Authentication.Contracts
{
    public interface IPasswordValidator
    {
        /// <summary>
        /// The minimum required Password Strength
        /// </summary>
        PasswordScore MinimumPasswordStrength { get; set; }

        /// <summary>
        /// Validates the Password
        /// </summary>
        /// <param name="password">The username to Validate</param>
        /// <returns>Whether the Password is validated</returns>
        bool ValidatePassword(string password);

        /// <summary>
        /// Checks the strength of a given Password
        /// </summary>
        /// <param name="password">The password</param>
        /// <returns>The strength of the Password</returns>
        PasswordScore CheckPasswordStrength(string password);
    }
}

using Killerrin.Toolkit.Authentication.Contracts;
using Killerrin.Toolkit.Authentication.Models.Enums;
using Killerrin.Toolkit.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Killerrin.Toolkit.Authentication.Services
{
    public class ValidationService : IUsernameValidator, IEmailValidator, IPasswordValidator
    {
        public string UsernameRegex { get; set; } = @"^[a-zA-Z0-9\s]";

        /// <summary>
        /// Validates a Username for the following criteria:
        ///     * Between 1-40 Characters
        ///     * Does not match the Username Regex
        /// </summary>
        /// <param name="username">The Username</param>
        /// <returns>Whether the Username is Valid</returns>
        public virtual bool ValidateUsername(string username)
        {
            if (!IntHelpers.IsBetween(username.Length, 1, 40))
                return false;

            if (!Regex.IsMatch(username, UsernameRegex))
                return false;

            return true;
        }

        public string EmailRegex { get; set; } = @"^[\w!#$%&'*+/=?`{|}~^-]+(?:\.[!#$%&'*+/=?`{|}~^-]+)*@(?:[A-Z0-9-]+\.)+[A-Z]{2,6}$";
        /// <summary>
        /// Validates the Email
        /// </summary>
        /// <param name="email">The Email</param>
        /// <returns>Whether the Email is valid</returns>
        public virtual bool ValidateEmail(string email)
        {
            if (Regex.IsMatch(email, EmailRegex))
                return false;
            return true;
        }

        /// <summary>
        /// The minimum required Password Strength
        /// </summary>
        public PasswordScore MinimumPasswordStrength { get; set; } = PasswordScore.Medium;

        /// <summary>
        /// Validates the Password for the following Criteria:
        ///     * Password Strength
        /// </summary>
        /// <param name="password">The username to Validate</param>
        /// <returns>Whether the Password is validated</returns>
        public virtual bool ValidatePassword(string password)
        {
            PasswordScore passwordStrength = CheckPasswordStrength(password);
            Debug.WriteLine($"{passwordStrength.ToString()}");
            if ((int)passwordStrength >= (int)MinimumPasswordStrength)
                return true;
            return false;
        }

        /// <summary>
        /// Checks the strength of a given Password using the following Criteria:
        ///     * Not Null or Empty
        ///     * Contains atleast one lowercase character a-z
        ///     * Contains atleast one uppercase character a-z
        ///     * Contains a Symbol !@#$%^&*?_~-£()
        /// </summary>
        /// <param name="password">The password</param>
        /// <returns>The strength of the Password</returns>
        public virtual PasswordScore CheckPasswordStrength(string password)
        {
            int score = 0;

            if (string.IsNullOrWhiteSpace(password) || password.Length < 1)
                return PasswordScore.Blank;

            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 10)
                score++;

            if (Regex.Match(password, @"\d", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            if (Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success && Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            if (Regex.Match(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            Debug.WriteLine($"Password: {password}, Length: {password.Length}, Score: {score}");
            return (PasswordScore)score;
        }

    }
}

using Killerrin.Toolkit.Authentication.Models.Enums;
using Killerrin.Toolkit.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Killerrin.Toolkit.Authentication.Services
{
    public class ValidationService
    {
        public string UsernameRegex { get; set; } = @"^[a-zA-Z0-9\s]";
        public virtual bool ValidateUsername(string username)
        {
            if (!IntHelpers.IsBetween(username.Length, 1, 40))
                return false;

            if (!Regex.IsMatch(username, UsernameRegex))
                return false;

            return true;
        }

        public string EmailRegex { get; set; } = @"^[\w!#$%&'*+/=?`{|}~^-]+(?:\.[!#$%&'*+/=?`{|}~^-]+)*@(?:[A-Z0-9-]+\.)+[A-Z]{2,6}$";
        public virtual bool ValidateEmail(string email)
        {
            if (Regex.IsMatch(email, EmailRegex))
                return false;
            return true;
        }

        public PasswordScore MinimumPasswordStrength { get; set; } = PasswordScore.Medium;
        public virtual bool ValidatePassword(string password)
        {
            PasswordScore passwordStrength = CheckPasswordStrength(password);
            Debug.WriteLine($"{passwordStrength.ToString()}");
            if ((int)passwordStrength >= (int)MinimumPasswordStrength)
                return true;
            return false;
        }

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

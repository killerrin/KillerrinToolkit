using Killerrin.Toolkit.Authentication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Authentication.Contracts
{
    public interface IPasswordValidator
    {
        PasswordScore MinimumPasswordStrength { get; set; }
        bool ValidatePassword(string password);
        PasswordScore CheckPasswordStrength(string password);
    }
}

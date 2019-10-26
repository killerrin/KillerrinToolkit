using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Authentication.Contracts
{
    public interface IEmailValidator
    {
        /// <summary>
        /// Validates the Email
        /// </summary>
        /// <param name="email">The Email</param>
        /// <returns>Whether the Email is valid</returns>
        bool ValidateEmail(string email);
    }
}

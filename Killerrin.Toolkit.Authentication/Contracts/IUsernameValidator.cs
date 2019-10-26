using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Authentication.Contracts
{
    public interface IUsernameValidator
    {
        /// <summary>
        /// Validates the Username
        /// </summary>
        /// <param name="username">The username to Validate</param>
        /// <returns>Whether the Username is validated</returns>
        bool ValidateUsername(string username);
    }
}

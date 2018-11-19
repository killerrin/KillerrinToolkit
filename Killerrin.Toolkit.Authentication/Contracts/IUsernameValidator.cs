using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Authentication.Contracts
{
    public interface IUsernameValidator
    {
        bool ValidateUsername(string username);
    }
}

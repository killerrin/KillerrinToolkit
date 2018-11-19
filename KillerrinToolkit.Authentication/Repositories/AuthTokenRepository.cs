using Killerrin.Toolkit.Authentication.Models;
using Killerrin.Toolkit.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Authentication.Repositories
{
    public class AuthTokenRepository<C> : RepositoryBase<AuthToken> where C : DbContext
    {
        public AuthTokenRepository(C context) : base(context)
        {
        }
    }
}

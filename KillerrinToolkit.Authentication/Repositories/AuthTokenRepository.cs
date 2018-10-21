using KillerrinToolkit.Authentication.Models;
using KillerrinToolkit.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KillerrinToolkit.Authentication.Repositories
{
    public class AuthTokenRepository<C> : RepositoryBase<AuthToken> where C : DbContext
    {
        public AuthTokenRepository(C context) : base(context)
        {
        }
    }
}

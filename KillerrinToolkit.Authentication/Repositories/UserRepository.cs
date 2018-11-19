using Killerrin.Toolkit.Authentication.Models;
using Killerrin.Toolkit.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Authentication.Repositories
{
    public class UserRepository<C> : RepositoryBase<User> where C : DbContext
    {
        public UserRepository(C context) : base(context)
        {
        }
    }
}

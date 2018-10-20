using KillerrinToolkit.Authentication.Models;
using KillerrinToolkit.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KillerrinToolkit.Authentication.Repositories
{
    public class UserRepository<C> : RepositoryBase<User> where C : DbContext
    {
        public UserRepository(C context) : base(context)
        {
        }
    }
}

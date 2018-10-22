using KillerrinToolkit.EFCore.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KillerrinToolkit.EFCore.Repositories
{
    public class GenericRepository<T> : RepositoryBase<T> where T : class
    {
        public GenericRepository(DbContext context)
            : base(context)
        {
        }
    }
}

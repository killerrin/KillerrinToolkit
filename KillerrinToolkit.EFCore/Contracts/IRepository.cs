using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KillerrinToolkit.EFCore.Contracts
{
    public interface IRepository<T> where T : class
    {
        (DbContext Context, DbSet<T> DbSet) DatabaseInfo { get; }
        int Count { get; }

        void Add(T item);
        void AddAndCommit(T item);
        void AddRange(IEnumerable<T> items);

        bool Exists(int key);
        T Find(int key);

        IEnumerable<T> GetAll();
        IQueryable<T> GetAllQuery();
        IQueryable<T> GetPaged(int skip, int take);


        void Remove(int key);
        void RemoveAndCommit(int key);
        void RemoveRange(IEnumerable<T> items);

        void Update(T item);
        void UpdateAndCommit(T item);

        void Clear();

        void Commit();
        void Dispose();
    }
}

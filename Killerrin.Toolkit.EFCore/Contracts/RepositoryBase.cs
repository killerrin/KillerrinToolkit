using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.EFCore.Contracts
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected DbSet<T> _dbSet;

        public (DbContext Context, DbSet<T> DbSet) DatabaseInfo { get { return (_context, _dbSet); } }
        public int Count => GetAllQuery().Count();

        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Adds an item to the Repository
        /// </summary>
        /// <param name="item">The item to add</param>
        public virtual void Add(T item)
        {
            _dbSet.Add(item);
        }
        
        /// <summary>
        /// Adds and commits an item to the Repository
        /// </summary>
        /// <param name="item">The Item</param>
        public virtual void AddAndCommit(T item)
        {
            Add(item);
            Commit();
        }

        /// <summary>
        /// Adds a range of items to the Repository
        /// </summary>
        /// <param name="items">The range of items to add</param>
        public virtual void AddRange(IEnumerable<T> items)
        {
            _dbSet.AddRange(items);
            _context.SaveChanges();
        }

        /// <summary>
        /// Checks if a given key exists in the Repository
        /// </summary>
        /// <param name="key">The Key</param>
        /// <returns>Whether the Key Exists</returns>
        public virtual bool Exists(int key)
        {
            return Find(key) != null;
        }

        /// <summary>
        /// Finds an item in the repository using its Key
        /// </summary>
        /// <param name="key">The Key</param>
        /// <returns>The Item from the Repository</returns>
        public virtual T Find(int key)
        {
            return _dbSet.Find(key);
        }

        /// <summary>
        /// Gets all the items from the Repository
        /// </summary>
        /// <returns>All the items in the Repository</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        /// <summary>
        /// Gets all the items in the Repository following a Query
        /// </summary>
        /// <returns>All the items in the Repository that fits the query</returns>
        public virtual IQueryable<T> GetAllQuery()
        {
            return _dbSet;
        }

        /// <summary>
        /// Gets a paged number of items in the Repository
        /// </summary>
        /// <param name="skip">The number of items to skip</param>
        /// <param name="take">The number of items to take</param>
        /// <returns>The paged list of items</returns>
        public virtual IQueryable<T> GetPaged(int skip, int take)
        {
            return GetAllQuery().Skip(skip).Take(take);
        }

        /// <summary>
        /// Removes a given item from the Repository using its Key
        /// </summary>
        /// <param name="key">The key to remove</param>
        public virtual void Remove(int key)
        {
            var entity = Find(key);

            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Removes and Commits a given item from the Repository using its Key
        /// </summary>
        /// <param name="key">The key to remove</param>
        public virtual void RemoveAndCommit(int key)
        {
            Remove(key);
            Commit();
        }

        /// <summary>
        /// Removes a given range from the Repository
        /// </summary>
        /// <param name="items">The range of items to remove</param>
        public virtual void RemoveRange(IEnumerable<T> items)
        {
            _dbSet.AttachRange(items);
            _dbSet.RemoveRange(items);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates an item within the Repository
        /// </summary>
        /// <param name="item">The updated Item</param>
        public virtual void Update(T item)
        {
            _dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Updates and Commits an item within the Repository
        /// </summary>
        /// <param name="item">The updated Item</param>
        public virtual void UpdateAndCommit(T item)
        {
            Update(item);
            Commit();
        }

        /// <summary>
        /// Clears the repository
        /// </summary>
        public virtual void Clear()
        {
            var allItems = GetAll();
            _dbSet.RemoveRange(allItems);
            _context.SaveChanges();
        }

        /// <summary>
        /// Commits a change to the Repository
        /// </summary>
        public virtual void Commit()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Disposes the Context for this Repository
        /// </summary>
        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}

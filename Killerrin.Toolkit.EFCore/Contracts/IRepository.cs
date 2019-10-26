using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.EFCore.Contracts
{
    public interface IRepository<T> : IDisposable where T : class
    {
        (DbContext Context, DbSet<T> DbSet) DatabaseInfo { get; }
        int Count { get; }

        /// <summary>
        /// Adds an item to the Repository
        /// </summary>
        /// <param name="item">The item to add</param>
        void Add(T item);

        /// <summary>
        /// Adds and commits an item to the Repository
        /// </summary>
        /// <param name="item">The Item</param>
        void AddAndCommit(T item);

        /// <summary>
        /// Adds a range of items to the Repository
        /// </summary>
        /// <param name="items">The range of items to add</param>
        void AddRange(IEnumerable<T> items);

        /// <summary>
        /// Checks if a given key exists in the Repository
        /// </summary>
        /// <param name="key">The Key</param>
        /// <returns>Whether the Key Exists</returns>
        bool Exists(int key);

        /// <summary>
        /// Finds an item in the repository using its Key
        /// </summary>
        /// <param name="key">The Key</param>
        /// <returns>The Item from the Repository</returns>
        T Find(int key);

        /// <summary>
        /// Gets all the items from the Repository
        /// </summary>
        /// <returns>All the items in the Repository</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets all the items in the Repository following a Query
        /// </summary>
        /// <returns>All the items in the Repository that fits the query</returns>
        IQueryable<T> GetAllQuery();

        /// <summary>
        /// Gets a paged number of items in the Repository
        /// </summary>
        /// <param name="skip">The number of items to skip</param>
        /// <param name="take">The number of items to take</param>
        /// <returns>The paged list of items</returns>
        IQueryable<T> GetPaged(int skip, int take);

        /// <summary>
        /// Removes a given item from the Repository using its Key
        /// </summary>
        /// <param name="key">The key to remove</param>
        void Remove(int key);

        /// <summary>
        /// Removes and Commits a given item from the Repository using its Key
        /// </summary>
        /// <param name="key">The key to remove</param>
        void RemoveAndCommit(int key);

        /// <summary>
        /// Removes a given range from the Repository
        /// </summary>
        /// <param name="items">The range of items to remove</param>
        void RemoveRange(IEnumerable<T> items);

        /// <summary>
        /// Updates an item within the Repository
        /// </summary>
        /// <param name="item">The updated Item</param>
        void Update(T item);

        /// <summary>
        /// Updates and Commits an item within the Repository
        /// </summary>
        /// <param name="item">The updated Item</param>
        void UpdateAndCommit(T item);

        /// <summary>
        /// Clears the repository
        /// </summary>
        void Clear();

        /// <summary>
        /// Commits a change to the Repository
        /// </summary>
        void Commit();
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Killerrin.Toolkit.Core.Helpers
{
    public static class ObservableCollectionHelpers
    {
        /// <summary>
        /// Sorts an Observable Collection
        /// </summary>
        /// <typeparam name="T">The type of the Observable Collection</typeparam>
        /// <param name="collection">The collection to sort</param>
        public static void Sort<T>(this ObservableCollection<T> observable) where T : IComparable<T>, IEquatable<T>
        {
            List<T> sorted = observable.OrderBy(x => x).ToList();

            int ptr = 0;
            while (ptr < sorted.Count)
            {
                if (!observable[ptr].Equals(sorted[ptr]))
                {
                    T t = observable[ptr];
                    observable.RemoveAt(ptr);
                    observable.Insert(sorted.IndexOf(t), t);
                }
                else
                {
                    ptr++;
                }
            }
        }

        /// <summary>
        /// Implements AddRange to an ObservableCollection
        /// </summary>
        /// <typeparam name="T">The type of the Observable Collection</typeparam>
        /// <param name="observable">The Observable Collection</param>
        /// <param name="collection">The collection to add</param>
        public static void AddRange<T>(this ObservableCollection<T> observable, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                observable.Add(item);
            }
        }
    }
}

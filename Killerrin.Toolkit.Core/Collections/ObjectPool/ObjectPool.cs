using Killerrin.Toolkit.Core.Collections.ObjectPool.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Killerrin.Toolkit.Core.Collections.ObjectPool
{

    public class ObjectPool<T> where T : class
    {
        public Type PoolType { get { return typeof(T); } }
        public int MaxItemsInPool { get; protected set; }
        public int CurrentPoolCount { get { return UnallocatedCount + AllocatedCount; } }

        public int UnallocatedCount { get { return m_unallocatedPool.Count; } }
        public int AllocatedCount { get { return m_allocatedPool.Count; } }
        public bool CanAllocate { get { return UnallocatedCount > 0; } }

        public Func<T> PoolItemInstantiationMethod { get; set; }
        public Action<T> PoolItemResetMethod { get; set; }

        protected Queue<T> m_unallocatedPool { get; set; }
        protected List<T> m_allocatedPool { get; set; }

        private object m_lockObject = new object();

        public ObjectPool(int maxItemsInPool, Func<T> poolItemInstantiationMethod, Action<T> poolItemResetMethod)
        {
            if (MaxItemsInPool <= 0) throw new ArgumentException($"{nameof(maxItemsInPool)} must be greater than 0", nameof(maxItemsInPool));
            MaxItemsInPool = maxItemsInPool;

            PoolItemInstantiationMethod = poolItemInstantiationMethod ?? throw new ArgumentNullException(nameof(poolItemInstantiationMethod));
            PoolItemResetMethod = poolItemResetMethod ?? throw new ArgumentNullException(nameof(poolItemResetMethod));

            CreatePools();
        }

        protected void CreatePools()
        {
            // Create Temporary Pools
            var unallocatedPool = new Queue<T>(MaxItemsInPool);
            var allocatedPool = new List<T>(MaxItemsInPool);

            // Create the items in the pool
            for (int i = 0; i < MaxItemsInPool; i++)
            {
                var newPoolItem = PoolItemInstantiationMethod();
                unallocatedPool.Enqueue(newPoolItem);
            }

            // Assign the pools to the class
            lock (m_lockObject)
            {
                m_unallocatedPool = unallocatedPool;
                m_allocatedPool = allocatedPool;
            }
        }

        public void Reset()
        {
            // Reset all the objects and return them to the unallocated pool
            lock (m_lockObject)
            {
                foreach (var i in m_allocatedPool)
                {
                    PoolItemResetMethod(i);
                    m_unallocatedPool.Enqueue(i);
                }

                // Clear the Allocated Pool
                m_allocatedPool.Clear();
            }
        }

        public void Resize(int newCapacity)
        {
            lock (m_lockObject)
            {
                var currentSize = MaxItemsInPool;
                var difference = newCapacity - currentSize;

                // If there is no difference, return as there is no change
                if (difference == 0) return;
                else if (difference > 0) // If the difference is a positive number, then the pool has increased in size and new items must be instantiated
                {
                    for (int i = 0; i < difference; i++)
                    {
                        var newItem = PoolItemInstantiationMethod();
                        m_unallocatedPool.Enqueue(newItem);
                    }
                }
                else
                {
                    // Finally, if the difference is a negative number, then the pool as decreased. To handle this we will try to delete excess from unallocated
                    // any leftover will be left alone until manual deallocation takes place
                    difference *= -1; // Put the difference into the positive for looping
                    for (int i = 0; i < difference; i++)
                    {
                        if (!CanAllocate) break;
                        m_unallocatedPool.Dequeue();
                    }
                }

                // Set the new max amount of items in the pool
                MaxItemsInPool = newCapacity;
            }
        }

        public T Allocate()
        {
            lock (m_lockObject)
            {
                if (!CanAllocate) throw new ObjectPoolAllocationException();

                // Retrieve the Pool Item from the Unallocated Pool, Swap it to the Allocated Pool, then return it
                var newItem = m_unallocatedPool.Dequeue();
                m_allocatedPool.Add(newItem);
                return newItem;
            }
        }

        public void Deallocate(T item)
        {
            lock (m_lockObject)
            {
                var index = m_allocatedPool.IndexOf(item);
                if (index == -1) throw new ObjectPoolObjectNotFoundException();

                // Deallocate the item from the allocatedpool, and return it to the deallocated pool
                m_allocatedPool.RemoveAt(index);

                // Only insert back into the Unallocated Pool if the reference hasn't changed
                if (CurrentPoolCount <= MaxItemsInPool)
                {
                    PoolItemResetMethod(item);
                    m_unallocatedPool.Enqueue(item);
                }
            }
        }
    }
}

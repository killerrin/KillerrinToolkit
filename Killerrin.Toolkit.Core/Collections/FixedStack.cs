using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.Core.Collections
{
    public class FixedStack<T> : IEnumerable
    {
        private LinkedList<T> m_internalList = new LinkedList<T>();
        public int MaxSize { get; private set; }
        public int Count
        {
            get
            {
                return m_internalList.Count;
            }
        }

        public FixedStack(int maxSize)
        {
            MaxSize = maxSize;
        }

        public void Push(T value)
        {
            m_internalList.AddFirst(value);
            if (Count > MaxSize)
            {
                m_internalList.RemoveLast();
            }
        }

        public T Pop()
        {
            if (Count > 0)
            {
                T value = m_internalList.First.Value;
                m_internalList.RemoveFirst();
                return value;
            }

            throw new InvalidOperationException("This Collection is empty");
        }

        public T Peek()
        {
            if (Count > 0)
            {
                T value = m_internalList.First.Value;
                return value;
            }

            throw new InvalidOperationException("This Collection is empty");
        }

        public void Clear()
        {
            m_internalList.Clear();
        }

        public bool Contains(T value)
        {
            bool result = false;
            if (Count > 0)
            {
                result = m_internalList.Contains(value);
            }
            return result;
        }

        public IEnumerator GetEnumerator()
        {
            return m_internalList.GetEnumerator();
        }

        public T this[int key]
        {
            get
            {
                return m_internalList.ElementAt(key);
            }
        }
    }
}

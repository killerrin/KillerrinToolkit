using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.Core.Collections.PriorityQueue
{
    public interface IPriorityQueueNode<K, T> : IComparable<IPriorityQueueNode<K, T>>
        where K : IComparable
    {
        K Priority { get; }
        T Data { get; set; }
    }
}

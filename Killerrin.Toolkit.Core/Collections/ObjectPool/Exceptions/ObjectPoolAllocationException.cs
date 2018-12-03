using System;

namespace Killerrin.Toolkit.Core.Collections.ObjectPool.Exceptions
{
    public class ObjectPoolAllocationException : Exception
    {
        private const string StandardErrorMessage = "This ObjectPool has no items left to allocate.";
        public ObjectPoolAllocationException() : base(StandardErrorMessage)
        {
        }

        public ObjectPoolAllocationException(string message) : base($"{StandardErrorMessage}\n{message}")
        {
        }

        public ObjectPoolAllocationException(string message, Exception innerException) : base($"{StandardErrorMessage}\n{message}", innerException)
        {
        }
    }
}

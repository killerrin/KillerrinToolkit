using System;

namespace Killerrin.Toolkit.Core.Collections.ObjectPool.Exceptions
{
    public class ObjectPoolObjectNotFoundException : Exception
    {
        private const string StandardErrorMessage = "This object does not exist within the Object Pool and cannot be Deallocated";
        public ObjectPoolObjectNotFoundException() : base(StandardErrorMessage)
        {
        }

        public ObjectPoolObjectNotFoundException(string message) : base($"{StandardErrorMessage}\n{message}")
        {
        }

        public ObjectPoolObjectNotFoundException(string message, Exception innerException) : base($"{StandardErrorMessage}\n{message}", innerException)
        {
        }
    }
}

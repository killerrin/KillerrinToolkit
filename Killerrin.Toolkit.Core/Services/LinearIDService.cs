using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Core.Services
{
    public class LinearIDService
    {
        private object m_lockObject;
        public uint CurrentID { get; protected set; }

        public LinearIDService()
        {
            m_lockObject = new object();
            CurrentID = 0;
        }

        public LinearIDService(uint currentId)
        {
            m_lockObject = new object();
            CurrentID = currentId;
        }

        /// <summary>
        /// Resets the count to zero
        /// </summary>
        public void Reset() { CurrentID = 0; }

        /// <summary>
        /// Increments the ID and returns the next number in line
        /// </summary>
        /// <returns>The next number in line</returns>
        public uint GetNewID()
        {
            uint IDToUse;
            lock (m_lockObject)
            {
                IDToUse = CurrentID;
                IncrimentID();
            }

            return IDToUse;
        }

        /// <summary>
        /// Increments the ID, and wraps it back to zero if the maximum is reached
        /// </summary>
        private void IncrimentID()
        {
            if (CurrentID < uint.MaxValue)
                CurrentID++;
            else
                CurrentID = uint.MinValue;
        }
    }
}

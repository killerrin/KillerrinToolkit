using System;
using System.Collections.Generic;
using System.Text;

namespace KillerrinToolkit.Core.Services
{
    public class LinearIDService
    {
        private object m_lockObject;
        public uint CurrentID { get; set; }

        public LinearIDService()
        {
            m_lockObject = new object();
            CurrentID = 0;
        }

        public void Reset() { CurrentID = 0; }
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

        private void IncrimentID()
        {
            if (CurrentID < uint.MaxValue)
                CurrentID++;
            else
                CurrentID = uint.MinValue;
        }
    }
}

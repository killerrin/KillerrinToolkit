using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KillerrinToolkit.Core.Helpers
{
    public class DebugHelpers
    {
        public static bool DebugMode { get { return Debugger.IsAttached; } }
        public static bool DebugConditional
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
        
        public static void PrintTotaloMemoryInUse()
        {
            Debug.WriteLine("GC: TOTAL MEMORY {0}", GC.GetTotalMemory(false));
        }
    }
}

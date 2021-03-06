﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Killerrin.Toolkit.Core.Helpers
{
    public class DebugHelpers
    {
        public static bool DebugMode { get { return Debugger.IsAttached; } }
        public static void PrintTotaloMemoryInUse()
        {
            Debug.WriteLine("GC: TOTAL MEMORY {0}", GC.GetTotalMemory(false));
        }
    }
}

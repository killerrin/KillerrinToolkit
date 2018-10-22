using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KillerrinToolkit.Core.Helpers
{
    public class TimespanHelper
    {
        public static int Microseconds(this Stopwatch watch) { return (int)(watch.ElapsedTicks * 1.0e6 / Stopwatch.Frequency + 0.4999); }
        public static int Nanoseconds(this Stopwatch watch) { return (int)(watch.ElapsedTicks * 1.0e9 / Stopwatch.Frequency + 0.4999); }

        public static long Microseconds(this TimeSpan span) { return span.Milliseconds * 1000; }
        public static long Nanoseconds(this TimeSpan span) { return span.Milliseconds * 1000000; }
        public static double TotalMicroseconds(this TimeSpan span) { return span.TotalMilliseconds * 1000; }
        public static double TotalNanoseconds(this TimeSpan span) { return span.TotalMilliseconds * 1000000; }
    }
}

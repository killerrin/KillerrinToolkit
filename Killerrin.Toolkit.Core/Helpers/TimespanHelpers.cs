using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Killerrin.Toolkit.Core.Helpers
{
    public static class TimespanHelper
    {
        /// <summary>
        /// Returns the number of Microseconds from this Timespan
        /// </summary>
        /// <param name="span">The Timespan</param>
        /// <returns>The number of Microseconds</returns>
        public static long Microseconds(this TimeSpan span) { return span.Milliseconds * 1000; }

        /// <summary>
        /// Returns the number of Nanoseconds from this Timespan
        /// </summary>
        /// <param name="span">The Timespan</param>
        /// <returns>The number of Nanoseconds</returns>
        public static long Nanoseconds(this TimeSpan span) { return span.Milliseconds * 1000000; }

        /// <summary>
        /// Returns the Total number of Microseconds from this Timespan
        /// </summary>
        /// <param name="span">The Timespan</param>
        /// <returns>The number of Microseconds</returns>
        public static double TotalMicroseconds(this TimeSpan span) { return span.TotalMilliseconds * 1000; }

        /// <summary>
        /// Returns the Total number of Nanoseconds from this Timespan
        /// </summary>
        /// <param name="span">The Timespan</param>
        /// <returns>The number of Nanoseconds</returns>
        public static double TotalNanoseconds(this TimeSpan span) { return span.TotalMilliseconds * 1000000; }
    }
}

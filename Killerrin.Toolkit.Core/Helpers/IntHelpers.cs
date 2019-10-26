using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.Core.Helpers
{
    public static class IntHelpers
    {
        /// <summary>
        /// Checks if a given number is between a range
        /// </summary>
        /// <param name="num">The number to check</param>
        /// <param name="low">The low end (inclusive)</param>
        /// <param name="high">The high end (inclusive)</param>
        /// <returns>Whether the number is in the range</returns>
        public static bool IsBetween(this int num, int low, int high)
        {
            return num >= low && num <= high;
        }

        /// <summary>
        /// Checks if a number is odd
        /// </summary>
        /// <param name="value">The number</param>
        /// <returns>Whether the number is odd</returns>
        public static bool IsOdd(this int value)
        {
            return value % 2 != 0;
        }

        /// <summary>
        /// Checks if a number is even
        /// </summary>
        /// <param name="value">The number</param>
        /// <returns>Whether the number is even</returns>
        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.Core.Helpers
{
    public static class TypeHelpers
    {
/// <summary>
        /// Attempts to Parse the given input into the specified type 
        /// </summary>
        /// <typeparam name="T">The type you wish to convert your input into</typeparam>
        /// <param name="input">The input you wish to convert into your Type</param>
        /// <returns>The Parsed Object</returns>
        /// <exception cref="NotSupportedException">NotSupportedException is thrown if the input can not be converted</exception>
        public static T Parse<T>(string input)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, input);
        }

        /// <summary>
        /// Attempts to Parse the given input into the specified type 
        /// </summary>
        /// <typeparam name="T">The type you wish to convert your input into</typeparam>
        /// <param name="input">The input you wish to convert into your Type</param>
        /// <returns>The Parsed Object</returns>
        /// <exception cref="NotSupportedException">NotSupportedException is thrown if the input can not be converted</exception>
        public static bool TryParse<T>(string input, out T output)
        {
            return Is<T>(input, out output);
        }

        /// <summary>
        /// Determines if a given input can be converted into the given type
        /// </summary>
        /// <typeparam name="T">The type you wish to check if your input can convert into</typeparam>
        /// <param name="input">The input you wish to check for converters</param>
        /// <returns>Whether the item can be converted into the given type</returns>
        public static bool Is<T>(this string input)
        {
            T converted = default(T);
            return Is<T>(input, out converted);
        }

        /// <summary>
        /// Determines if a given input can be converted into the given type
        /// </summary>
        /// <typeparam name="T">The type you wish to check if your input can convert into</typeparam>
        /// <param name="input">The input you wish to check for converters</param>
        /// <param name="converted">The converted object</param>
        /// <returns>Whether the item can be converted into the given type</returns>
        public static bool Is<T>(this string input, out T converted)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                converted = (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, input);
                return true;
            }
            catch (NotSupportedException)
            {
                converted = default(T);
                return false;
            }
            catch (Exception)
            {
                converted = default(T);
                return false;
            }
        }
    }
}

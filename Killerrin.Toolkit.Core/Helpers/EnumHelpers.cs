using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.Core.Helpers
{
    public static class EnumHelpers
    {
        /// <summary>
        /// Returns a Name Value pair of all the values in an enum
        /// </summary>
        /// <typeparam name="T">The type of an enum</typeparam>
        /// <returns>A KeyValuePair containing all the items in this Enum</returns>
        public static IEnumerable<KeyValuePair<string, int>> GetNameValuePair<T>()
        {
            List<KeyValuePair<string, int>> enums = new List<KeyValuePair<string, int>>();

            Type genericType = typeof(T);
            foreach (T obj in Enum.GetValues(genericType))
            {
                Enum test = Enum.Parse(typeof(T), obj.ToString()) as Enum;
                int x = Convert.ToInt32(test);
                enums.Add(new KeyValuePair<string, int>(obj.ToString(), x));
            }

            return enums;
        }
        
        /// <summary>
        /// Returns a count of all the items in an enum
        /// </summary>
        /// <param name="e">The enum to count</param>
        /// <returns>The count of the items in this Enum</returns>
        public static int Count(this Enum e)
        {
            var names = Enum.GetNames(e.GetType());
            return names.Length;
        }
    }
}

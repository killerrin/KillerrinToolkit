using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Killerrin.Toolkit.Core.Serializers
{
    public static class UTF8StringSerializer
    {
        /// <summary>
        /// Serializes a string into a UTF8 binary string
        /// </summary>
        /// <param name="value">The string to serialize</param>
        /// <returns>The serialized string</returns>
        public static byte[] Serialize(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// Deserializes a UTF8 binary string into a string
        /// </summary>
        /// <param name="data">The UTF8 formatted binary</param>
        /// <returns>The deserialized string</returns>
        public static string Deserialize(byte[] data)
        {
            try
            {
                string deserial = Encoding.UTF8.GetString(data, 0, data.Length);
                return deserial;
            }
            catch (Exception) { }
            return "";
        }
    }
}

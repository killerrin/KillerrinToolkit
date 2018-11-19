using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Killerrin.Toolkit.Core.Serializers
{
    public static class UTF8StringSerializer
    {
        public static byte[] Serialize(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

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

using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Core.Helpers
{
    public class EncryptionHelper
    {
        /// <summary>
        /// Encodes a String into Base64
        /// </summary>
        /// <param name="plainText">The string to encode</param>
        /// <returns>The encoded Base64 string</returns>
        public static string EncodeBase64String(string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText)) return "";
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decodes a Base64 String
        /// </summary>
        /// <param name="base64EncodedData">The string to decode</param>
        /// <returns>The decoded string</returns>
        public static string DecodeBase64String(string base64EncodedData)
        {
            if (string.IsNullOrWhiteSpace(base64EncodedData)) return "";
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
		
        /// <summary>
        /// Checks whether a given string is a Base64 String
        /// </summary>
        /// <param name="base64EncodedData">The string to check</param>
        /// <returns>Whether the string is Base64</returns>
        public static bool IsBase64String(string base64EncodedData)
        {
            try
            {
                var decodedString = DecodeBase64String(base64EncodedData);
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Checks whether a given string is a Base64 String
        /// </summary>
        /// <param name="base64EncodedData">The string to check</param>
        /// <param name="decodedString">The decoded string</param>
        /// <returns>Whether the string is Base64</returns>
        public static bool IsBase64String(string base64EncodedData, out string decodedString)
        {
            try
            {
                decodedString = DecodeBase64String(base64EncodedData);
                return true;
            }
            catch (Exception)
            {
                decodedString = "";
                return false;
            }
        }
    }
}

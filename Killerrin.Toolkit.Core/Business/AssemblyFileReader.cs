using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.Core.Business
{
    public static class AssemblyFileReader
    {
        /// <summary>
        /// Gets the current assembly that a given Type lives in
        /// </summary>
        /// <typeparam name="T">The type you wish to get the assembly from</typeparam>
        /// <returns>The assembly of the Type</returns>
        public static Assembly GetCurrentAssembly<T>()
        {
            return Assembly.GetAssembly(typeof(T));
        }

        /// <summary>
        /// Creates a fully formed Resource Name
        /// </summary>
        /// <param name="fileNameAndExtension"></param>
        /// <param name="namespaceParts"></param>
        /// <returns>The Resource Name</returns>
        public static string ResourceNameCreator(string fileNameAndExtension, params string[] namespaceParts)
        {
            var resourceName = string.Join(".", namespaceParts);
            return $"{resourceName}.{fileNameAndExtension}";
        }

        /// <summary>
        /// Returns an EmbeddedResource in the Assembly
        /// </summary>
        /// <param name="assembly">The Assembly</param>
        /// <param name="resourceName">The fully qualified Resource Name</param>
        /// <returns></returns>
        public static List<string> ReadFile(this Assembly assembly, string resourceName)
        {
            List<string> lines = new List<string>();

            // Get Resource Stream
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                // Open File
                using (StreamReader reader = new StreamReader(stream))
                {
                    // Read until end of stream
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                }
            }

            return lines;
        }
    }
}

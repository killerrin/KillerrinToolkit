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

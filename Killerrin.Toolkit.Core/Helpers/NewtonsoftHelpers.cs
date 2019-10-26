using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.Core.Helpers
{
    public static class NewtonsoftHelpers
    {
        /// <summary>
        /// Renames a given parameter token
        /// </summary>
        /// <param name="token">The token to rename</param>
        /// <param name="newName">The new name of the token</param>
        public static void Rename(this JToken token, string newName)
        {
            var parent = token.Parent;
            if (parent == null)
                throw new InvalidOperationException("The parent is missing.");
            var newToken = new JProperty(newName, token);
            parent.Replace(newToken);
        }
    }
}

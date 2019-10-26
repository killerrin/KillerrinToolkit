using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Managers
{
    public class InputManager
    {
        /// <summary>
        /// Determines whether a given MenuCode is an exit code
        /// </summary>
        /// <param name="menuCode"></param>
        /// <returns>Whether the input is an Exit Code</returns>
        public virtual bool IsExitCode(string menuCode) { return menuCode == "exit" || menuCode == "quit" || menuCode == "close" || menuCode == "-1"; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Managers
{
    public class InputManager
    {
        public virtual bool IsExitCode(string menuCode) { return menuCode == "exit" || menuCode == "quit" || menuCode == "close" || menuCode == "-1"; }
    }
}

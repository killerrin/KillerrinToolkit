using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Managers.Contracts
{
    public interface IMenuManager
    {
        event EventHandler<object> OnMenuManagerExit;
        void ExitMenu();
        void RunMenuSystem();

    }
}

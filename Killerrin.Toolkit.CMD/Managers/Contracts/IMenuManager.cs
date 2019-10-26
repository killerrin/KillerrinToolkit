using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Managers.Contracts
{
    public interface IMenuManager
    {
        /// <summary>
        /// Notifies when the Menu Manager Exits
        /// </summary>
        event EventHandler<object> OnMenuManagerExit;

        /// <summary>
        /// Exits the current menu
        /// </summary>
        void ExitMenu();

        /// <summary>
        /// Activates the Menu System
        /// </summary>
        void RunMenuSystem();

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Menus.Contracts
{
    public interface IMenu
    {
        Dictionary<string, Menu> MenuItems { get; }
        string Name { get; set; }

        /// <summary>
        /// Notifies before the Menu is run
        /// </summary>
        event EventHandler<string> OnPreMenuRun;

        /// <summary>
        /// Runs the menu
        /// </summary>
        void Run();

        /// <summary>
        /// Notifies after the menu is run
        /// </summary>
        event EventHandler<string> OnPostMenuRun;
    }
}

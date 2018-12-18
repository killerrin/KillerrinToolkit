using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Menus.Contracts
{
    public interface IMenu
    {
        Dictionary<string, Menu> MenuItems { get; }
        string Name { get; set; }

        event EventHandler<string> OnPreMenuRun;
        void Run();
        event EventHandler<string> OnPostMenuRun;
    }
}

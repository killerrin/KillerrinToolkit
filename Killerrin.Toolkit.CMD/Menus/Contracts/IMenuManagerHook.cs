using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Menus.Contracts
{
    public interface IMenuManagerHook
    {
        bool PrintMenuNavigation { get; }
        bool EnableMenuSystemInput { get;}

        event EventHandler<string> OnPreMenuRun;
        void Run();
        event EventHandler<string> OnPostMenuRun;

        void InvokeOnPreMenuRun(object sender, string args);
        void InvokeOnPostMenuRun(object sender, string args);
    }
}

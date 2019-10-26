using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Menus.Contracts
{
    public interface IMenuManagerHook
    {
        bool PrintMenuNavigation { get; }
        bool EnableMenuSystemInput { get;}

        /// <summary>
        /// Notifies before the Menu is run
        /// </summary>
        event EventHandler<string> OnPreMenuRun;

        /// <summary>
        /// Runs the menu
        /// </summary>
        void Run();

        /// <summary>
        /// Notifies after the Menu is run
        /// </summary>
        event EventHandler<string> OnPostMenuRun;

        /// <summary>
        /// Invokes the OnPreMenuRun Event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">The arguments for this event invocation</param>
        void InvokeOnPreMenuRun(object sender, string args);

        /// <summary>
        /// Invokes the OnPostMenuRun Event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">The arguments for this event invocation</param>
        void InvokeOnPostMenuRun(object sender, string args);
    }
}

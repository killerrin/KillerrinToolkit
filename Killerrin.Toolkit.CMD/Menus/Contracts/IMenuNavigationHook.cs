using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Menus.Contracts
{
    public interface IMenuNavigationHook
    {
        /// <summary>
        /// Notifies when this menu is being navigated to
        /// </summary>
        event EventHandler<object> OnNavigatingTo;

        /// <summary>
        /// Notifies when this menu has been navigated to
        /// </summary>
        event EventHandler<object> OnNavigated;

        /// <summary>
        /// Notifies when this menu has been loaded
        /// </summary>
        event EventHandler<object> OnLoaded;

        /// <summary>
        /// Notifies when this menu is being navigated from
        /// </summary>
        event EventHandler<object> OnNavigatingFrom;

        /// <summary>
        /// Notifies when this menu is unloaded
        /// </summary>
        event EventHandler<object> OnUnloaded;

        /// <summary>
        /// Invokes the OnNavigatingTo Event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">The event arguments</param>
        void InvokeOnNavigatingTo(object sender, object args);

        /// <summary>
        /// Invokes the OnNavigated Event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">The event arguments</param>
        void InvokeOnNavigated(object sender, object args);

        /// <summary>
        /// Invokes the OnLoaded Event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">The event arguments</param>
        void InvokeOnLoaded(object sender, object args);

        /// <summary>
        /// Invokes the OnNavigatingFrom Event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">The event arguments</param>
        void InvokeOnNavigatingFrom(object sender, object args);

        /// <summary>
        /// Invokes the OnUnloaded Event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">The event arguments</param>
        void InvokeOnUnloaded(object sender, object args);
    }
}

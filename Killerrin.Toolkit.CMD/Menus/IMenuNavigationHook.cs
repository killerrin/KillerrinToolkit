using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Menus
{
    public interface IMenuNavigationHook
    {
        event EventHandler<object> OnNavigatingTo;
        event EventHandler<object> OnNavigated;
        event EventHandler<object> OnLoaded;
        event EventHandler<object> OnNavigatingFrom;
        event EventHandler<object> OnUnloaded;

        void InvokeOnNavigatingTo(object sender, object args); 
        void InvokeOnNavigated(object sender, object args);
        void InvokeOnLoaded(object sender, object args);
        void InvokeOnNavigatingFrom(object sender, object args);
        void InvokeOnUnloaded(object sender, object args);
    }
}

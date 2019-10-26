using Killerrin.Toolkit.CMD.Managers;
using Killerrin.Toolkit.CMD.Menus.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.CMD.Menus
{
    public abstract class Menu : IMenu, IMenuNavigationHook, IMenuManagerHook
    {
        public Dictionary<string, Menu> MenuItems { get; protected set; } = new Dictionary<string, Menu>();
        public string Name { get; set; }
        public string[] Args { get; set; }

        public Menu(string name) : this(name, null) { }
        public Menu(string name, string[] args)
        {
            Name = name;
            Args = args;

        }

        /// <summary>
        /// Notifies before the Menu is run
        /// </summary>
        public event EventHandler<string> OnPreMenuRun;

        /// <summary>
        /// Runs the menu
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Notifies after the Menu is run
        /// </summary>
        public event EventHandler<string> OnPostMenuRun;

        /// <summary>
        /// Adds a Submenu to this Menu
        /// </summary>
        /// <param name="menu">The Submenu to add</param>
        protected internal void AddMenuItem(Menu menu)
        {
            if (menu is SeparatorMenuItem)
            {
                MenuItems[$"{Guid.NewGuid().ToString()}"] = menu;
            }
            else if (menu is TextMenuItem)
            {
                TextMenuItem tmp = menu as TextMenuItem;
                MenuItems[$"{MenuItems.Count}"] = menu;
            }
            else
            {
                MenuItems[$"{MenuItems.Count}"] = menu;
            }
        }

        /// <summary>
        /// Gets a given Submenu using its Menu Code
        /// </summary>
        /// <param name="menuCode">The Menu Code</param>
        /// <returns>The Submenu</returns>
        protected internal Menu GetMenuItem(string menuCode) { return MenuItems[menuCode]; }

        /// <summary>
        /// Gets a given Submenu using its Menu Name
        /// </summary>
        /// <param name="name">The Name of the menu</param>
        /// <returns>The Submenu</returns>
        protected internal Menu GetMenuItemByName(string name) { return MenuItems.Values.Where(x => x.Name == name).FirstOrDefault(); }

        #region IMenuNavigationHook
        public event EventHandler<object> OnNavigatingTo;
        public event EventHandler<object> OnNavigated;
        public event EventHandler<object> OnLoaded;
        public event EventHandler<object> OnNavigatingFrom;
        public event EventHandler<object> OnUnloaded;
        void IMenuNavigationHook.InvokeOnNavigatingTo(object sender, object args) { OnNavigatingTo?.Invoke(sender, args); }
        void IMenuNavigationHook.InvokeOnNavigated(object sender, object args) { OnNavigated?.Invoke(sender, args); }
        void IMenuNavigationHook.InvokeOnLoaded(object sender, object args) { OnLoaded?.Invoke(sender, args); }
        void IMenuNavigationHook.InvokeOnNavigatingFrom(object sender, object args) { OnNavigatingFrom?.Invoke(sender, args); }
        void IMenuNavigationHook.InvokeOnUnloaded(object sender, object args) { OnUnloaded?.Invoke(sender, args); }
        #endregion
        #region IMenuManagerHook
        protected bool PrintMenuNavigation { get; set; } = true;
        protected bool EnableMenuSystemInput { get; set; } = true;

        bool IMenuManagerHook.PrintMenuNavigation => PrintMenuNavigation;
        bool IMenuManagerHook.EnableMenuSystemInput => EnableMenuSystemInput;
        void IMenuManagerHook.InvokeOnPreMenuRun(object sender, string args) { OnPreMenuRun?.Invoke(sender, args); }
        void IMenuManagerHook.InvokeOnPostMenuRun(object sender, string args) { OnPostMenuRun?.Invoke(sender, args); }
        #endregion
    }
}

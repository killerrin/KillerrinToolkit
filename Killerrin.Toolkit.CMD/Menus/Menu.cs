using Killerrin.Toolkit.CMD.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.CMD.Menus
{
    public abstract class Menu
    {
        public enum MenuResultCode
        {
            Successful,
            Invalid,
            Exit,
        }

        object m_lockObject = new object();
        Dictionary<string, Menu> MenuItems { get; set; } = new Dictionary<string, Menu>();
        public string Name { get; set; }
        public string[] Args { get; set; }

        public string LastInput { get; set; }
        public Menu LastMenu { get; set; }

        public Menu(string name) : this(name, null) { }
        public Menu(string name, string[] args)
        {
            Name = name;
            Args = args;

            LastInput = "";
            LastMenu = this;
        }

        public event EventHandler<object> OnNavigatingTo;
        public event EventHandler<object> OnNavigated;
        public event EventHandler<object> OnLoaded;
        public event EventHandler<object> OnNavigatingFrom;
        public event EventHandler<object> OnUnloaded;
        internal void InvokeOnNavigatingTo(object sender, object args) { OnNavigatingTo?.Invoke(sender, args); }
        internal void InvokeOnNavigated(object sender, object args) { OnNavigated?.Invoke(sender, args); }
        internal void InvokeOnLoaded(object sender, object args) { OnLoaded?.Invoke(sender, args); }
        internal void InvokeOnNavigatingFrom(object sender, object args) { OnNavigatingFrom?.Invoke(sender, args); }
        internal void InvokeOnUnloaded(object sender, object args) { OnUnloaded?.Invoke(sender, args); }

        public event EventHandler<string> OnPreMenuRun;
        public event EventHandler<string> OnPostMenuRun;
        public abstract void Run();
        public event EventHandler<Menu> OnSuccess;
        public event EventHandler<string> OnExit;
        public event EventHandler<string> OnInvalid;

        public virtual MenuResultCode RunMenu(string menuCode)
        {
            var menuResult = GetMenuResult(menuCode);
            lock (m_lockObject)
            {
                switch (menuResult)
                {
                    case MenuResultCode.Successful:
                        var menu = MenuItems[menuCode];
                        NavigationManager.Instance.Navigate(menu);
                        NavigationManager.Instance.Peek().Run();
                        break;
                    case MenuResultCode.Invalid: break;
                    case MenuResultCode.Exit: break;
                    default: break;
                }
                return menuResult;
            }
        }
        public virtual MenuResultCode GetMenuResult(string menuCode)
        {
            lock (m_lockObject)
            {
                if (IsExitCode(menuCode)) return MenuResultCode.Exit;
                else if (MenuItems.ContainsKey(menuCode)) return MenuResultCode.Successful;
                return MenuResultCode.Invalid;
            }
        }

        protected virtual bool IsExitCode(string menuCode) { return menuCode == "exit" || menuCode == "quit" || menuCode == "close" || menuCode == "-1"; }
        protected virtual void PrintMenu()
        {
            try
            {
                lock (m_lockObject)
                {
                    Console.WriteLine(new string('=', SeparatorMenuItem.DEFAULT_NUMBER_OF_TIMES));
                    Console.WriteLine($"{Name}");
                    Console.WriteLine(new string('=', SeparatorMenuItem.DEFAULT_NUMBER_OF_TIMES));
                    for (int i = 0; i < MenuItems.Count; i++)
                    {
                        var valuePair = MenuItems.ElementAt(i);
                        if (valuePair.Value is TextMenuItem)
                        {
                            var menuItem = (TextMenuItem)valuePair.Value;
                            if (menuItem.DisplayKey) Console.WriteLine($"{valuePair.Key} = {valuePair.Value.Name}");
                            else Console.WriteLine(menuItem.Text);
                        }
                        else if (valuePair.Value is SeparatorMenuItem)
                        {
                            var menuItem = (SeparatorMenuItem)valuePair.Value;
                            Console.WriteLine(menuItem.SeparationString);
                        }
                        else Console.WriteLine($"{valuePair.Key} = {valuePair.Value.Name}");
                    }

                    Console.WriteLine(new string('-', SeparatorMenuItem.DEFAULT_NUMBER_OF_TIMES));
                    Console.WriteLine("exit - Exit the menu");
                    Console.WriteLine(new string('=', SeparatorMenuItem.DEFAULT_NUMBER_OF_TIMES));
                    Console.Write("Select your Menu: ");
                }
            }
            catch (Exception) { }
        }

        protected void AddMenuItem(Menu menu)
        {
            lock (m_lockObject)
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
        }
        protected Menu GetMenuItem(string menuCode) { lock (m_lockObject) { return MenuItems[menuCode]; } }
        protected Menu GetMenuItemByName(string name) { lock (m_lockObject) { return MenuItems.Values.Where(x => x.Name == name).FirstOrDefault(); } }
        protected void RunDefaultLoop()
        {
            while (true)
            {
                Console.Clear();
                PrintMenu();

                // Run the appropriate Menu
                var input = Console.ReadLine().ToLower();
                LastInput = input;
                Console.WriteLine();
                var result = RunDefault(input);

                if (result == MenuResultCode.Exit) break;
            }
        }

        protected MenuResultCode RunDefault(string input)
        {
            OnPreMenuRun?.Invoke(this, input);
            var result = GetMenuResult(input);
            switch (result)
            {
                case MenuResultCode.Successful:
                    lock (m_lockObject)
                    {
                        var menuItem = MenuItems[input];
                        NavigationManager.Instance.Navigate(menuItem);
                        NavigationManager.Instance.Peek().Run();
                        LastMenu = menuItem;
                        OnSuccess?.Invoke(this, menuItem);
                    }
                    break;
                case MenuResultCode.Exit:
                    OnExit?.Invoke(this, input);
                    NavigationManager.Instance.GoBack();
                    break;
                case MenuResultCode.Invalid:
                default:
                    if (OnInvalid == null)
                    {
                        try { Console.WriteLine("That was an invalid selection. Please try again"); OnInvalid += Menu_OnInvalid; }
                        catch (Exception) { }
                    }
                    else OnInvalid?.Invoke(this, input);
                    break;
            }
            OnPostMenuRun?.Invoke(this, input);
            return result;
        }

        /// <summary>
        /// A default implementation of OnInvalid that does nothing
        /// This is subscribed to only if RunDefault - Invalid throws an exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_OnInvalid(object sender, string e) { }
    }
}

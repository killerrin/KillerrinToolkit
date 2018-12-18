using Killerrin.Toolkit.CMD.Managers.Contracts;
using Killerrin.Toolkit.CMD.Menus;
using Killerrin.Toolkit.CMD.Menus.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Killerrin.Toolkit.CMD.Models.LegacyMenuManager;

namespace Killerrin.Toolkit.CMD.Managers
{
    public partial class LegacyMenuManager : IMenuManager
    {
        private static LegacyMenuManager m_instance;
        public static LegacyMenuManager Instance
        {
            get
            {
                if (m_instance == null) m_instance = new LegacyMenuManager(new InputManager());
                return m_instance;
            }
        }

        public InputManager MenuInputManager { get; set; }
        public bool MenuRunning { get; set; } = false;
        public string LastInput { get; set; } = "";

        public event EventHandler<Menu> OnNavigationSuccess;
        public event EventHandler<string> OnNavigationExit;
        public event EventHandler<string> OnNavigationInvalid;
        public event EventHandler<object> OnMenuManagerExit;

        public LegacyMenuManager(InputManager inputManager)
        {
            MenuInputManager = inputManager;
        }

        public void ExitMenu()
        {
            MenuRunning = false;
            OnMenuManagerExit?.Invoke(this, null);
        }

        public void RunMenuSystem()
        {
            MenuRunning = true;
            while (MenuRunning)
            {
                // Get the current Menu
                var currentMenu = NavigationManager.Instance.Peek();
                var currentMenuHook = (IMenuManagerHook)currentMenu;

                // Clear the screen and print the menu
                Console.Clear();
                if (currentMenuHook.PrintMenuNavigation) { PrintMenu(currentMenu); }

                // Run the Current Menu
                currentMenuHook.InvokeOnPreMenuRun(this, "");
                currentMenu.Run();
                currentMenuHook.InvokeOnPostMenuRun(this, "");

                // Gather Menu Input
                if (currentMenuHook.EnableMenuSystemInput)
                {
                    var input = Console.ReadLine().ToLower();
                    LastInput = input;
                    Console.WriteLine();

                    // Process the Menu Input to see if we need to Navigate
                    var result = GetMenuResult(currentMenu, input);
                    switch (result)
                    {
                        case MenuResultCode.Successful:
                            var newMenuItem = currentMenu.MenuItems[input];
                            NavigationManager.Instance.Navigate(newMenuItem);
                            OnNavigationSuccess?.Invoke(this, newMenuItem);
                            break;
                        case MenuResultCode.Exit:
                            // If we are exiting the menu, and there is no more navigation on the BackStack, exit the Menu Loop
                            OnNavigationExit?.Invoke(this, input);
                            if (NavigationManager.Instance.GoBack() == null) { ExitMenu(); }
                            break;
                        case MenuResultCode.Invalid:
                        default:
                            if (OnNavigationInvalid == null)
                            {
                                try { Console.WriteLine("That was an invalid selection. Please try again"); }
                                catch (Exception) { }
                            }
                            else OnNavigationInvalid?.Invoke(this, input);
                            break;
                    }
                }
            }
        }

        protected virtual void PrintMenu(IMenu menu)
        {
            try
            {

                Console.WriteLine(new string('=', SeparatorMenuItem.DEFAULT_NUMBER_OF_TIMES));
                Console.WriteLine($"{menu.Name}");
                Console.WriteLine(new string('=', SeparatorMenuItem.DEFAULT_NUMBER_OF_TIMES));
                for (int i = 0; i < menu.MenuItems.Count; i++)
                {
                    var valuePair = menu.MenuItems.ElementAt(i);
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
            catch (Exception) { }
        }
        protected virtual MenuResultCode GetMenuResult(IMenu menu, string menuCode)
        {
            if (MenuInputManager.IsExitCode(menuCode)) return MenuResultCode.Exit;
            else if (menu.MenuItems.ContainsKey(menuCode)) return MenuResultCode.Successful;
            return MenuResultCode.Invalid;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Killerrin.Toolkit.CMD.Menus;

namespace Killerrin.Toolkit.CMD.Managers
{
    public class NavigationManager
    {
        private static NavigationManager m_instance;
        public static NavigationManager Instance
        {
            get
            {
                if (m_instance == null) m_instance = new NavigationManager();
                return m_instance;
            }
        }

        public IReadOnlyList<Menu> BackStack { get { return (IReadOnlyList<Menu>)m_backStack; } }
        private Stack<Menu> m_backStack = new Stack<Menu>();

        private NavigationManager() { }

        public event EventHandler<object> OnExit;

        public Menu Peek()
        {
            if (m_backStack.Count == 0) return null;
            try { return m_backStack.Peek(); }
            catch (Exception) { return null; }
        }

        public Menu GoBack()
        {
            // Trigger exit if nothing exists
            IMenuNavigationHook currentPage = Peek();
            if (currentPage == null)
            {
                OnExit?.Invoke(this, new object());
                return null;
            }

            // Unload the Previous Menu
            currentPage.InvokeOnNavigatingFrom(this, new object());
            currentPage = m_backStack.Pop();
            currentPage.InvokeOnUnloaded(this, new object());

            // Resume the previous page
            IMenuNavigationHook newPage = Peek();
            if (newPage == null)
            {
                OnExit?.Invoke(this, new object());
                return null;
            }
            newPage?.InvokeOnNavigatingTo(this, new object());
            newPage?.InvokeOnNavigated(this, new object());
            newPage?.InvokeOnLoaded(this, new object());

            // Return the new menu
            return newPage as Menu;
        }
        public void Navigate(Menu menu)
        {
            var iMenu = menu as IMenuNavigationHook;

            // Unload the Previous Menu
            IMenuNavigationHook currentPage = Peek();
            if (currentPage != null)
            {
                currentPage.InvokeOnNavigatingFrom(this, new object());
                currentPage.InvokeOnUnloaded(this, new object());
            }

            // Load the New Menu
            iMenu.InvokeOnNavigatingTo(this, new object());
            m_backStack.Push(menu);
            iMenu.InvokeOnNavigated(this, new object());
            iMenu.InvokeOnLoaded(this, new object());
        }
    }
}

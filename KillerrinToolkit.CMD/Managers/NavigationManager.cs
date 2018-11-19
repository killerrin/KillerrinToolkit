using System;
using System.Collections.Generic;
using System.Text;
using Killerrin.Toolkit.CMD.Menus;

namespace Killerrin.Toolkit.CMD.Managers
{
    public class NavigationManager
    {
        public static NavigationManager Instance { get; } = new NavigationManager();
        public Stack<Menu> NavigationStack { get; } = new Stack<Menu>();

        public void Navigate(Menu menu)
        {
            NavigationStack.Push(menu);
        }

        public Menu Peek()
        {
            return NavigationStack.Peek();
        }

        public Menu GoBack()
        {
            return NavigationStack.Pop();
        }
    }
}

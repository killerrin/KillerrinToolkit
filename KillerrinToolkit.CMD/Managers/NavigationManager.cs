using System;
using System.Collections.Generic;
using System.Text;
using KillerrinToolkit.CMD.Menus;

namespace KillerrinToolkit.CMD.Managers
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

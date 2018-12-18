using Killerrin.Toolkit.CMD.Managers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Managers
{
    public class InteractiveMenuManager : IMenuManager
    {
        private static InteractiveMenuManager m_instance;
        public static InteractiveMenuManager Instance
        {
            get
            {
                if (m_instance == null) m_instance = new InteractiveMenuManager(new InputManager());
                return m_instance;
            }
        }

        public InputManager MenuInputManager { get; set; }

        public bool MenuRunning { get; set; } = false;
        public event EventHandler<object> OnMenuManagerExit;

        public InteractiveMenuManager(InputManager inputManager)
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
            // Utilize this for new menu navigation https://stackoverflow.com/questions/46908148/controlling-menu-with-the-arrow-keys-and-enter
            MenuRunning = true;
            while (MenuRunning)
            {
            }
        }
    }
}

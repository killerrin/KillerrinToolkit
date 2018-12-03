using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.CMD.Managers
{
    public class MenuManager
    {
        public event EventHandler<string> OnPreMenuRun;
        public event EventHandler<string> OnPostMenuRun;

        public MenuManager()
        {

        }

        public void RunMenuSystem()
        {
            // Utilize this for new menu navigation https://stackoverflow.com/questions/46908148/controlling-menu-with-the-arrow-keys-and-enter
        }
    }
}

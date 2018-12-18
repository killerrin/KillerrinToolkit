using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.CMD.Menus
{
    /// <summary>
    /// Blank MenuItem used to populate the Menu with Text
    /// </summary>
    public class TextMenuItem : Menu
    {
        public string Text { get; set; }
        public bool DisplayKey { get; set; }
        public Action SpecialAction { get; set; }

        public TextMenuItem(string name, string text, Action specialAction = null) : this(name, text, false, specialAction) { }
        public TextMenuItem(string name, bool displayKey, Action specialAction = null) : this(name, name, displayKey, specialAction) { }
        public TextMenuItem(string name, string text, bool displayKey, Action specialAction = null) : base(name)
        {
            Text = text;
            DisplayKey = displayKey;
            SpecialAction = specialAction;
        }
        public override void Run()
        {
            SpecialAction?.Invoke();
            NavigationManager.Instance.GoBack();
        }
    }
}

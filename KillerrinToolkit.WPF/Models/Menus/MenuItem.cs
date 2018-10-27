using GalaSoft.MvvmLight.Command;
using KillerrinToolkit.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KillerrinToolkit.WPF.Models.Menus
{
    public class MenuItem : ModelBase
    {
        private string m_text = "";
        public string Text
        {
            get { return m_text; }
            set
            {
                if (m_text == value) return;
                m_text = value;
                RaisePropertyChanged(nameof(Text));
            }
        }

        private string m_keyboardShortcut = "";
        public string KeyboardShortcut
        {
            get { return m_keyboardShortcut; }
            set
            {
                if (m_keyboardShortcut == value) return;
                m_keyboardShortcut = value;
                RaisePropertyChanged(nameof(KeyboardShortcut));
            }
        }

        private bool m_enabled = true;
        public bool Enabled
        {
            get { return m_enabled; }
            set
            {
                if (m_enabled == value) return;
                m_enabled = value;
                RaisePropertyChanged(nameof(Enabled));
            }
        }

        private Visibility m_visible = Visibility.Visible;
        public Visibility Visible
        {
            get { return m_visible; }
            set
            {
                if (m_visible == value) return;
                m_visible = value;
                RaisePropertyChanged(nameof(Visible));
            }
        }

        private Brush m_backgroundBrush = new SolidColorBrush(Colors.Transparent);
        public Brush BackgroundBrush
        {
            get { return m_backgroundBrush; }
            set
            {
                if (m_backgroundBrush == value) return;
                m_backgroundBrush = value;
                RaisePropertyChanged(nameof(BackgroundBrush));
            }
        }

        public RelayCommand Command { get { return new RelayCommand(CustomAction); } }
        public Action m_customAction;
        public Action CustomAction
        {
            get { return m_customAction; }
            set
            {
                if (m_customAction == value) return;
                m_customAction = value;
                RaisePropertyChanged(nameof(CustomAction));
                RaisePropertyChanged(nameof(Command));
            }
        }

        public MenuItem() : this("") { }
        public MenuItem(string text) : this(text, new SolidColorBrush(Colors.Transparent)) { }
        public MenuItem(string text, Action action) : this(text, new SolidColorBrush(Colors.Transparent), action) { }
        public MenuItem(string text, Brush brush) : this(text, brush, () => { Debug.WriteLine($"{text} Selected"); }) { }
        public MenuItem(string text, Brush brush, Action action)
        {
            Text = text;
            BackgroundBrush = brush;
            CustomAction = action;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}

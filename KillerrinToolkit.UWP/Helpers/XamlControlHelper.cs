using KillerrinToolkit.UWP.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KillerrinToolkit.UWP.Helpers
{
    public static class XamlControlHelper
    {
        public static bool DisplayDebugText { get; set; } = false;
        public static void ChangeProgressIndicator(object progressBar, bool isEnabled)
        {
            if (progressBar == null) return;

            if (progressBar is ProgressRing) { (progressBar as ProgressRing).IsActive = isEnabled; }
            else if (progressBar is ProgressBar) { (progressBar as ProgressBar).IsEnabled = isEnabled; }
            else if (progressBar is ProgressIndicator)
            {
                var progressIndicator = (progressBar as ProgressIndicator);
                progressIndicator.IsRingActive = isEnabled;
                progressIndicator.IsEnabled = isEnabled;

                if (isEnabled) progressIndicator.Visibility = Visibility.Visible;
                else progressIndicator.Visibility = Visibility.Collapsed;
            }
        }

        public static void SetDebugString(object textBlock, string str, bool forceDisplay = false)
        {
            if (string.IsNullOrEmpty(str)) return;
            Debug.WriteLine(str);

            if (textBlock == null) return;

            if (DisplayDebugText || forceDisplay)
            {
                (textBlock as TextBlock).Text = str;
            }
        }

        public static void LoseFocusOnTextBox(object sender)
        {
            if (sender == null) return;

            var control = (sender as Control);
            var isTabStop = control.IsTabStop;
            control.IsTabStop = false;
            control.IsEnabled = false;
            control.IsEnabled = true;
            control.IsTabStop = isTabStop;
        }
    }
}

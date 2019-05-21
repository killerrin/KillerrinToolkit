using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Killerrin.Toolkit.WPF.Services
{
    public class NavigationService
    {
        public static NavigationService Instance { get; set; }

        public Frame Frame { get; protected set; }
        public Uri CurrentPage { get; protected set; }
        public object Parameter { get; protected set; }

        public NavigationService(Frame frame)
        {
            Frame = frame;
            Frame.Navigating += Frame_OnNavigatingTo;
            Frame.Navigated += Frame_OnNavigatedTo;
        }
        public NavigationService(Frame frame, bool setDefaultInstance) : this(frame) { if (setDefaultInstance) { Instance = this; } }

        #region Events
        public event EventHandler<NavigatingCancelEventArgs> OnNavigatingTo;
        public event EventHandler<NavigationEventArgs> OnNavigatedTo;
        public event EventHandler<CancelEventArgs> OnNavigatingFrom;
        public event EventHandler<EventArgs> OnNavigatedFrom;
        private void Frame_OnNavigatingTo(object sender, NavigatingCancelEventArgs e) { OnNavigatingTo?.Invoke(sender, e); }
        private void Frame_OnNavigatedTo(object sender, NavigationEventArgs e) { OnNavigatedTo?.Invoke(sender, e); }
        #endregion

        public bool CanGoBack { get { return Frame.CanGoBack; } }
        public bool CanGoForward { get { return Frame.CanGoForward; } }

        public IEnumerable BackStack { get { return Frame.BackStack; } }
        public IEnumerable ForwardStack { get { return Frame.ForwardStack; } }

        public bool Navigate(Uri pageUri, object parameter)
        {
            Parameter = parameter;
            var result = Frame.Navigate(pageUri, parameter);
            CurrentPage = Frame.CurrentSource;

            return result;
        }

        public void GoBack()
        {
            // Notify Navigation Away and Cancel if flag is set
            CancelEventArgs args = new CancelEventArgs();
            OnNavigatingFrom?.Invoke(this, args);
            if (args.Cancel) return;

            // Go Back and notify OnNavigatedFrom
            Frame.GoBack();
            CurrentPage = Frame.CurrentSource;

            OnNavigatedFrom?.Invoke(this, new EventArgs());
        }
        public void GoForward()
        {
            Frame.GoForward();
            CurrentPage = Frame.CurrentSource;
        }
    }
}

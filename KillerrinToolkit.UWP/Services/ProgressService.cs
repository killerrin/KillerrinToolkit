using Killerrin.Toolkit.UWP.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Killerrin.Toolkit.UWP.Services
{
    public class ProgressService : ServiceBase
    {
        public ProgressIndicator ProgressIndicator { get; private set; }

        public ProgressService(ProgressIndicator progressIndicator)
        {
            ProgressIndicator = progressIndicator;
        }

        public void Reset()
        {
            IsRingEnabled = false;

            PercentageVisibility = Visibility.Visible;
            PercentageCompleted = 0.0;

            StatusMessage = "";

            Visibility = Visibility.Collapsed;
        }

        #region Show/Hide
        public void Show()
        {
            ProgressIndicator.Visibility = Visibility.Visible;
            ProgressIndicator.PercentageVisibility = Visibility.Visible;
        }
        public void Hide()
        {
            ProgressIndicator.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Enable/Disable Ring
        public void EnableRing()
        {
            IsRingEnabled = true;
        }
        public void DisableRing()
        {
            IsRingEnabled = false;
        }
        #endregion

        #region Individual Values
        public Visibility Visibility
        {
            get { return ProgressIndicator.Visibility; }
            set { ProgressIndicator.Visibility = value; }
        }

        public bool IsRingEnabled
        {
            get { return ProgressIndicator.IsRingActive; }
            set { ProgressIndicator.IsRingActive = value; }
        }

        public Visibility PercentageVisibility
        {
            get { return ProgressIndicator.PercentageVisibility; }
            set { ProgressIndicator.PercentageVisibility = value; }
        }

        public double PercentageCompleted
        {
            get { return ProgressIndicator.PercentageCompleted; }
            set { ProgressIndicator.PercentageCompleted = value; }
        }

        public string StatusMessage
        {
            get { return ProgressIndicator.StatusMessage; }
            set { ProgressIndicator.StatusMessage = value; }
        }
        #endregion

        public void SetIndicator(bool isRingEnabled, double percentage, string message, bool debugWriteLine = false)
        {
            if (!ServiceEnabled) return;

            IsRingEnabled = isRingEnabled;
            ProgressIndicator.PercentageCompleted = percentage;
            ProgressIndicator.StatusMessage = message;

            if (debugWriteLine)
                Debug.WriteLine(string.Format("{0} | {1} | {2}", isRingEnabled, percentage, message));
        }
        public void SetIndicatorAndShow(bool isRingEnabled, double percentage, string message, bool debugWriteLine = false)
        {
            if (!ServiceEnabled) return;

            SetIndicator(isRingEnabled, percentage, message, debugWriteLine);
            Show();
        }

        public override void EnableService()
        {
            Show();
            base.EnableService();
        }
        public override void DisableService()
        {
            Hide();
            base.DisableService();
        }
    }
}

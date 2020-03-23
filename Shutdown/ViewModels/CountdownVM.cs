using Shutdown.Models;
using System;
using System.Windows;

namespace Shutdown.ViewModels
{
    /// <summary>
    /// Countdown view model, it controls countdown and shows actual and rest value
    /// </summary>
    public class CountdownVM : ViewModel
    {
        /// <summary>
        /// Actual countdown value
        /// </summary>
        public string ActualValue
        {
            get { return this._ticker.RestTime.ToString(@"hh\:mm\:ss"); }
        }

        /// <summary>
        /// Value which left to 0
        /// </summary>
        public TimeSpan RestTime
        {
            get { return this._ticker.RestTime; }
        }

        private TimeTicker _ticker;

        /// <summary>
        /// Is invoked when countdown is finished
        /// </summary>
        public event Action Elapsed;

        public CountdownVM()
        {
            this._ticker = new TimeTicker(1);
            this._ticker.IntervalElapsed += _ticker_IntervalElapsed;
            this._ticker.Elapsed += _ticker_Elapsed;
        }

        private void _ticker_Elapsed()
        {
            this.Elapsed?.Invoke();
        }

        private void _ticker_IntervalElapsed()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                NotifiyPropertyChanged("ActualValue");
            });
        }

        public void Start(TimeSpan countdownValue)
        {
            this._ticker.Start(countdownValue);
            NotifiyPropertyChanged("ActualValue");
        }

        public void Stop()
        {
            this._ticker.Stop();
        }
    }
}

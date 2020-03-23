using System;
using System.Timers;

namespace Shutdown.Models
{
    /// <summary>
    /// Countdown from setted time to zero
    /// </summary>
    public class TimeTicker
    {
        private Timer _timer;
        private TimeSpan _countdownTime;
        private uint _intervalInSeconds;
        private uint _stepInSeconds;

        /// <summary>
        /// Predefined interval, after the interval IntervalElapsed event is invoked
        /// </summary>
        public uint IntervalInSeconds
        {
            get { return this._intervalInSeconds; }
            set 
            {
                this._intervalInSeconds = value;
                if (this._intervalInSeconds > this._stepInSeconds)
                    this._stepInSeconds = value;
            }
        }
        /// <summary>
        /// Time from which is countdowned
        /// </summary>
        public TimeSpan CountdownTime
        {
            get { return this._countdownTime; }
            private set 
            {
                this.RestTime = value;
                this._countdownTime = value;
            }
        }
        /// <summary>
        /// Elapsed time
        /// </summary>
        public TimeSpan ElapsedTime => this._countdownTime - this.RestTime;
        /// <summary>
        /// Time to zero
        /// </summary>
        public TimeSpan RestTime { get; private set; }
        /// <summary>
        /// Invoked after predefined interval
        /// </summary>
        public event Action IntervalElapsed;
        /// <summary>
        /// Invoked at zero
        /// </summary>
        public event Action Elapsed;
        public TimeTicker(uint intervalInSeconds)
        {
            InitTimer();
            this._intervalInSeconds = intervalInSeconds;
        }

        private void InitTimer()
        {
            this._timer = new Timer();
            this._timer.Interval = 1000;
            this._timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.RestTime += TimeSpan.FromSeconds(-1);
            _stepInSeconds += 1;

            if (this._stepInSeconds == this._intervalInSeconds)
            {
                this.IntervalElapsed.Invoke();
                this._stepInSeconds = 0;
            }
            if (this.RestTime.TotalSeconds <= 0)
            {
                this._timer.Stop();
                this.Elapsed.Invoke();
            }
        }

        public void Start(TimeSpan countdownTime)
        {
            this.CountdownTime = countdownTime;
            this._timer.Start();
        }
        public void Stop()
        {
            this._timer.Stop();
        }

        public void Clear()
        {
            this._timer.Stop();
            this.RestTime = this.CountdownTime;
            this._stepInSeconds = 0;
        }
    }
}

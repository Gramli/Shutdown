using System;
using System.Timers;

namespace Shutdown.Model
{
    public class TimeTicker
    {
        private Timer _timer;
        private TimeSpan _countdownTime;
        private uint _intervalInSeconds;
        private uint _stepInSeconds;

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
        public TimeSpan CountdownTime
        {
            get { return this._countdownTime; }
            private set 
            {
                this.RestTime = value;
                this._countdownTime = value;
            }
        }
        public TimeSpan ElapsedTime => this._countdownTime - this.RestTime;
        public TimeSpan RestTime { get; private set; }

        public event Action IntervalElapsed;

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
            this.Clear();
        }

        public void Pause()
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

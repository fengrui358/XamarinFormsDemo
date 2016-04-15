using System;
using System.Timers;
using Xamarin.Forms;
using XamarinFormsDemo.Helper;
using XamarinFormsDemo.Interface;

[assembly: Dependency(typeof(AdvancedTimerImplementation))]
namespace XamarinFormsDemo.Helper
{
    /// <summary>
    /// AdvancedTimer Implementation
    /// </summary>
    public class AdvancedTimerImplementation : IAdvancedTimer
    {
        private Timer _timer;
        private int _interval;
        private bool _autoReset;

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }

        /// <summary>
        /// Used for initializing timer and options
        /// </summary>
        public void InitTimer(int interval, EventHandler e, bool autoReset)
        {
            if (this._timer == null)
            {
                this._timer = new Timer(interval);
                this._timer.Elapsed += new ElapsedEventHandler(e);
            }

            this._interval = interval;
            this._autoReset = autoReset;

            this._timer.AutoReset = autoReset;
        }

        /// <summary>
        /// Used for starting timer
        /// </summary>
        public void StartTimer()
        {
            if (this._timer != null)
            {
                if (!this._timer.Enabled)
                {
                    this._timer.Start();
                }
            }
            else
            {
                throw new NullReferenceException("Timer not initialized. You should call InitTimer function first!");
            }
        }

        /// <summary>
        /// Used for stopping timer
        /// </summary>
        public void StopTimer()
        {
            if (this._timer != null)
            {
                if (this._timer.Enabled)
                {
                    this._timer.Stop();
                }
            }
            else
            {
                throw new NullReferenceException("Timer not initialized. You should call InitTimer function first!");
            }
        }

        /// <summary>
        /// Used for checking timer status
        /// </summary>
        public bool IsTimerEnabled()
        {
            return this._timer.Enabled;
        }

        /// <summary>
        /// Used for checking timer interval
        /// </summary>
        public int GetInterval()
        {
            return this._interval;
        }

        /// <summary>
        /// Used for setting timer interval
        /// </summary>
        public void SetInterval(int interval)
        {
            this._interval = interval;
            this._timer.Interval = interval;
        }
    }
}
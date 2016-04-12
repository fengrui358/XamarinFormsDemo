using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsDemo.Interface
{
    public interface IAdvancedTimer
    {
        /// <summary>
        /// Used for initializing timer and options
        /// </summary>
        void InitTimer(int interval, EventHandler e, bool autoReset);

        /// <summary>
        /// Used for starting timer
        /// </summary>
        void StartTimer();

        /// <summary>
        /// Used for stopping timer
        /// </summary>
        void StopTimer();

        /// <summary>
        /// Used for checking timer status
        /// </summary>
        bool IsTimerEnabled();

        /// <summary>
        /// Used for checking timer interval
        /// </summary>
        int GetInterval();

        /// <summary>
        /// Used for setting timer interval
        /// </summary>
        void SetInterval(int interval);
    }
}

using System;
using System.Diagnostics;

namespace BarNone.DataLift.UI.ViewModels.Common
{
    public class CustomTimer
    {
        Stopwatch internalTimer;
        private long offset;

        public long ElapsedMilliseconds
        {
            get
            {
                return internalTimer.ElapsedMilliseconds + offset;
            }

            set
            {
                offset = value;
            }
        }

        public CustomTimer()
        {
            internalTimer = new Stopwatch();
            offset = 0;
        }

        public void Start()
        {
            internalTimer.Start();
        }

        public void Stop()
        {
            internalTimer.Stop();
        }

        public void Restart()
        {
            internalTimer.Restart();
        }

        public TimeSpan GetTimeSpanPosition()
        {
            return new TimeSpan(internalTimer.ElapsedTicks);
        }
    }
}

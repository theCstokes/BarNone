using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            offset = 0;
            internalTimer.Restart();
        }

        //public long ElapsedMilliseconds()
        //{
        //    return internalTimer.ElapsedMilliseconds;
        //}
    }
}

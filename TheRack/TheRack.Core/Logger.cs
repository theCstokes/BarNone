using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.Core
{
    public static class Logger
    {
        public static void Info(string value)
        {
            System.Diagnostics.Debug.WriteLine(value, "INFO");
        }

        public static void Info(object value)
        {
            System.Diagnostics.Debug.WriteLine(value, "INFO");
        }
    }
}

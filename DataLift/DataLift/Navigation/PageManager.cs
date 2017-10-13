using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLift.Nav
{
    public static class PageManager
    {
        internal static MainWindow window { private get; set; }

        public static void SwitchPage(UIPages newPage)
        {
            window.Navigate(newPage);
        }
    }
}

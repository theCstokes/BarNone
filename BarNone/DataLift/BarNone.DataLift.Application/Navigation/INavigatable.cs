using BarNone.DataLift.UI.Nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.UI.Nav
{
    public interface INavigatable
    {
        void Navigate(UIPages nextPage);
    }
}

using BarNone.DataLift.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BarNone.DataLift.UI.Nav
{
    public class UIPages
    {
        public UserControl page { get; }

        public static readonly UIPages LoginView = new UIPages(new LoginScreen());
        public static readonly UIPages DataRecorderView = new UIPages(new DataRecorderScreen());
        public static readonly UIPages RegistrationView = new UIPages(new RegistrationScreen());

        private UIPages(UserControl page)
        {
            this.page = page;
        }
    }
}

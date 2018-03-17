using BarNone.DataLift.UI.Nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BarNone.DataLift.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INavigatable
    {
        /// <summary>
        /// Instantiates a new Data Lift window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //Remove junk page management
            PageManager.Window = this;
            PageManager.SwitchPage(UIPages.LoginView);

            UserControlManager.SwitchPage(UIPages.DataRecorderView);
        }

        public void Navigate(UIPages nextPage)
        {
            Content = nextPage.Page;
        }
    }
}

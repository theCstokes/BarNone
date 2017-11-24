using BarNone.DataLift.APIRequest;
using BarNone.DataLift.UI.ViewModels;
using BarNone.Shared.DataTransfer;
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

namespace BarNone.DataLift.UI.Views
{
    /// <summary>
    /// Interaction logic for RegistrationScreen.xaml
    /// </summary>
    public partial class RegistrationScreen : UserControl
    {
        public RegistrationScreen()
        {
            InitializeComponent();

            var vm = DataContext as ViewModelBase;
            Loaded += (a, b) => vm.Loaded();
            Loaded += (a, b) => LoginPassword.Clear();
            Loaded += (a, b) => ConfirmPassword.Clear();

        }

        private void PasswordBoxChangedEvent(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
        }
        
        private void PasswordConfirmBoxChangedEvent(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)DataContext).ConfirmPassword = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}

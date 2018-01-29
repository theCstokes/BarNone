using BarNone.DataLift.UI.ViewModels;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace BarNone.DataLift.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : UserControl
    {
        /// <summary>
        /// Initializes the view
        /// </summary>
        public LoginScreen()
        {
            InitializeComponent();
            //Temporary
            //  Initializes the Password structure that wpf uses
            //  Fixes the issue of having a delay of text on input
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = new SecureString();
                var vm = (ViewModelBase)DataContext;
                Loaded += (a, b) => vm.Loaded();
                Loaded += (a, b) => LoginPassword.Clear();
            }
        }


        /// <summary>
        /// Bindings to password box being changed
        ///     <paramref name="sender"/> is a View object, and thus this event is a middle man to the VM
        ///     for loose coupling purposes
        /// </summary>
        /// <param name="sender">Password box object refference</param>
        /// <param name="e">Unused, supplied by the viewbox event</param>
        private void PasswordBoxChangedEvent(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}

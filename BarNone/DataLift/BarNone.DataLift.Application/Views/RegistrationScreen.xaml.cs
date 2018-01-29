using BarNone.DataLift.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BarNone.DataLift.UI.Views
{
    /// <summary>
    /// Interaction logic for RegistrationScreen.xaml
    /// </summary>
    public partial class RegistrationScreen : UserControl
    {
        /// <summary>
        /// Initializes the view
        /// </summary>
        public RegistrationScreen()
        {
            InitializeComponent();

            var vm = DataContext as ViewModelBase;
            Loaded += (a, b) => vm.Loaded();
            Loaded += (a, b) => LoginPassword.Clear();
            Loaded += (a, b) => ConfirmPassword.Clear();

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
                ((dynamic)DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
        }
        
        /// <summary>
        /// Bindings to password confirm box being changed
        ///     <paramref name="sender"/> is a View object, and thus this event is a middle man to the VM
        ///     for loose coupling purposes
        /// </summary>
        /// <param name="sender">Password box object refference</param>
        /// <param name="e">Unused, supplied by the viewbox event</param>
        private void PasswordConfirmBoxChangedEvent(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)DataContext).ConfirmPassword = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}

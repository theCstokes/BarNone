using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

namespace BarNone.DataLift.UI.Views
{
    /// <summary>
    /// Interaction logic for YesNoDialogScreen.xaml
    /// </summary>
    public partial class YesNoDialogScreen : UserControl
    {
        

        /// <summary>
        /// Initializes the view
        /// </summary>
        public YesNoDialogScreen()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, this);
        }
    }
}

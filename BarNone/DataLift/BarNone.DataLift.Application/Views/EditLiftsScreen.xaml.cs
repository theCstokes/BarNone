using BarNone.DataLift.UI.ViewModels;
using System.Windows.Controls;

namespace BarNone.DataLift.UI.Views
{
    /// <summary>
    /// Interaction logic for EditLiftsScreen.xaml
    /// </summary>
    public partial class EditLiftsScreen : UserControl
    {
        /// <summary>
        /// Initializes the view
        /// </summary>
        public EditLiftsScreen()
        {
            InitializeComponent();
            var vm = DataContext as ViewModelBase;

            Loaded += (a, b) => vm.Loaded();
            Unloaded += (a, b) => vm.Closed();
        }
    }
}

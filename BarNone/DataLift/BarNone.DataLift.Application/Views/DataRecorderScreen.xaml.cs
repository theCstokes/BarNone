using System.Windows.Controls;
using BarNone.DataLift.UI.ViewModels;

namespace BarNone.DataLift.UI.Views
{
    /// <summary>
    /// Interaction logic for DataRecorder.xaml
    /// </summary>
    public partial class DataRecorderScreen : UserControl
    {

        #region Constructor
        /// <summary>
        /// Initialize the component on creation.
        /// </summary>
        public DataRecorderScreen()
        {
            InitializeComponent();
            var vm = DataContext as ViewModelBase;

            Loaded += (a, b) => vm.Loaded();
            Unloaded += (a, b) => vm.Closed();
        }

        #endregion
        
    }

}

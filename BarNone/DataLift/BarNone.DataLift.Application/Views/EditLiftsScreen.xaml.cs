using BarNone.DataLift.UI.ViewModels;
using System.Windows.Controls;

namespace BarNone.DataLift.UI.Views
{
    /// <summary>
    /// Interaction logic for EditLiftsScreen.xaml
    /// </summary>
    public partial class EditLiftsScreen : UserControl
    {
        EditLiftsScreenVM vm;

        /// <summary>
        /// Initializes the view
        /// </summary>
        public EditLiftsScreen()
        {
            InitializeComponent();
            vm = DataContext as EditLiftsScreenVM;

            Loaded += (a, b) => vm.Loaded();
            Unloaded += (a, b) => vm.Closed();

            VideoPlayer.LoadedBehavior = MediaState.Manual;

            //We get Play Pause and Stop, the rest must occur through bindings!
            vm.PlayRequested += (sender, e) => VideoPlayer.Play();
            vm.StopRequested += (sender, e) => VideoPlayer.Stop();
            vm.PauseRequested += (sender, e) => VideoPlayer.Pause();
            vm.UpdatePositionEvent += (sender, e) => VideoPlayer.Position = e.Position;
        }


    }
}

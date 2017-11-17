using Microsoft.Kinect;
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
using BarNone.DataLift.DataModel.KinectData;
using System.ComponentModel;
using BarNone.DataLift.UI.ViewModels;

namespace BarNone.DataLift.UI.Views
{
    /// <summary>
    /// Interaction logic for DataRecorder.xaml
    /// </summary>
    public partial class DataRecorderScreen : UserControl
    {
        private DataRecorderVM ViewModel;

        #region Constructor
        public DataRecorderScreen()
        {
            InitializeComponent();
            ViewModel = (DataRecorderVM)DataContext;
        }
        #endregion

        /// <summary>
        /// Execute start up tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.On_Loaded();
        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void UserControl_Closed(object sender, RoutedEventArgs e)
        {
            ViewModel.On_Closed();
        }

    }
}

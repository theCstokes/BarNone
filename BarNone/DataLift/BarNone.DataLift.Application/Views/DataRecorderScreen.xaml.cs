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

        #region Constructor
        public DataRecorderScreen()
        {
            InitializeComponent();
            var vm = DataContext as ViewModelBase;

            Loaded += (a, b) => vm.Loaded();
            Unloaded += (a, b) => vm.Closed();
        }

        #endregion

        //#region dependancy properties
        //public bool IsRecorderVisible
        //{
        //    get { return (bool)GetValue(IsRecorderVisibleProperty); }
        //    set { SetValue(IsRecorderVisibleProperty, value); }
        //}


        //// Using a DependencyProperty as the backing store for IsRecorderVisible.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IsRecorderVisibleProperty =
        //    DependencyProperty.Register("IsRecorderVisible", typeof(bool), typeof(DataRecorderScreen), new PropertyMetadata());

        //private static void OnIsRecorderVisibleChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        //{

        //}
        //#endregion
    }

}

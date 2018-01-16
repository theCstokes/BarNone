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

namespace BarNone.DataLift.UI.Views.CustomComponents
{
    /// <summary>
    /// Interaction logic for VideoControlsBar.xaml
    /// </summary>
    public partial class VideoControlsBar : UserControl
    {
        public VideoControlsBar()
        {
            InitializeComponent();
        }

        public ICommand CommandPlay
        {
            get { return (ICommand)GetValue(CommandPlayProperty); }
            set { SetValue(CommandPlayProperty, value); }
        }
        
        public static readonly DependencyProperty CommandPlayProperty =
            DependencyProperty.Register("CommandPlay", typeof(ICommand), typeof(VideoControlsBar));

        public ICommand CommandPause
        {
            get { return (ICommand)GetValue(CommandPauseProperty); }
            set { SetValue(CommandPauseProperty, value); }
        }
        
        public static readonly DependencyProperty CommandPauseProperty =
            DependencyProperty.Register("CommandPause", typeof(ICommand), typeof(VideoControlsBar));

        public ICommand CommandResetInterval
        {
            get { return (ICommand)GetValue(CommandResetIntervalProperty); }
            set { SetValue(CommandResetIntervalProperty, value); }
        }

        public static readonly DependencyProperty CommandResetIntervalProperty =
            DependencyProperty.Register("CommandResetInterval", typeof(ICommand), typeof(VideoControlsBar));

        public ICommand CommandSlowMo
        {
            get { return (ICommand)GetValue(CommandSlowMoProperty); }
            set { SetValue(CommandSlowMoProperty, value); }
        }

        public static readonly DependencyProperty CommandSlowMoProperty =
            DependencyProperty.Register("CommandSlowMo", typeof(ICommand), typeof(VideoControlsBar));

        public ICommand CommandFastForward
        {
            get { return (ICommand)GetValue(CommandFastForwardProperty); }
            set { SetValue(CommandFastForwardProperty, value); }
        }

        public static readonly DependencyProperty CommandFastForwardProperty =
            DependencyProperty.Register("CommandFastForward", typeof(ICommand), typeof(VideoControlsBar));
    }
}

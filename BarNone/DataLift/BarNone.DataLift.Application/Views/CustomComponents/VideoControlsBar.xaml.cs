using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BarNone.DataLift.UI.Views.CustomComponents
{
    /// <summary>
    /// Interaction logic for VideoControlsBar.xaml
    /// </summary>
    public partial class VideoControlsBar : UserControl
    {
        /// <summary>
        /// Initializes the view
        /// </summary>
        public VideoControlsBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bindable command to execute when the play button is clicked
        /// </summary>
        public ICommand CommandPlay
        {
            get { return (ICommand)GetValue(CommandPlayProperty); }
            set { SetValue(CommandPlayProperty, value); }
        }

        /// <summary>
        /// Dependency parameter to bind to <see cref="CommandPlay"/> on object creation, this binding is one way to the command reference
        /// </summary>
        public static readonly DependencyProperty CommandPlayProperty =
            DependencyProperty.Register("CommandPlay", typeof(ICommand), typeof(VideoControlsBar));

        /// <summary>
        /// Bindable command to execute when the pause button is clicked
        /// </summary>
        public ICommand CommandPause
        {
            get { return (ICommand)GetValue(CommandPauseProperty); }
            set { SetValue(CommandPauseProperty, value); }
        }

        /// <summary>
        /// Dependency parameter to bind to <see cref="CommandPause"/> on object creation, this binding is one way to the command reference
        /// </summary>
        public static readonly DependencyProperty CommandPauseProperty =
            DependencyProperty.Register("CommandPause", typeof(ICommand), typeof(VideoControlsBar));

        /// <summary>
        /// Bindable command to execute when the reset interval command button is clicked
        /// </summary>
        public ICommand CommandResetInterval
        {
            get { return (ICommand)GetValue(CommandResetIntervalProperty); }
            set { SetValue(CommandResetIntervalProperty, value); }
        }

        /// <summary>
        /// Dependency parameter to bind to <see cref="CommandResetInterval"/> on object creation, this binding is one way to the command reference
        /// </summary>
        public static readonly DependencyProperty CommandResetIntervalProperty =
            DependencyProperty.Register("CommandResetInterval", typeof(ICommand), typeof(VideoControlsBar));

        /// <summary>
        /// Bindable command to execute when the slow motion button is clicked
        /// </summary>
        public ICommand CommandSlowMo
        {
            get { return (ICommand)GetValue(CommandSlowMoProperty); }
            set { SetValue(CommandSlowMoProperty, value); }
        }

        /// <summary>
        /// Dependency parameter to bind to <see cref="CommandSlowMo"/> on object creation, this binding is one way to the command reference
        /// </summary>
        public static readonly DependencyProperty CommandSlowMoProperty =
            DependencyProperty.Register("CommandSlowMo", typeof(ICommand), typeof(VideoControlsBar));

        /// <summary>
        /// Bindable command to execute when the fast forward button is clicked
        /// </summary>
        public ICommand CommandFastForward
        {
            get { return (ICommand)GetValue(CommandFastForwardProperty); }
            set { SetValue(CommandFastForwardProperty, value); }
        }

        /// <summary>
        /// Dependency parameter to bind to <see cref="CommandFastForward"/> on object creation, this binding is one way to the command reference
        /// </summary>
        public static readonly DependencyProperty CommandFastForwardProperty =
            DependencyProperty.Register("CommandFastForward", typeof(ICommand), typeof(VideoControlsBar));
    }
}

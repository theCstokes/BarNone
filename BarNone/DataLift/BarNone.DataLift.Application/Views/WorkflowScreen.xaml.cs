using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BarNone.DataLift.UI.Views
{
    /// <summary>
    /// Interaction logic for WorkflowScreen.xaml
    /// </summary>
    public partial class WorkflowScreen : UserControl
    {
        /// <summary>
        /// Initializes the view
        /// </summary>
        public WorkflowScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bindable parameter to set the style of the 1st workflow
        /// </summary>
        public ViewModels.ControlHolderVM.ButtonState StepOneStyle
        {
            get { return (ViewModels.ControlHolderVM.ButtonState)GetValue(StepOneStyleProperty); }
            set { SetValue(StepOneStyleProperty, value); }
        }

        /// <summary>
        /// Dependency parameter to bind to <see cref="StepOneStyle"/> on object creation, this binding is two way
        /// </summary>
        public static readonly DependencyProperty StepOneStyleProperty =
            DependencyProperty.Register("StepOneStyle", typeof(ViewModels.ControlHolderVM.ButtonState), typeof(WorkflowScreen),
                new FrameworkPropertyMetadata(ViewModels.ControlHolderVM.ButtonState.Selected, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Bindable parameter to set the style of the 2nd workflow
        /// </summary>
        public ViewModels.ControlHolderVM.ButtonState StepTwoStyle
        {
            get { return (ViewModels.ControlHolderVM.ButtonState)GetValue(StepTwoStyleProperty); }
            set { SetValue(StepTwoStyleProperty, value); }
        }
        /// <summary>
        /// Dependency parameter to bind to <see cref="StepTwoStyle"/> on object creation, this binding is two way
        /// </summary>
        public static readonly DependencyProperty StepTwoStyleProperty =
            DependencyProperty.Register("StepTwoStyle", typeof(ViewModels.ControlHolderVM.ButtonState), typeof(WorkflowScreen),
                new FrameworkPropertyMetadata(ViewModels.ControlHolderVM.ButtonState.CanGoTo, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Bindable parameter to set the style of the 3rd workflow
        /// </summary>
        public ViewModels.ControlHolderVM.ButtonState StepThreeStyle
        {
            get { return (ViewModels.ControlHolderVM.ButtonState)GetValue(StepThreeStyleProperty); }
            set { SetValue(StepThreeStyleProperty, value); }
        }

        /// <summary>
        /// Dependency parameter to bind to <see cref="StepThreeStyle"/> on object creation, this binding is two way
        /// </summary>
        public static readonly DependencyProperty StepThreeStyleProperty =
            DependencyProperty.Register("StepThreeStyle", typeof(ViewModels.ControlHolderVM.ButtonState), typeof(WorkflowScreen),
                new FrameworkPropertyMetadata(ViewModels.ControlHolderVM.ButtonState.Disabled, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Bindable parameter to control the progress bar animation between the first and second workflow options
        /// </summary>
        public int StepTwoProgress
        {
            get { return (int)GetValue(StepTwoProgressProperty); }
            set { SetValue(StepTwoProgressProperty, value); }
        }


        /// <summary>
        /// Dependency parameter to bind to <see cref="StepTwoProgress"/> on object creation, this binding is two way
        /// </summary>
        public static readonly DependencyProperty StepTwoProgressProperty =
            DependencyProperty.Register("StepTwoProgress", typeof(int), typeof(WorkflowScreen), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        /// <summary>
        /// Bindable parameter to control the progress bar animation between the second and third workflow options
        /// </summary>
        public int StepThreeProgress
        {
            get { return (int)GetValue(StepThreeProgressProperty); }
            set { SetValue(StepThreeProgressProperty, value); }
        }

        /// <summary>
        /// Dependency parameter to bind to <see cref="StepThreeProgress"/> on object creation, this binding is two way
        /// </summary>
        public static readonly DependencyProperty StepThreeProgressProperty =
            DependencyProperty.Register("StepThreeProgress", typeof(int), typeof(WorkflowScreen), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Bindable parameter to control connand of the 1st button in workflow diagram (record step)
        /// </summary>
        public ICommand StepOneCommand
        {
            get { return ( ICommand)GetValue(StepOneCommandProperty); }
            set { SetValue(StepOneCommandProperty, value); }
        }
        /// <summary>
        /// Dependency parameter to bind to <see cref="StepOneCommand"/> on object creation, this binding is one way
        /// </summary>
        public static readonly DependencyProperty StepOneCommandProperty =
            DependencyProperty.Register("StepOneCommand", typeof( ICommand), typeof(WorkflowScreen), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Bindable parameter to control connand of the 2nd button in workflow diagram (edit step)
        /// </summary>
        public ICommand StepTwoCommand
        {
            get { return (ICommand)GetValue(StepTwoCommandProperty); }
            set { SetValue(StepTwoCommandProperty, value); }
        }
        /// <summary>
        /// Dependency parameter to bind to <see cref="StepTwoCommand"/> on object creation, this binding is one way
        /// </summary>
        public static readonly DependencyProperty StepTwoCommandProperty =
            DependencyProperty.Register("StepTwoCommand", typeof(ICommand), typeof(WorkflowScreen), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Bindable parameter to control connand of the 3rd button in workflow diagram (send step)
        /// </summary>
        public ICommand StepThreeCommand
        {
            get { return (ICommand)GetValue(StepThreeCommandProperty); }
            set { SetValue(StepThreeCommandProperty, value); }
        }
        /// <summary>
        /// Dependency parameter to bind to <see cref="StepThreeCommand"/> on object creation, this binding is one way
        /// </summary>
        public static readonly DependencyProperty StepThreeCommandProperty =
            DependencyProperty.Register("StepThreeCommand", typeof(ICommand), typeof(WorkflowScreen), new FrameworkPropertyMetadata(null));
    }
}

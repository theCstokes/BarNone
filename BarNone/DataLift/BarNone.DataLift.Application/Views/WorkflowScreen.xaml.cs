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
        /// Bindable parameter to set the opacity of the 2nd workflow
        /// </summary>
        public double StepTwoOpacity
        {
            get { return (double)GetValue(StepTwoOpacityProperty); }
            set { SetValue(StepTwoOpacityProperty, value); }
        }
        /// <summary>
        /// Dependency parameter to bind to <see cref="StepTwoOpacity"/> on object creation, this binding is two way
        /// </summary>
        public static readonly DependencyProperty StepTwoOpacityProperty =
            DependencyProperty.Register("StepTwoOpacity", typeof(double), typeof(WorkflowScreen), new FrameworkPropertyMetadata(0.5, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        
        // <summary>
        /// Bindable parameter to set the opacity of the 3rd workflow
        /// </summary>
        public double StepThreeOpacity
        {
            get { return (double)GetValue(StepThreeOpacityProperty); }
            set { SetValue(StepThreeOpacityProperty, value); }
        }
        /// <summary>
        /// Dependency parameter to bind to <see cref="StepThreeOpacity"/> on object creation, this binding is two way
        /// </summary>
        public static readonly DependencyProperty StepThreeOpacityProperty =
            DependencyProperty.Register("StepThreeOpacity", typeof(double), typeof(WorkflowScreen), new FrameworkPropertyMetadata(0.5, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        /// <summary>
        /// Bindable parameter to check if the third workflow step is enabled
        /// </summary>
        public bool IsStepThreeEnabled
        {
            get { return (bool)GetValue(IsStepThreeEnabledProperty); }
            set { SetValue(IsStepThreeEnabledProperty, value); }
        }
        /// <summary>
        /// Dependency parameter to bind to <see cref="IsStepThreeEnabled"/> on object creation, this binding is two way
        /// </summary>
        public static readonly DependencyProperty IsStepThreeEnabledProperty =
            DependencyProperty.Register("IsStepThreeEnabled", typeof(bool), typeof(WorkflowScreen), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

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

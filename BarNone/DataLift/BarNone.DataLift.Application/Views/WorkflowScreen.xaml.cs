using System.Windows;
using System.Windows.Controls;

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
        /// Bindable parameter to check if the second workflow step is enabled
        /// </summary>
        public double IsStepTwoEnabled
        {
            get
            {
                return (double)GetValue(IsStepTwoEnabledProperty);
            }
            set
            {
                SetValue(IsStepTwoEnabledProperty, value);
            }
        }

        /// <summary>
        /// Dependency parameter to bind to <see cref="IsStepTwoEnabled"/> on object creation, this binding is two way
        /// </summary>
        public static readonly DependencyProperty IsStepTwoEnabledProperty =
            DependencyProperty.Register("IsStepTwoEnabled", typeof(double), typeof(WorkflowScreen), new FrameworkPropertyMetadata(0.5, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        /// <summary>
        /// Bindable parameter to check if the third workflow step is enabled
        /// </summary>
        public double IsStepThreeEnabled
        {
            get { return (double)GetValue(IsStepThreeEnabledProperty); }
            set { SetValue(IsStepThreeEnabledProperty, value); }
        }


        /// <summary>
        /// Dependency parameter to bind to <see cref="IsStepThreeEnabled"/> on object creation, this binding is two way
        /// </summary>
        public static readonly DependencyProperty IsStepThreeEnabledProperty =
            DependencyProperty.Register("IsStepThreeEnabled", typeof(double), typeof(WorkflowScreen), new FrameworkPropertyMetadata(0.5, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



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
            
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BarNone.DataLift.UI.Views
{
    /// <summary>
    /// Interaction logic for WorkflowScreen.xaml
    /// </summary>
    public partial class WorkflowScreen : UserControl
    {
        public WorkflowScreen()
        {
            InitializeComponent();
        }

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

        // Using a DependencyProperty as the backing store for IsStepTwoEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsStepTwoEnabledProperty =
            DependencyProperty.Register("IsStepTwoEnabled", typeof(double), typeof(WorkflowScreen), new FrameworkPropertyMetadata(0.5, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double IsStepThreeEnabled
        {
            get { return (double)GetValue(IsStepThreeEnabledProperty); }
            set { SetValue(IsStepThreeEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsStepThreeEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsStepThreeEnabledProperty =
            DependencyProperty.Register("IsStepThreeEnabled", typeof(double), typeof(WorkflowScreen), new FrameworkPropertyMetadata(0.5, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public int StepTwoProgress
        {
            get { return (int)GetValue(StepTwoProgressProperty); }
            set { SetValue(StepTwoProgressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StepTwoProgress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepTwoProgressProperty =
            DependencyProperty.Register("StepTwoProgress", typeof(int), typeof(WorkflowScreen), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int StepThreeProgress
        {
            get { return (int)GetValue(StepThreeProgressProperty); }
            set { SetValue(StepThreeProgressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StepThreeProgress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepThreeProgressProperty =
            DependencyProperty.Register("StepThreeProgress", typeof(int), typeof(WorkflowScreen), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    }

}

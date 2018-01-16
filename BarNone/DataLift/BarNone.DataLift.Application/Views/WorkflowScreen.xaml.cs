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

        public bool IsStepTwoEnabled
        {
            get
            {
                return (bool)GetValue(IsStepTwoEnabledProperty);
            }
            set
            {
                SetValue(IsStepTwoEnabledProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for IsStepTwoEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsStepTwoEnabledProperty =
            DependencyProperty.Register("IsStepTwoEnabled", typeof(bool), typeof(WorkflowScreen), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private static void Testeroonie(DependencyObject d, DependencyPropertyChangedEventArgs baseValue)
        {
            WorkflowScreen WorkFlowDataContextParent = d as WorkflowScreen;
            
        }

        public bool IsStepThreeEnabled
        {
            get { return (bool)GetValue(IsStepThreeEnabledProperty); }
            set { SetValue(IsStepThreeEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsStepThreeEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsStepThreeEnabledProperty =
            DependencyProperty.Register("IsStepThreeEnabled", typeof(bool), typeof(WorkflowScreen), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public int StepTwoProgress
        {
            get { return (int)GetValue(StepTwoProgressProperty); }
            set { SetValue(StepTwoProgressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StepTwoProgress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepTwoProgressProperty =
            DependencyProperty.Register("StepTwoProgress", typeof(int), typeof(WorkflowScreen), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    }

}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BarNone.DataLift.UI.Views.CustomComponents
{
    /// <summary>
    /// Interaction logic for Scrubber.xaml
    /// </summary>
    public partial class Scrubber : UserControl
    {
        /// <summary>
        /// Initializes the view
        /// </summary>
        public Scrubber()
        {
            InitializeComponent();
        }

        //public string CurrentStringTime
        //{
        //    get
        //    {
        //        TimeSpan currentTime = new TimeSpan(0, 0, 0, 0, (int)GetValue(CurrentValueProperty));
        //        return $"{currentTime.Minutes:00}:{currentTime.Seconds:00}.{currentTime.Milliseconds:000}";
        //    }
        //}

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(Scrubber), new UIPropertyMetadata(0d));

        public double LowerValue
        {
            get { return (double)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, value); }
        }

        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(double), typeof(Scrubber), 
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double UpperValue
        {
            get { return (double)GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, value); }
        }

        public static readonly DependencyProperty UpperValueProperty =
            DependencyProperty.Register("UpperValue", typeof(double), typeof(Scrubber), 
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double CurrentValue
        {
            get { return (double)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue", typeof(double), typeof(Scrubber), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(Scrubber), new UIPropertyMetadata(1d));

        private void LowerSlider_MouseMove(object sender, MouseEventArgs e)
        {
            tt_lower.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;

            tt_lower.HorizontalOffset = e.GetPosition((IInputElement)sender).X + 20;
            tt_lower.VerticalOffset = e.GetPosition((IInputElement)sender).Y -
                LowerSlider.TranslatePoint(e.GetPosition((IInputElement)sender), (UIElement)sender).Y - 40;

            var temp = new TimeSpan(0, 0, 0, 0, (int)LowerValue);
            tt_lower.Content = $"{temp.Minutes:00}:{temp.Seconds:00}.{temp.Milliseconds:000}";
        }

        private void UpperSlider_MouseMove(object sender, MouseEventArgs e)
        {
            tt_upper.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;

            tt_upper.HorizontalOffset = e.GetPosition((IInputElement)sender).X + 20;
            tt_upper.VerticalOffset = e.GetPosition((IInputElement)sender).Y -
                UpperSlider.TranslatePoint(e.GetPosition((IInputElement)sender), (UIElement)sender).Y - 40;

            var temp = new TimeSpan(0, 0, 0, 0, (int)UpperValue);
            tt_upper.Content = $"{temp.Minutes:00}:{temp.Seconds:00}.{temp.Milliseconds:000}";
        }
    }
}

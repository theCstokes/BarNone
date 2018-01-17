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
using SysDraw = System.Drawing;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using System.IO;

namespace BarNone.DataLift.UI.Views.CustomComponents
{
    /// <summary>
    /// Interaction logic for VideoViewer.xaml
    /// </summary>
    public partial class VideoViewer : UserControl
    {
        public VideoViewer()
        {
            InitializeComponent();
        }
        
        public ImageSource ImageSourceLeft
        {
            get { return (ImageSource)GetValue(ImageSourceLeftProperty); }
            set { SetValue(ImageSourceLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsStepTwoEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceLeftProperty =
            DependencyProperty.Register("ImageSourceLeft", typeof(ImageSource), typeof(VideoViewer), new FrameworkPropertyMetadata(DrawFilledRectangle(200,200), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public ImageSource ImageSourceRight
        {
            get { return (ImageSource)GetValue(ImageSourceRightProperty); }
            set { SetValue(ImageSourceRightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsStepTwoEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceRightProperty =
            DependencyProperty.Register("ImageSourceRight", typeof(ImageSource), typeof(VideoViewer), new FrameworkPropertyMetadata(DrawFilledRectangle(200, 200), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public ImageSource ImageSourceMiddle
        {
            get { return (ImageSource)GetValue(ImageSourceMiddleProperty); }
            set { SetValue(ImageSourceMiddleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsStepTwoEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceMiddleProperty =
            DependencyProperty.Register("ImageSourceMiddle", typeof(ImageSource), typeof(VideoViewer), new FrameworkPropertyMetadata(DrawFilledRectangle(600, 400), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private static BitmapImage DrawFilledRectangle(int x, int y)
        {
            //Makes a black image appear, good for debugging!
            SysDraw.Bitmap bmp = new SysDraw.Bitmap(x, y);
            using (SysDraw.Graphics graph = SysDraw.Graphics.FromImage(bmp))
            {
                SysDraw.Rectangle ImageSize = new SysDraw.Rectangle(0, 0, x, y);
                graph.FillRectangle(SysDraw.Brushes.Black, ImageSize);
            }
            
            using (MemoryStream memory = new MemoryStream())
            {
                bmp.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

    }
}

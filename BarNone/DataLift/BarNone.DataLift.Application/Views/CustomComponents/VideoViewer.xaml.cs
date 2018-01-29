using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SysDraw = System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BarNone.DataLift.UI.Views.CustomComponents
{
    /// <summary>
    /// Interaction logic for VideoViewer.xaml
    /// </summary>
    public partial class VideoViewer : UserControl
    {
        /// <summary>
        /// Initializes the view
        /// </summary>
        public VideoViewer()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Bindable dependency property for the left image
        /// </summary>
        public ImageSource ImageSourceLeft
        {
            get { return (ImageSource)GetValue(ImageSourceLeftProperty); }
            set { SetValue(ImageSourceLeftProperty, value); }
        }

        /// <summary>
        /// Dependency property for the image source of the left image
        /// </summary>
        public static readonly DependencyProperty ImageSourceLeftProperty =
            DependencyProperty.Register("ImageSourceLeft", typeof(ImageSource), typeof(VideoViewer), new FrameworkPropertyMetadata(GetDefaultCanvas(200,200)));

        /// <summary>
        /// Bindable dependency property for the right image
        /// </summary>
        public ImageSource ImageSourceRight
        {
            get { return (ImageSource)GetValue(ImageSourceRightProperty); }
            set { SetValue(ImageSourceRightProperty, value); }
        }

        /// <summary>
        /// Dependency property for the image source of the right image
        /// </summary>
        public static readonly DependencyProperty ImageSourceRightProperty =
            DependencyProperty.Register("ImageSourceRight", typeof(ImageSource), typeof(VideoViewer), new FrameworkPropertyMetadata(GetDefaultCanvas(200, 200)));


        /// <summary>
        /// Bindable dependency property for the middle image
        /// </summary>
        public ImageSource ImageSourceMiddle
        {
            get { return (ImageSource)GetValue(ImageSourceMiddleProperty); }
            set { SetValue(ImageSourceMiddleProperty, value); }
        }
        
        /// <summary>
        /// Dependency property for the image source of the largest and central image
        /// </summary>
        public static readonly DependencyProperty ImageSourceMiddleProperty =
            DependencyProperty.Register("ImageSourceMiddle", typeof(ImageSource), typeof(VideoViewer), new FrameworkPropertyMetadata(GetDefaultCanvas(600, 400)));
        
        /// <summary>
        /// Creates a default canvas which is x * y pixels in size
        /// </summary>
        /// <param name="x">Width of canvas</param>
        /// <param name="y">Height of canvas</param>
        /// <returns>Default canvas</returns>
        private static BitmapImage GetDefaultCanvas(int x, int y)
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

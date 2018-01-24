using System;
using System.Windows.Media.Imaging;

namespace BarNone.DataLift.UI.Models
{
    public class ColorImageFrame
    {
        public WriteableBitmap Image { get; }
        public TimeSpan Time { get; }

        public ColorImageFrame(WriteableBitmap image, TimeSpan time)
        {
            Time = time;
            Image = image;
        }
    }
}

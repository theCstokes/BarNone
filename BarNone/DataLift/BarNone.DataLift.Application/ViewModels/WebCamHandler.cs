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
using System.Windows.Shapes;
using Accord.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;
using System.Collections.Concurrent;

namespace BarNone.DataLift.UI.ViewModels
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class WebCamHandler
    {
        VideoCaptureDevice webCam;
        public FilterInfoCollection webCamCollection;
        VideoFileWriter writer;



        private static BlockingCollection<Bitmap> _renderQueue;// = new BlockingCollection<Bitmap>();
        private static BlockingCollection<Bitmap> _writeQueue;// = new BlockingCollection<Bitmap>();

        //Task<int> consumer;
        //static BufferBlock<Bitmap> buffer;
        //private void OnLoadEvent(object sender, RoutedEventArgs e)
        //{

        //    //buffer = new BufferBlock<Bitmap>();

        //    // Start the consumer. The Consume method runs asynchronously. 
        //    //consumer = ConsumeAsync(buffer);


        //    webCamCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        //    webCam = new VideoCaptureDevice(webCamCollection[0].MonikerString);
        //    webCam.NewFrame += FrameHandler;

        //    var maxWidth = webCam.VideoCapabilities.Max(c => c.FrameSize.Width);
        //    webCam.VideoResolution = webCam.VideoCapabilities.First(c => c.FrameSize.Width == maxWidth);

        //    Run();
        //}

        public void Start()
        {
            _renderQueue = new BlockingCollection<Bitmap>();
            _writeQueue = new BlockingCollection<Bitmap>();

            webCamCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            webCam = new VideoCaptureDevice(webCamCollection[0].MonikerString);
            webCam.NewFrame += FrameHandler;

            var maxWidth = webCam.VideoCapabilities.Max(c => c.FrameSize.Width);
            webCam.VideoResolution = webCam.VideoCapabilities.First(c => c.FrameSize.Width == maxWidth);

            Run();
        }

        public void Stop()
        {
            webCam.NewFrame -= FrameHandler;
            //Dispatcher.InvokeShutdown();
            webCam.SignalToStop();
            //op.Wait();
            //consumer.Wait();
            writer.Close();
        }

        private async Task Run()
        {
            //Video Writer
            writer = new VideoFileWriter();

            int framesPerSecond = 15;
            int bitRate = ((webCam.VideoResolution.FrameSize.Width *
                webCam.VideoResolution.FrameSize.Height) * framesPerSecond);

            // create new video file
            writer.Open("test.avi",
                webCam.VideoResolution.FrameSize.Width,
                webCam.VideoResolution.FrameSize.Height,
                framesPerSecond,
                VideoCodec.H263P, // This looks the be the best all around that works.
                bitRate);


            //new Thread(() => ConsumeLoop(_cancellationToken.Token)).Start();

            //Task.

            var task = Task.WhenAll(
                Task.Run(() => RenderConsumeLoop()),
                Task.Run(() => WriteConsumeLoop()));

            webCam.Start();

            await task;
        }

        //private void OnCloseEvent(object sender, EventArgs e)
        //{
        //    webCam.NewFrame -= FrameHandler;
        //    //Dispatcher.InvokeShutdown();
        //    webCam.SignalToStop();
        //    op.Wait();
        //    //consumer.Wait();
        //    writer.Close();
        //}

        private object _CameraImageLock = new object();

        int processed = 0, requested = 0;


        Stopwatch sw = new Stopwatch();

        static BitmapImage bi;
        DispatcherOperation op;
        private void FrameHandler(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                sw.Restart();
                _writeQueue.Add(eventArgs.Frame.Clone() as Bitmap);
                //_renderQueue.Add(eventArgs.Frame.Clone() as Bitmap);
                Console.WriteLine($"TOT TIME: {sw.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
            }
        }

        private void WriteConsumeLoop()
        {
            while (!_writeQueue.IsCompleted)
            {
                var frame = _writeQueue.Take();
                writer.WriteVideoFrame(frame);
                frame.Dispose();
            }
        }

        private void RenderConsumeLoop()
        {
            while (!_renderQueue.IsCompleted)
            {
                //var frame = _renderQueue.Take();
                //MemoryStream ms = new MemoryStream();
                //bi = new BitmapImage
                //{
                //    CacheOption = BitmapCacheOption.OnLoad  //Optimization for caching bmp
                //};


                //frame.Save(ms, ImageFormat.Bmp);
                //requested++;
                //ms.Seek(0, SeekOrigin.Begin);
                //bi.BeginInit();
                //bi.StreamSource = ms;
                //bi.EndInit();
                //bi.Freeze();

                //op = Capture.Dispatcher.BeginInvoke(DispatcherPriority.Send, (Action<BitmapImage>)((obj) =>
                //{
                //    if (bi != null)
                //        Capture.Source = obj;
                //    processed++;

                //}), bi);
                //frame.Dispose();
            }
        }

    }
}




















//using Accord.Video.FFMPEG;
//using AForge.Video;
//using AForge.Video.DirectShow;
//using System;
//using System.Collections.Concurrent;
//using System.Diagnostics;
//using System.Drawing;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Media.Imaging;

//namespace BarNone.DataLift.UI.ViewModels
//{
//    public class WebCamHandler : IDisposable
//    {

//        private VideoCaptureDevice _webCam;
//        private VideoFileWriter _videoWriter;

//        /// <summary>
//        /// Queue to handle rendering recieved Webcam frames
//        /// </summary>
//        private static BlockingCollection<Bitmap> _renderQueue;
//        /// <summary>
//        /// Queue to handle outputting recieved webcam frames
//        /// </summary>
//        private static BlockingCollection<Bitmap> _writeQueue;

//        //Tasks
//        Task _renderConsumeTask;
//        Task _writeConsumeTask;

//        public WebCamHandler()
//        {
//            //Setup the webcam objects
//            var webCamCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
//            _webCam = new VideoCaptureDevice(webCamCollection[0].MonikerString);
//            _webCam.NewFrame += FrameHandler;

//            //Video Wriper params
//            var maxWidth = _webCam.VideoCapabilities.Max(c => c.FrameSize.Width);
//            maxWidth = maxWidth > 1920 ? 1920 : maxWidth;

//            _webCam.VideoResolution = _webCam.VideoCapabilities.First(c => c.FrameSize.Width == maxWidth);            
//        }

//        public void StartCapturing()
//        {

//            _renderQueue = new BlockingCollection<Bitmap>();
//            _renderConsumeTask = Task.Run(() => RenderConsumeLoop());
//            _webCam.Start();
//        }

//        public void StartRecording(string fileName)
//        {
//            int framesPerSecond = 15;
//            int bitRate = ((_webCam.VideoResolution.FrameSize.Width *
//                _webCam.VideoResolution.FrameSize.Height) * framesPerSecond);

//            //TODO use max height and width of the camera as in Capture
//            _videoWriter = new VideoFileWriter
//            {
//                Width = _webCam.VideoResolution.FrameSize.Width,
//                Height = _webCam.VideoResolution.FrameSize.Height,
//                VideoCodec = VideoCodec.H264,
//                FrameRate = framesPerSecond,
//                BitRate = bitRate
//                //PixelFormat = AVPixelFormat.FormatRgb24bpp
//            };

//            //Ensure we are capturing
//            if (!_webCam.IsRunning)
//                StartCapturing();

//            if (_videoWriter.IsOpen)
//                throw new Exception("Video Writer is already active, check order of events! Ensure WebCamHander.Stop() is called.");

//            _writeQueue = new BlockingCollection<Bitmap>();

//            // Create new video file
//            _videoWriter.Open("New.avi", "avi");

//            _writeConsumeTask = Task.Run(() => WriteConsumeLoop());
//        }

//        public async Task Stop()
//        {
//            if (_webCam.IsRunning)
//                await StopRecording();

//            //Stop the webcam
//            _webCam.SignalToStop();

//            //Prevent new frames
//            _renderQueue.CompleteAdding();

//            //Wait for the writes to complete
//            await _renderConsumeTask;

//            //Close the writer
//            _videoWriter.Close();

//            _renderQueue.Dispose();

//        }

//        public async Task StopRecording()
//        {
//            _webCam.Stop();

//            _writeQueue.CompleteAdding();

//            //if (_webCam.IsRunning)

//            await _writeConsumeTask;

//            _writeQueue.Dispose();

//            //_videoWriter.Flush();
//            _videoWriter.Close();
//            //_videoWriter.Dispose();
//        }

//        private void FrameHandler(object sender, NewFrameEventArgs eventArgs)
//        {
//            try
//            {
//                if (!_renderQueue.IsAddingCompleted)
//                    _renderQueue.Add(eventArgs.Frame.Clone() as Bitmap);

//                if (!_writeQueue.IsAddingCompleted)
//                    _writeQueue.Add(eventArgs.Frame.Clone() as Bitmap);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//        }

//        int fi = 0;
//        private void WriteConsumeLoop()
//        {
//            while (!_writeQueue.IsCompleted)
//            {
//                if (_writeQueue.Count == 0) continue;
//                var frame = _writeQueue.Take();
//                Debug.WriteLine("Write");
//                frame.Save($"f{++fi}.bmp");
//                _videoWriter?.WriteVideoFrame(frame);
//                frame.Dispose();
//            }
//            Debug.WriteLine("Done");
//        }

//        static BitmapImage bi;
//        //DispatcherOperation op;
//        private void RenderConsumeLoop()
//        {
//            while (!_renderQueue.IsCompleted)
//            {
//                var frame = _renderQueue.Take();

//                //TODO Figure below out

//                //MemoryStream ms = new MemoryStream();
//                //bi = new BitmapImage
//                //{
//                //    CacheOption = BitmapCacheOption.OnLoad  //Optimization for caching bmp
//                //};

//                //frame.Save(ms, ImageFormat.Bmp);
//                //ms.Seek(0, SeekOrigin.Begin);
//                //bi.BeginInit();
//                //bi.StreamSource = ms;
//                //bi.EndInit();
//                //bi.Freeze();

//                //op = Capture.Dispatcher.BeginInvoke(DispatcherPriority.Send, (Action<BitmapImage>)((obj) =>
//                //{
//                //    if (bi != null)
//                //        Capture.Source = obj;
//                //    processed++;

//                ////}), bi);
//                frame.Dispose();
//            }
//        }


//        public void Dispose()
//        {
//            Stop();
//            _webCam.NewFrame -= FrameHandler;
//            _videoWriter.Dispose();
//        }
//    }
//}

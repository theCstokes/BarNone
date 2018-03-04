using Accord.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BarNone.DataLift.UI.ViewModels
{
    public class WebCamHandler : IDisposable
    {

        private VideoCaptureDevice _webCam;
        private VideoFileWriter _videoWriter;

        /// <summary>
        /// Queue to handle rendering recieved Webcam frames
        /// </summary>
        private static BlockingCollection<Bitmap> _renderQueue;
        /// <summary>
        /// Queue to handle outputting recieved webcam frames
        /// </summary>
        private static BlockingCollection<Bitmap> _writeQueue;


        //Tasks
        Task _readConsumeTask;
        Task _writeConsumeTask;


        public WebCamHandler()
        {
            //Setup the webcam objects
            var webCamCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            _webCam = new VideoCaptureDevice(webCamCollection[0].MonikerString);
            _webCam.NewFrame += FrameHandler;

            //Video Wriper params
            var maxWidth = _webCam.VideoCapabilities.Max(c => c.FrameSize.Width);
            _webCam.VideoResolution = _webCam.VideoCapabilities.First(c => c.FrameSize.Width == maxWidth);            
            int framesPerSecond = 15;
            int bitRate = ((_webCam.VideoResolution.FrameSize.Width *
                _webCam.VideoResolution.FrameSize.Height) * framesPerSecond);

            //TODO use max height and width of the camera as in Capture
            _videoWriter = new VideoFileWriter
            {
                Width = 1920,
                Height = 1080,
                VideoCodec = VideoCodec.H263P,
                FrameRate = framesPerSecond,
                BitRate = bitRate
            };
        }

        public void StartCapturing()
        {

            _renderQueue = new BlockingCollection<Bitmap>();
            _readConsumeTask = Task.Run(() => RenderConsumeLoop());
            _webCam.Start();
        }

        public void StartRecording(string fileName)
        {
            //Ensure we are capturing
            if (!_webCam.IsRunning)
                StartCapturing();

            if (_videoWriter.IsOpen)
                throw new Exception("Video Writer is already active, check order of events! Ensure WebCamHander.Stop() is called.");

            _writeQueue = new BlockingCollection<Bitmap>();

            // Create new video file
            _videoWriter.Open(fileName);

            _writeConsumeTask = Task.Run(() => WriteConsumeLoop());

        }

        public async void Stop()
        {
            if (_webCam.IsRunning)
                StopRecording();

            //Stop the webcam
            _webCam.SignalToStop();

            //Prevent new frames
            _renderQueue.CompleteAdding();

            //Wait for the writes to complete
            await _readConsumeTask;

            //Close the writer
            _videoWriter.Close();

            _renderQueue.Dispose();

        }

        public async void StopRecording()
        {
            _writeQueue.CompleteAdding();

            if (_webCam.IsRunning)
                await _writeConsumeTask;

            _writeQueue.Dispose();

            _videoWriter.Close();
        }

        private void FrameHandler(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                if (!_renderQueue.IsAddingCompleted)
                    _renderQueue.Add(eventArgs.Frame.Clone() as Bitmap);

                if (!_writeQueue.IsAddingCompleted)
                    _writeQueue.Add(eventArgs.Frame.Clone() as Bitmap);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void WriteConsumeLoop()
        {
            while (!_writeQueue.IsCompleted)
            {
                var frame = _writeQueue.Take();
                _videoWriter?.WriteVideoFrame(frame);
                frame.Dispose();
            }
        }

        static BitmapImage bi;
        //DispatcherOperation op;
        private void RenderConsumeLoop()
        {
            while (!_renderQueue.IsCompleted)
            {
                var frame = _renderQueue.Take();
                
                //TODO Figure below out

                //MemoryStream ms = new MemoryStream();
                //bi = new BitmapImage
                //{
                //    CacheOption = BitmapCacheOption.OnLoad  //Optimization for caching bmp
                //};

                //frame.Save(ms, ImageFormat.Bmp);
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

                ////}), bi);
                frame.Dispose();
            }
        }


        public void Dispose()
        {
            Stop();
            _webCam.NewFrame -= FrameHandler;
            _videoWriter.Dispose();
        }
    }
}

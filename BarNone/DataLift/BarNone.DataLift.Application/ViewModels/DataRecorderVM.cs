﻿using Accord.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using BarNone.DataLift.APIRequest;
using BarNone.DataLift.DataConverters.KinectToDM;
using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Drawing;
using BarNone.DataLift.UI.Nav;
using BarNone.DataLift.UI.ViewModels.Common;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.Flex;
using BarNone.Shared.DomainModel;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace BarNone.DataLift.UI.ViewModels
{
    public class DataRecorderVM : ViewModelBase
    {

        #region Bound Properties
        /// <summary>
        /// Vm holding all information shared between video data dependent viewmodels
        /// </summary>
        private CurrentLiftDataVM _currentLiftData = CurrentLiftDataVMSingleton.GetInstance();
        /// <summary>
        /// Bindable data context holding all information shared between video data dependent viewmodels
        /// </summary>
        public CurrentLiftDataVM CurrentLiftData
        {
            get => _currentLiftData;
        }

        #endregion

        #region Color Drawing Details
        /// <summary>
        /// Bitmap to display the color video
        /// </summary>
        private WriteableBitmap colorBitmap = null;

        private WebCamHandler _webCamHandler;
        #endregion

        #region UI Components
        /// <summary>
        /// Drawing group for body rendering front profile output
        /// </summary>
        private DrawingGroup FrontProfileDrawingGroup;

        /// <summary>
        /// Drawing group for body rendering side profile output
        /// </summary>
        private DrawingGroup SideProfileDrawingGroup;

        /// <summary>
        /// Front profile drawing image that we will display
        /// </summary>
        private DrawingImage imageSourceFront;

        /// <summary>
        /// Side pofile drawing image that we will display
        /// </summary>
        private DrawingImage imageSourceSide;

        /// <summary>
        /// Width of display (depth space)
        /// </summary>
        private int displayWidth;

        /// <summary>
        /// Height of display (depth space)
        /// </summary>
        private int displayHeight;

        /// <summary>
        /// Array for the bodies
        /// </summary>
        private IList<Body> Bodies { get; set; }

        /// <summary>
        /// Gets the bitmap to display the front profile
        /// </summary>
        public ImageSource ImageSourceFront { get => imageSourceFront; }

        /// <summary>
        /// Gets the bitmap to display the side profile
        /// </summary>
        public ImageSource ImageSourceSide { get => imageSourceSide; }

        /// <summary>
        /// Gets the bitmap to display the color image
        /// </summary>
        public ImageSource ImageSourceColor { get => cries; }

        private BitmapSource cries;

        #endregion

        #region Kinect Properties
        /// <summary>
        /// Event handler for recieveing multiple frame types (color and depth) from the kinect
        /// </summary>
        private MultiSourceFrameReader Reader;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Coordinate mapper to map one type of point to another
        /// </summary>
        private CoordinateMapper coordinateMapper = null;

        #endregion

        #region Reccording Data Variables
        /// <summary>
        /// New Recordings refresh the locally stored data
        /// TODO REMOVE = true and actually control
        /// </summary>
        public bool IsNewRecording { get; private set; } = true;

        /// <summary>
        /// Control flow variable for if the system is recording data or not
        /// </summary>
        public bool IsRecording { get; private set; } = false;

        /// <summary>
        /// The current lift session, TODO make deprecated
        /// </summary>
        private BodyData CurrentRecordingBodyData { get; set; }

        /// <summary>
        /// The current lifter and the auth they hold, TODO move to common
        /// </summary>
        private UserDTO CurrentUser { get; set; }

        /// <summary>
        /// Is the user in the middle of a lift that is recorded.
        /// </summary>
        private bool isCurrentlyRecording = false;

        #endregion

        #region User Control events
        internal override void Loaded()
        {
            IsRecording = false;
            isCurrentlyRecording = false;
            CurrentLiftData.CurrentRecordedBodyData.Clear();
        }

        internal override void Closed()
        {
            IsRecording = false;
            isCurrentlyRecording = false;
        }

        #endregion

        #region Frame Arrival
        /// <summary>
        /// Handles the body frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_FrameArrived(BodyFrame frame)
        {
            bool dataReceived = false;

            if (frame != null)
            {
                if (Bodies == null)
                {
                    Bodies = new Body[frame.BodyFrameSource.BodyCount];
                }

                // The first time GetAndRefreshBodyData is called, Kinect will allocate each Body in the array.
                // As long as those body objects are not disposed and not set to null in the array,
                // those body objects will be re-used.
                frame.GetAndRefreshBodyData(Bodies);
                dataReceived = true;
            }

            if (dataReceived)
            {
                // Gets the closest body to the kinect sensor
                var body = GetPrimaryBody(Bodies);
                //Convert the frame to a more usable form
                var dataFrame = frame.KinectBdfToDmBdf(body);
                dataFrame.TimeOfFrame = TimeSpan.FromMilliseconds(lastRecievedBodyFrameMs);

                if (isCurrentlyRecording)
                    CurrentLiftData.CurrentRecordedBodyData.Add(dataFrame);

                //Update The Side And Front Views
                KinectToImage.DrawFrameSideView(dataFrame, SideProfileDrawingGroup, displayHeight, displayWidth);
                KinectToImage.DrawFrameFrontView(dataFrame, FrontProfileDrawingGroup, displayHeight, displayWidth);
            }
        }

        /// <summary>
        /// Sets the body we draw on DL and send to the Rack.  The body we draw is the one whos spine base is closest to the Kinect.
        /// </summary>
        /// <param name="bodies_in">A list of all bodies being tracked by the kinect.</param>
        /// <returns></returns>
        private static Body GetPrimaryBody(IList<Body> bodies_in)
        {
            Body primaryBody = null;

            // for all the bodies the Kinect is currently tracking.
            foreach (Body body in bodies_in)
            {
                /// If the position of spinebase (the enum value 0) is not 0.
                /// Because for some godforsaken reason MS initiliazes position data to (0,0,0) 
                //TODO fix using MicrosoftKinect, best to put all kinect stuff in a KinectHandler
                if (body.Joints[Microsoft.Kinect.JointType.SpineBase].Position.Z != 0)
                {
                    // If there is currently no body compare against then it is the one use by default.
                    if (primaryBody == null)
                    {
                        primaryBody = body;
                    }
                    else if (body.Joints[Microsoft.Kinect.JointType.SpineBase].Position.Z < primaryBody.Joints[Microsoft.Kinect.JointType.SpineBase].Position.Z)
                    {
                        // If there are mutiple then we use the one whos spine base is closest to the kinect.
                        primaryBody = body;
                    }
                }
            }

            // We cannot return a null body, so we just assign the first body as the one we return.
            if (primaryBody == null)
            {
                primaryBody = bodies_in[0];
            }

            return primaryBody;
        }
        #endregion

        #region TO REMOVE Draw Color
        /// <summary>
        /// Handles the color frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_ColorFrameArrived(ColorFrame frame)
        {
            if (frame != null)
            {
                FrameDescription colorFrameDescription = frame.FrameDescription;

                using (KinectBuffer colorBuffer = frame.LockRawImageBuffer())
                {
                    colorBitmap.Lock();

                    // verify data and write the new color frame data to the display bitmap
                    if ((colorFrameDescription.Width == colorBitmap.PixelWidth) && (colorFrameDescription.Height == colorBitmap.PixelHeight))
                    {
                        frame.CopyConvertedFrameDataToIntPtr(
                            colorBitmap.BackBuffer,
                            (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                            ColorImageFormat.Bgra);

                        colorBitmap.AddDirtyRect(new Int32Rect(0, 0, colorBitmap.PixelWidth, colorBitmap.PixelHeight));
                    }

                    colorBitmap.Unlock();
                    if (isCurrentlyRecording)
                    {
                        var toSave = new WriteableBitmap(colorBitmap);
                        CurrentLiftData.CurrentRecordedColorData.Add(new Models.ColorImageFrame(toSave, TimeSpan.FromMilliseconds(lastRecievedColorFrameMs)));
                    }
                }
            }
        }
        #endregion

        #region Reset Option
        /// <summary>
        /// Deprecated, used to debug via resetting the kinect
        /// </summary>
        public RelayCommand _TestStrategy1 { get; private set; }

        /// <summary>
        /// Deprecated, binding command to reset the kinect hardware
        /// </summary>
        public ICommand TestStrategy1
        {
            get
            {
                if (_TestStrategy1 == null)
                {
                    _TestStrategy1 = new RelayCommand(action => TestStrategy1_ResetKinectSensor());
                }
                return _TestStrategy1;
            }
        }

        /// <summary>
        /// Resets the kinect hardware, debug tool
        /// </summary>
        private void TestStrategy1_ResetKinectSensor()
        {
            kinectSensor.Close();
            kinectSensor.Open();
        }
        #endregion

        #region Start and Finish Recording
        /// <summary>
        /// Reference to the start start recording field
        /// </summary>
        private RelayCommand _StartRecording;
        /// <summary>
        /// Bindable command to start recortding data
        /// </summary>
        public ICommand StartRecording
        {
            get
            {
                if (_StartRecording == null)
                {
                    _StartRecording = new RelayCommand(action => StartNewRecording(), pred => !isCurrentlyRecording);
                }
                return _StartRecording;
            }
        }

        /// <summary>
        /// Resets the current recording body when the user wants to begin a lift
        /// </summary>
        private void StartNewRecording()
        {
            //TODO move this to the VM
            CurrentLiftData.CurrentRecordedBodyData.Clear();
            CurrentLiftData.CurrentRecordedColorData.Clear();
            isCurrentlyRecording = true;

            GlobalFrameTimer.Restart();

            _webCamHandler.StartRecording("Test.avi");
        }

        /// <summary>
        /// Bindable command to log the current use out, deprecated
        /// </summary>
        public ICommand LogoutCommand { get; } = new RelayCommand(action => PageManager.SwitchPage(UIPages.LoginView));

        /// <summary>
        /// Reference to the start end recording field
        /// </summary>
        private RelayCommand _EndRecording;
        /// <summary>
        /// Bindable command to stop recortding data
        /// </summary>
        public ICommand EndRecording
        {
            get
            {
                if (_EndRecording == null)
                {
                    _EndRecording = new RelayCommand(async action => await EndCurrentRecording(), pred => isCurrentlyRecording);
                }
                return _EndRecording;
            }
        }

        /// <summary>
        /// Posts the recorded lift to the server when the user denotes a lift has been completed.
        /// </summary>
        /// <returns></returns>
        private async Task EndCurrentRecording()
        {
            GlobalFrameTimer.Stop();
            isCurrentlyRecording = false;
            IsRecording = false;

            //TODO add Spinner here
            _webCamHandler.StopRecording();
            
            //videoSource.SignalToStop();
            //videoSource.WaitForStop();
            //videoSource.Stop();

            //waitFor.Wait();
            //writer.Close();

            try
            {
                CurrentLiftData.NormalizeTimes();
                //TODO Notify To Switch Page
            }
            catch (ArgumentException)
            {
                //Argument exception if the data cannot be normalized for processing
            }
            return;
            //TODO CLEAN!
            //TempAddCurrentLift();

            var toSend = new LiftDTO
            {
                ParentID = 1,
                Name = "FIX_NAME_SENDS" + Guid.NewGuid(),
                Details = new LiftDetailDTO()
                {
                    BodyData = new BodyDataDTO()
                }

            };

            var bodyDto = Converters.NewConvertion().BodyData.CreateDTO(KinectDepthFrameConverter.KinectBodyDataToDmBodyData(CurrentLiftData.CurrentRecordedBodyData));
            toSend.Details.BodyData = bodyDto;

            var temp = await DataManager.Flex.Post(new FlexDTO
            {
                Entities = new List<FlexEntityDTO>
                {
                    new FlexEntityDTO
                    {
                        Resource = "LIFT",
                        Entity = toSend
                    }
                }
            });

        }

        #endregion
        
        #region Constructor(s) and Destructor
        ~DataRecorderVM()
        {
            if (Reader != null)
            {
                Reader?.Dispose();
                Reader = null;
            }

            if (kinectSensor != null)
            {
                //kinectSensor?.Close();
                kinectSensor = null;
            }

            _webCamHandler.Dispose();
        }

        /// <summary>
        /// Initializes the kinect and prepares the bindings to the DataRecorderScreen
        /// </summary>
        public DataRecorderVM()
        {
            // one sensor is currently supported
            kinectSensor = KinectSensor.GetDefault();

            // get the coordinate mapper
            coordinateMapper = kinectSensor.CoordinateMapper;
            
            //Reader = kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color |
            //                                     FrameSourceTypes.Body);
            Reader = kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body);

            Reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;

            FrameDescription frameDescription = kinectSensor.DepthFrameSource.FrameDescription;
            // get size of joint space
            displayWidth = frameDescription.Width;
            displayHeight = frameDescription.Height;

            FrameDescription colorFrameDescription = kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);
            // create the bitmap to display
            colorBitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 72.0, 72.0, PixelFormats.Bgr32, null);

            // open the sensor
            kinectSensor.Open();

            // Create the drawing group we'll use for drawing
            FrontProfileDrawingGroup = new DrawingGroup();

            // Create the drawing group we'll use for drawing
            SideProfileDrawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            imageSourceFront = new DrawingImage(FrontProfileDrawingGroup);

            // Create an image source that we can use in our image control
            imageSourceSide = new DrawingImage(SideProfileDrawingGroup);

            //InitializeVideoRecorder();
            _webCamHandler = new WebCamHandler();
            _webCamHandler.StartCapturing();
        }


        //DispatcherOperation waitFor;
        //VideoFileWriter writer;
        //VideoCaptureDevice videoSource;
        //object VideoLock = new object();

        //private void InitializeVideoRecorder()
        //{
        //    FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

        //    if (videoDevices.Count == 0)
        //    {
        //        throw new Exception();
        //    }

        //    videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

        //    var maxWidth = videoSource.VideoCapabilities.Max(c => c.FrameSize.Width);
        //    videoSource.VideoResolution = videoSource.VideoCapabilities.First(c => c.FrameSize.Width == maxWidth);

        //    writer = new VideoFileWriter();

        //    int framesPerSecond = 15;
        //    int bitRate = ((videoSource.VideoResolution.FrameSize.Width *
        //        videoSource.VideoResolution.FrameSize.Height) * framesPerSecond);

        //    // create new video file
        //    writer.Open("test.avi",
        //        1920,
        //        1080,
        //        framesPerSecond,
        //        VideoCodec.H263P, // This looks the be the best all around that works.
        //        bitRate);

        //    var i = 0;
        //    var s = Stopwatch.StartNew();


        //    videoSource.NewFrame += (object sender, NewFrameEventArgs eventArgs) =>
        //    {
        //        i++;
        //        s.Reset();


        //        var frame = eventArgs.Frame;

        //        waitFor = Application.Current.Dispatcher.BeginInvoke((Action)delegate
        //        {

        //            try
        //            {
        //                BitmapImage bi;
        //                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
        //                {
        //                    bi = new BitmapImage();
        //                    bi.BeginInit();
        //                    var ms = new System.IO.MemoryStream();
        //                    bitmap.Save(ms, ImageFormat.Bmp);
        //                    bi.StreamSource = ms;
        //                    bi.CacheOption = BitmapCacheOption.OnLoad;
        //                    bi.EndInit();
        //                }
        //                bi.Freeze();
        //                Application.Current.Dispatcher.Invoke((Action)delegate
        //                {
        //                    cries = bi;
        //                    if (isCurrentlyRecording)
        //                        writer.WriteVideoFrame(frame, new Rectangle(0, 0, 1920, 1080));
        //                });

        //            }
        //            catch (Exception ex)
        //            {
        //                //catch your error here
        //            }
        //        });



        //        //Bitmap clone = eventArgs.Frame.Clone(
        //        //    new Rectangle(0, 0, eventArgs.Frame.Width, eventArgs.Frame.Height), 
        //        //    PixelFormat.Format24bppRgb);

        //        Console.WriteLine(s.ElapsedMilliseconds);
        //        waitFor.Wait();
        //    };

        //    videoSource.Start();
        //}


        #endregion

        private Stopwatch GlobalFrameTimer = new Stopwatch();
        long lastRecievedBodyFrameMs = 0;
        long lastRecievedColorFrameMs = 0;

        /// <summary>
        /// Event fired when the kinect sends any data frame type, depth and color are monitored
        /// </summary>
        /// <param name="sender">Kinect source</param>
        /// <param name="e">Frame type</param>
        private void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            // Get a reference to the multi-frame
            var reference = e.FrameReference.AcquireFrame();

            //// Open color frame
            //using (var frame = reference.ColorFrameReference.AcquireFrame())
            //{
            //    if (frame != null)
            //    {
            //        lastRecievedColorFrameMs = GlobalFrameTimer.ElapsedMilliseconds;
            //        Reader_ColorFrameArrived(frame);
            //    }
            //}

            // Open depth frame
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    lastRecievedBodyFrameMs = GlobalFrameTimer.ElapsedMilliseconds;
                    Reader_FrameArrived(frame);
                }
            }
        }

        //TODO IMPLEMENT AND TEST
        /// <summary>
        /// Shows the status of the kinect on the record screen
        /// </summary>
        public string KinectConnected;

        /// <summary>
        /// Handles the event which the sensor becomes unavailable (E.g. paused, closed, unplugged).
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            // on failure, set the status text
            KinectConnected = kinectSensor.IsAvailable ? "Connected"
                                                            : "The Gym Rats Unplugged the Camera!";
        }
    }
}

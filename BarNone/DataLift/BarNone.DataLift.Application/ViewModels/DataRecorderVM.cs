using BarNone.DataLift.APIRequest;
using BarNone.DataLift.DataConverters;
using BarNone.DataLift.DataModel.KinectData;
using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Drawing;
using BarNone.DataLift.UI.Nav;
using BarNone.DataLift.UI.ViewModels.Common;
using BarNone.Shared.DataTransfer;
using Microsoft.Kinect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BarNone.Shared.DataTransfer.Flex;
using System.Security.AccessControl;

namespace BarNone.DataLift.UI.ViewModels
{
    public class DataRecorderVM : ViewModelBase
    {
        #region Bound Properties
        //TODO Make this deprecated
        private ObservableCollection<LiftDTO> _allLiftData = new ObservableCollection<LiftDTO>();
        public ObservableCollection<LiftDTO> AllLiftData
        {
            get { return _allLiftData; }
            set
            {
                _allLiftData = AllLiftData;
                OnPropertyChanged(new PropertyChangedEventArgs("allLiftData"));
            }
        }

        private CurrentLiftDataVM _currentLiftData = CurrentLiftDataVMSingleton.GetInstance();
        public CurrentLiftDataVM CurrentLiftData
        {
            get => _currentLiftData;
        } 

        #endregion

        #region Color Drawing Details
        /// <summary>
        /// Bitmap to display
        /// </summary>
        private WriteableBitmap colorBitmap = null;

        #endregion

        #region UI Components
        /// <summary>
        /// Drawing group for body rendering output
        /// </summary>
        private DrawingGroup FrontProfileDrawingGroup;

        /// <summary>
        /// Drawing group for body rendering output
        /// </summary>
        private DrawingGroup SideProfileDrawingGroup;

        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSourceFront;

        /// <summary>
        /// Drawing image that we will display
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
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSourceFront { get => imageSourceFront; }

        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSourceSide { get => imageSourceSide; }

        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSourceColor { get => colorBitmap; }

        #endregion

        #region Kinect Properties
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
        /// </summary>
        public bool IsNewRecording { get; private set; } = true; //TODO REMOVE = true and actually control
        public bool IsRecording { get; private set; } = false;
        private BodyData CurrentRecordingBodyData { get; set; }

        private UserDTO CurrentUser { get; set; }

        /// <summary>
        /// Is the user in the middle of a lift that is recorded.
        /// </summary>
        private bool isCurrentlyRecording = false;

        /// <summary>
        /// The last state of the users hand.  To track a change in hand state.
        /// </summary>
        private HandState prevHandState;

        /// <summary>
        /// All data that will be sent to the Rack.
        /// </summary>
        //private IList<BodyData> _allLiftData;

        #endregion

        #region User Control events
        internal override void Loaded()
        {
            IsRecording = false;

            isCurrentlyRecording = false;

            prevHandState = 0;

            AllLiftData.Clear();
        }

        internal override void Closed()
        {
            IsRecording = false;

            isCurrentlyRecording = false;

            prevHandState = 0;

            AllLiftData.Clear();
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

                // If the right hand goes from some hand position (other than open) to open.
                /*if ((body.HandRightState == HandState.Open) && (prevHandState != HandState.Open))
                {
                    // If the user is in the middle of a lift and has indicated it is now finished.
                    if(isCurrentlyRecording)
                    {
                        var toSend = new LiftDTO()
                        {
                            ParentID = 1,
                            Name = String.Format("{0}_{1}_{2}_New_Lift_{3}", CurrentRecordingBodyData.RecordDate.Year, CurrentRecordingBodyData.RecordDate.Month, CurrentRecordingBodyData.RecordDate.Day, (allLiftData.Count + 1)),
                            Details = new LiftDetailDTO()
                            {
                                BodyData = Converters.Convert.BodyData.CreateDTO(CurrentRecordingBodyData)
                            }

                        };
                
                        // Add the lift to the list of all lifts. 
                        allLiftData.Add(toSend);

                        // Set is currently recording to false
                        isCurrentlyRecording = false;
                    }
                    // Else means that they are indicating the beginning of a lift.
                    else
                    {
                        //  Then replace CurrentRecordingBodyData with a new body data (start a new lift)
                        CurrentRecordingBodyData = new BodyData
                        {
                            DataFrames = new List<BodyDataFrame>(),
                            RecordDate = DateTime.Now
                        };
                        // Set the status of recoring to in progress.
                        isCurrentlyRecording = true;
                    }

                }

                // Save the status of the hand (so that when it is called the following iteration it will be the prev. one).
                prevHandState = body.HandRightState;
                */
                var dataframe = new BodyDataFrame() { TimeOfFrame = DateTime.Now, Joints = body.Joints.ToDictionary(k => k.Key, v => v.Value) };
                if (isCurrentlyRecording)
                    CurrentRecordingBodyData.AddNewFrame(dataframe);

                //Update The Side And Front Views
                KinectToImage.DrawFrameSideView(
                    dataframe
                    .Joints
                    .Select(x => new JointDTO() { JointTypeID = (int)x.Key, X = x.Value.Position.X, Y = x.Value.Position.Y, Z = x.Value.Position.Z, JointTrackingStateTypeID = (int)x.Value.TrackingState }).ToList(),
                    SideProfileDrawingGroup, displayHeight, displayWidth);
                KinectToImage.DrawFrameFrontView(
                    dataframe
                    .Joints
                    .Select(x => new JointDTO() { JointTypeID = (int)x.Key, X = x.Value.Position.X, Y = x.Value.Position.Y, Z = x.Value.Position.Z, JointTrackingStateTypeID = (int)x.Value.TrackingState }).ToList(),
                    FrontProfileDrawingGroup, displayHeight, displayWidth);
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
                if (body.Joints[JointType.SpineBase].Position.Z != 0)
                {
                    // If there is currently no body compare against then it is the one use by default.
                    if (primaryBody == null)
                    {
                        primaryBody = body;
                    }
                    else if (body.Joints[JointType.SpineBase].Position.Z < primaryBody.Joints[JointType.SpineBase].Position.Z)
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

        #region Draw Color
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
                }
            }

        }
        #endregion

        #region Reset Option
        public RelayCommand _TestStrategy1 { get; private set; }


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

        private void TestStrategy1_ResetKinectSensor()
        {
            kinectSensor.Close();
            kinectSensor.Open();
        }
        #endregion

        #region Start and Finish Recording

        public RelayCommand _StartRecording { get; private set; }
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
            if (isCurrentlyRecording)
                TempAddCurrentLift();

            CurrentRecordingBodyData = new BodyData
            {
                DataFrames = new List<BodyDataFrame>(),
                RecordDate = DateTime.Now

            };
            isCurrentlyRecording = true;
        }

        public ICommand LogoutCommand { get; } = new RelayCommand(action => PageManager.SwitchPage(UIPages.LoginView));

        public RelayCommand _EndRecording { get; private set; }
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

            isCurrentlyRecording = false;
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


            IsRecording = false;


            var bodyDto = Converters.Convert.BodyData.CreateDTO(CurrentRecordingBodyData);
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

            //System.Diagnostics.Debug.WriteLine("The lift was sent to the server {0}", temp.ToString());
        }

        private void TempAddCurrentLift()
        {
            var toSend = new LiftDTO()
            {
                ParentID = 1,
                Name = String.Format("{0}_{1}_{2}_New_Lift_{3}", CurrentRecordingBodyData.RecordDate.Year, CurrentRecordingBodyData.RecordDate.Month, CurrentRecordingBodyData.RecordDate.Day, (AllLiftData.Count + 1)),
                Details = new LiftDetailDTO()
                {
                    BodyData = Converters.Convert.BodyData.CreateDTO(CurrentRecordingBodyData)
                }
            };

            // Add the lift to the list of all lifts. 
            AllLiftData.Add(toSend);
        }

        #endregion

        public string KinectConnected;

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
        }

        public DataRecorderVM()
        {
            Task.Run(() => SetUser());
            
            // one sensor is currently supported
            kinectSensor = KinectSensor.GetDefault();

            // get the coordinate mapper
            coordinateMapper = kinectSensor.CoordinateMapper;

            Reader = kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color |
                                                 FrameSourceTypes.Body);
            Reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;

            FrameDescription frameDescription = kinectSensor.DepthFrameSource.FrameDescription;
            // get size of joint space
            displayWidth = frameDescription.Width;
            displayHeight = frameDescription.Height;

            FrameDescription colorFrameDescription = kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);
            // create the bitmap to display
            colorBitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);

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
        }

        private async Task SetUser()
        {
            var user = await DataManager.Users.GetAll();

            foreach (UserDTO singleUser in user)
            {
                if (LoginScreenVM._username == singleUser.UserName)
                    CurrentUser = singleUser;
            }
        }

        private async Task GetAllLifts()
        {
            var lifts = await DataManager.Lifts.GetAll();

            int currentID = lifts.Count();
        }

        private void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            // Get a reference to the multi-frame
            var reference = e.FrameReference.AcquireFrame();

            // Open color frame
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    Reader_ColorFrameArrived(frame);
                }
            }

            // Open depth frame
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {

                    Reader_FrameArrived(frame);
                }
            }
        }
        
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

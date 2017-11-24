using BarNone.DataLift.DataModel.KinectData;
using BarNone.DataLift.UI.Commands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BarNone.DataLift.APIRequest;
using BarNone.Shared.DataTransfer;
using BarNone.DataLift.UI.Nav;
using System.ComponentModel;
using BarNone.DataLift.DataConverters;

namespace BarNone.DataLift.UI.ViewModels
{
    public class DataRecorderVM : ViewModelBase
    {
        #region Bound Properties
        private string _LiftName = "";
        public string LiftName
        {
            get => _LiftName;
            set
            {
                if (_LiftName != value)
                {
                    _LiftName = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LiftName"));
                }
            }
        }
        #endregion

        #region Private Properties
        #region Brushes
        /// <summary>
        /// Brush used for drawing hands that are currently tracked as closed
        /// </summary>
        private readonly Brush handClosedBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));

        /// <summary>
        /// Brush used for drawing hands that are currently tracked as opened
        /// </summary>
        private readonly Brush handOpenBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));

        /// <summary>
        /// Brush used for drawing hands that are currently tracked as in lasso (pointer) position
        /// </summary>
        private readonly Brush handLassoBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));

        /// <summary>
        /// Brush used for drawing joints that are currently tracked
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// Brush used for drawing joints that are currently inferred
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// Pen used for drawing bones that are currently inferred
        /// </summary>        
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

        #endregion

        #region Skeleton Drawing Details
        /// <summary>
        /// Radius of drawn hand circles
        /// </summary>
        private const double HandSize = 30;

        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 3;

        /// <summary>
        /// Thickness of clip edge rectangles
        /// </summary>
        private const double ClipBoundsThickness = 10;

        /// <summary>
        /// Constant for clamping Z values of camera space points from being negative
        /// </summary>
        private const float InferredZPositionClamp = 0.1f;
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
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Coordinate mapper to map one type of point to another
        /// </summary>
        private CoordinateMapper coordinateMapper = null;

        /// <summary>
        /// Width of display (depth space)
        /// </summary>
        private int displayWidth;

        /// <summary>
        /// Height of display (depth space)
        /// </summary>
        private int displayHeight;

        /// <summary>
        /// Body Color for Body[0]
        /// </summary>
        private Pen bodyColor = new Pen(Brushes.Violet, 6);

        /// <summary>
        /// Array for the bodies
        /// </summary>
        private IList<Body> Bodies { get; set; }

        /// <summary>
        /// Current status text to display
        /// </summary>
        private string statusText = null;


        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSourceFront
        {
            get
            {
                return imageSourceFront;
            }
        }

        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSourceSide
        {
            get
            {
                return imageSourceSide;
            }
        }

        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSourceColor
        {
            get
            {
                return colorBitmap;
            }
        }

        #endregion

        #region Frame Reader
        MultiSourceFrameReader Reader;

        #endregion



        #region Reccording Data Variables
        /// <summary>
        /// New Recordings refresh the locally stored data
        /// </summary>
        public bool IsNewRecording { get; private set; } = true; //TODO REMOVE = true and actually control

        private BodyData CurrentRecordingBodyData { get; set; }

        private UserDTO CurrentUser { get; set; }

        private int currentID;

        #endregion

        #endregion

        #region User Control events
        internal override void Loaded()
        {
            LiftName = "";
        }

        internal override void Closed()
        {
            if (Reader != null)
            {
                Reader.Dispose();
                Reader = null;
            }

            if (kinectSensor != null)
            {
                kinectSensor.Close();
                kinectSensor = null;
            }
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

            //foreach(Body b in Bodies)
            //{
            //    if (WeGotHands())
            //    {
            //        tracked and set a flag to break
            //    }
            //    else
            //    {
            //        b = null;
            //    }
            //}


            if (dataReceived)
            {
                if (IsNewRecording)
                {
                    CurrentRecordingBodyData = new BodyData()
                    {
                        RecordDate = DateTime.Now
                    };
                    IsNewRecording = false;
                }


                //The parent user will be the lifter
                //  They must first block the camera then reverse until they are spotted (for now)

                var body = Bodies[0];
                var dataframe = new BodyDataFrame() { TimeOfFrame = DateTime.Now, Joints = body.Joints.ToDictionary(k => k.Key, v => v.Value) };
                CurrentRecordingBodyData.AddNewFrame(dataframe);
                //Update The Side And Front Views
                UpdateFrontView(dataframe, body);
                UpdateSideView(dataframe, body);

                //for(int i = 0; i < Bodies.Count; i++)
                //{
                //    Bodies[i] = null;
                //}


            }
        }
        #endregion

        #region Draw Skeletons
        private void UpdateFrontView(BodyDataFrame frame, Body lifter)
        {
            using (DrawingContext dc = FrontProfileDrawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, displayWidth, displayHeight));

                DrawClippedEdges(lifter, dc);

                // convert the joint points to depth (display) space
                Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                foreach (JointType jointType in frame.Joints.Keys)
                {
                    // sometimes the depth(Z) of an inferred joint may show as negative
                    // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                    CameraSpacePoint position = frame.Joints[jointType].Position;
                    if (position.Z < 0)
                    {
                        position.Z = InferredZPositionClamp;
                    }

                    DepthSpacePoint depthSpacePoint = coordinateMapper.MapCameraPointToDepthSpace(position);
                    jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
                }

                DrawBody(frame.Joints, jointPoints, dc, bodyColor);

                // prevent drawing outside of our render area
                FrontProfileDrawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, displayWidth, displayHeight));
            }
        }

        private void UpdateSideView(BodyDataFrame frame, Body lifter)
        {
            using (DrawingContext dc = SideProfileDrawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, displayWidth, displayHeight));

                DrawClippedEdges(lifter, dc);

                // convert the joint points to depth (display) space
                Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                foreach (JointType jointType in frame.Joints.Keys)
                {
                    // sometimes the depth(Z) of an inferred joint may show as negative
                    // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                    CameraSpacePoint position = frame.Joints[jointType].Position;
                    if (position.Z < 0)
                    {
                        position.Z = InferredZPositionClamp;
                    }

                    DepthSpacePoint depthSpacePoint = coordinateMapper.MapCameraPointToDepthSpace(position);
                    jointPoints[jointType] = new Point((position.Z - lifter.Joints[JointType.SpineBase].Position.Z) * 153.34 + displayWidth / 2, position.Y * (-153.34) + displayHeight / 2);
                }

                DrawBody(frame.Joints, jointPoints, dc, bodyColor);

                // prevent drawing outside of our render area
                SideProfileDrawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, displayWidth, displayHeight));
            }
        }

        /// <summary>
        /// Draws a body
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="drawingPen">specifies color to draw a specific body</param>
        private void DrawBody(IDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            // Draw the bones
            foreach (var bone in Skeleton.bones)
            {
                DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // Draw the joints
            foreach (JointType jointType in joints.Keys)
            {
                Brush drawBrush = null;

                TrackingState trackingState = joints[jointType].TrackingState;

                if (trackingState == TrackingState.Tracked)
                {
                    drawBrush = trackedJointBrush;
                }
                else if (trackingState == TrackingState.Inferred)
                {
                    drawBrush = inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, jointPoints[jointType], JointThickness, JointThickness);
                }
            }
        }

        /// <summary>
        /// Draws one bone of a body (joint to joint)
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="jointType0">first joint of bone to draw</param>
        /// <param name="jointType1">second joint of bone to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// /// <param name="drawingPen">specifies color to draw a specific bone</param>
        private void DrawBone(IDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == TrackingState.NotTracked ||
                joint1.TrackingState == TrackingState.NotTracked)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = inferredBonePen;
            if ((joint0.TrackingState == TrackingState.Tracked) && (joint1.TrackingState == TrackingState.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, jointPoints[jointType0], jointPoints[jointType1]);
        }

        /// <summary>
        /// Draws a hand symbol if the hand is tracked: red circle = closed, green circle = opened; blue circle = lasso
        /// </summary>
        /// <param name="handState">state of the hand</param>
        /// <param name="handPosition">position of the hand</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawHand(HandState handState, Point handPosition, DrawingContext drawingContext)
        {
            //DrawHand(lifter.HandLeftState, jointPoints[JointType.HandLeft], dc);
            //DrawHand(lifter.HandRightState, jointPoints[JointType.HandRight], dc);

            switch (handState)
            {
                case HandState.Closed:
                    drawingContext.DrawEllipse(handClosedBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Open:
                    drawingContext.DrawEllipse(handOpenBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Lasso:
                    drawingContext.DrawEllipse(handLassoBrush, null, handPosition, HandSize, HandSize);
                    break;
            }
        }

        /// <summary>
        /// Draws indicators to show which edges are clipping body data
        /// </summary>
        /// <param name="body">body to draw clipping information for</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawClippedEdges(Body body, DrawingContext drawingContext)
        {
            FrameEdges clippedEdges = body.ClippedEdges;

            if (clippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, displayHeight - ClipBoundsThickness, displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, displayHeight));
            }

            if (clippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(displayWidth - ClipBoundsThickness, 0, ClipBoundsThickness, displayHeight));
            }
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

        #region TEST_TEMP
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
                    _StartRecording = new RelayCommand(action => StartNewRecording());
                }
                return _StartRecording;
            }
        }
        /// <summary>
        /// Resets the current recording body when the user wants to begin a lift
        /// </summary>
        private void StartNewRecording()
        {
            CurrentRecordingBodyData = new BodyData
            {
                DataFrames = new List<BodyDataFrame>(),
                RecordDate = DateTime.Now
            };
        }

        public ICommand LogoutCommand { get; } = new RelayCommand(action => PageManager.SwitchPage(UIPages.LoginView));

        public RelayCommand _EndRecording { get; private set; }
        public ICommand EndRecording
        {
            get
            {
                if (_EndRecording == null)
                {
                    //_EndRecording = new RelayCommand(action => EndCurrentRecording());
                    _EndRecording = new RelayCommand(async action => await EndCurrentRecording());
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
            //var _bodyData = 

            var toSend = new LiftDTO()
            {
                ParentID = 1,
                Name = LiftName,
                Details = new LiftDetailDTO()
                {
                    BodyData = new BodyDataDTO()
                }

            };

            var bodyDto = Converters.Convert.BodyData.CreateDTO(CurrentRecordingBodyData);
            toSend.Details.BodyData = bodyDto;

            var temp = await DataManager.Lifts.Post(toSend);

            //System.Diagnostics.Debug.WriteLine("The lift was sent to the server {0}", temp.ToString());

            StartNewRecording();
        }

        #endregion

        public string KinectConnected;

        public DataRecorderVM()
        {
            Task.Run(() => SetUser());
            //Task.Run(() => GetAllLifts());

            // one sensor is currently supported
            kinectSensor = KinectSensor.GetDefault();

            // get the coordinate mapper
            coordinateMapper = kinectSensor.CoordinateMapper;
            // get the depth (display) extents

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
                if (LoginScreenVM._Username == singleUser.UserName)
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

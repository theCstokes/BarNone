using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Drawing;
using BarNone.DataLift.UI.ViewModels.Common;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace BarNone.DataLift.UI.ViewModels
{
    /// <summary>
    /// The view model for the edit screen of the Data Lift system.  This control is responsible for editing
    /// lifts that were recorded in a lifting session.
    /// </summary>
    public class EditLiftsScreenVM : ViewModelBase
    {
        #region Video Data Properties
        /// <summary>
        /// Field representation for the <see cref="CurrentLifts"/> bindable property
        /// </summary>
        private CurrentLiftDataVM _currentLifts = CurrentLiftDataVMSingleton.GetInstance();
        /// <summary>
        /// Shared viewmodel reference which holds currently recorded data consistently between VM's
        /// </summary>
        public CurrentLiftDataVM CurrentLifts
        {
            get => _currentLifts;
            set
            {
                _currentLifts = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentLifts"));
            }
        }

        #endregion

        #region Image Properties
        /// <summary>
        /// Drawing group for rendering output of the left image
        /// </summary>
        private DrawingGroup _leftImageDrawingGroup = new DrawingGroup();
        /// <summary>
        /// Field representation for the <see cref="LeftImage"/> bindable property
        /// </summary>
        private DrawingImage _leftImage;
        /// <summary>
        /// Drawing image that we will display on the left
        /// </summary>
        public ImageSource LeftImage
        {
            get => _leftImage;
            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs("LeftImage"));
            }
        }

        /// <summary>
        /// Drawing group for rendering output of the right image
        /// </summary>
        private DrawingGroup _rightImageDrawingGroup = new DrawingGroup();
        /// <summary>
        /// Field representation for the <see cref="RightImage"/> bindable property
        /// </summary>
        private DrawingImage _rightImage;
        /// <summary>
        /// Drawing image that we will display on the right
        /// </summary>
        public ImageSource RightImage
        {
            get => _rightImage;
            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs("RightImage"));
            }
        }

        /// <summary>
        /// Drawing group for rendering output of the middle image
        /// </summary>
        private DrawingGroup _middleImageDrawingGroup = new DrawingGroup();
        /// <summary>
        /// Field representation for the <see cref="MiddleImage"/> bindable property
        /// </summary>
        private DrawingImage _middleImage;
        /// <summary>
        /// Drawing image that we will display in the middle
        /// </summary>
        public ImageSource MiddleImage
        {
            get => _middleImage;
            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs("MiddleImage"));
            }
        }

        #endregion

        #region ListView properties
        /// <summary>
        /// Field representation for the <see cref="LiftIntervals"/> bindable property
        /// </summary>
        private ObservableCollection<LiftListVM> _liftIntervals = new ObservableCollection<LiftListVM>
        {
            new LiftListVM
            {
                LiftName = "Test1",
                LiftType = "Squat",
                LiftStartTime = "00:00:000",
                LiftEndTime = "00:01:000",
                Count = 0
            },
            new LiftListVM
            {
                LiftName = "Test2",
                LiftType = "Clean and Jerk",
                LiftStartTime = "00:01:001",
                LiftEndTime = "00:02:000",
                Count =  1
            }
        };

        /// <summary>
        /// Observable collection to be displayed in the ListView.
        /// </summary>
        public ObservableCollection<LiftListVM> LiftIntervals
        {
            get
            {
                return _liftIntervals;
            }
            set
            {
                if (_liftIntervals == value) return;

                _liftIntervals = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Test"));
            }
        }

        /// <summary>
        /// Field representation for the <see cref="SelectedLift"/> bindable property
        /// </summary>
        private LiftListVM _selectedLift;
        /// <summary>
        /// The lift that is selected in the lift.  Used to bind to video player to display the selected lift.
        /// </summary>
        public LiftListVM SelectedLift
        {
            get { return _selectedLift; }

            set
            {
                if (_selectedLift == value) return;

                _selectedLift = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedLift"));
            }
        }

        #endregion

        #region ListView Commands
        /// <summary>
        /// Field representation for the <see cref="DeleteSelectedRecording"/> bindable property
        /// </summary>
        private RelayCommand _deleteSelectedRecording;
        /// <summary>
        /// Command that calls the function to delete lifts from the ListView.
        /// </summary>
        public ICommand DeleteSelectedRecording
        {
            get
            {
                if (_deleteSelectedRecording == null)
                {
                    _deleteSelectedRecording = new RelayCommand(action => DeleteSelectedRecordingCommand(action));
                }
                return _deleteSelectedRecording;
            }
        }

        /// <summary>
        /// Delete function for the ListView.  Removed the selected lift from the ObservableCollection.
        /// </summary>
        /// <param name="action">The object in the ObservableCollection that called Delete.</param>
        private void DeleteSelectedRecordingCommand(object action)
        {
            // Cast action to a LiftListVM.
            LiftListVM selected = (LiftListVM)action;

            // If action is null then return
            if (action == null) return;

            // Remove the correct lift and redo count for all the remaining lifts in ListView.
            LiftIntervals.RemoveAt(selected.Count);
            for (int i = 0; i < LiftIntervals.Count; i++) LiftIntervals[i].Count = i;
        }
        #endregion

        #region Video Control Commands
        /// <summary>
        /// Field representation for the <see cref="CommandPlayVideo"/> bindable command
        /// </summary>
        private RelayCommand _commandPlayVideo;
        /// <summary>
        /// The command to play/resume the videos.
        /// </summary>
        public ICommand CommandPlayVideo
        {
            get
            {
                if (_commandPlayVideo == null)
                {
                    _commandPlayVideo = new RelayCommand(action => VideoTimer.IsEnabled = true);
                }
                return _commandPlayVideo;
            }
        }

        /// <summary>
        /// Field representation for the <see cref="CommandPauseVideo"/> bindable command
        /// </summary>
        private RelayCommand _commandPauseVideo;
        /// <summary>
        /// The command to pause the videos.
        /// </summary>
        public ICommand CommandPauseVideo
        {
            get
            {
                if (_commandPauseVideo == null)
                {
                    _commandPauseVideo = new RelayCommand(action => VideoTimer.IsEnabled = false);
                }
                return _commandPauseVideo;
            }
        }

        /// <summary>
        /// Field representation for the <see cref="CommandResetVideo"/> bindable command
        /// </summary>
        private RelayCommand _commandResetVideo;
        /// <summary>
        /// The command to reset the videos to the first frame, play or pause is not invoked.
        /// </summary>
        public ICommand CommandResetVideo
        {
            get
            {
                if (_commandResetVideo == null)
                {
                    _commandResetVideo = new RelayCommand(action => Console.WriteLine("Not implemented Action Reset"));
                }
                return _commandResetVideo;
            }
        }

        /// <summary>
        /// Field representation for the <see cref="CommandFastForwardVideo"/> bindable command
        /// </summary>
        private RelayCommand _commandFastForwardVideo;
        /// <summary>
        /// The command to fast forward the videos.
        /// </summary>
        public ICommand CommandFastForwardVideo
        {
            get
            {
                if (_commandFastForwardVideo == null)
                {
                    _commandFastForwardVideo = new RelayCommand(action => Console.WriteLine("Not implemented Action Fast Forward"));
                }
                return _commandFastForwardVideo;
            }
        }

        /// <summary>
        /// Field representation for the <see cref="CommandSlowMotionVideo"/> bindable command
        /// </summary>
        private RelayCommand _commandSlowMotionVideo;
        /// <summary>
        /// The command to play the videos in slow motion, half speed
        /// </summary>
        public ICommand CommandSlowMotionVideo
        {
            get
            {
                if (_commandSlowMotionVideo == null)
                {
                    _commandSlowMotionVideo = new RelayCommand(action => Console.WriteLine("Not implemented Action Slow Motion"));
                }
                return _commandSlowMotionVideo;
            }
        }

        #endregion

        #region Video Controls
        /// <summary>
        /// Control Timer in the UI thread to handle VM to UI timed requests
        /// </summary>
        DispatcherTimer VideoTimer;

        /// <summary>
        /// Current Video frame to draw
        /// </summary>
        private int currentFrame = 0;

        /// <summary>
        /// Redraws each image if required
        /// </summary>
        /// <param name="sender">Event object, unused</param>
        /// <param name="e">Event parameters, unused</param>
        private void Redraw(object sender, EventArgs e)
        {
            KinectToImage.DrawFrameFrontView(CurrentLifts.CurrentRecordedData[0].Details.BodyData.Details.OrderedFrames[currentFrame].Details.Joints, _leftImageDrawingGroup, 424, 424);
            KinectToImage.DrawFrameSideView(CurrentLifts.CurrentRecordedData[0].Details.BodyData.Details.OrderedFrames[currentFrame].Details.Joints, _rightImageDrawingGroup, 424, 424);

            currentFrame = (currentFrame + 1) % CurrentLifts.CurrentRecordedData[0].Details.BodyData.Details.OrderedFrames.Count;
        }

        #endregion

        #region Constructor(s) & Desctructor
        /// <summary>
        /// Instantiates a new EditLiftsScreenVM and sets up the draws
        /// </summary>
        public EditLiftsScreenVM()
        {
            //Init Images
            _leftImage = new DrawingImage(_leftImageDrawingGroup);
            _rightImage = new DrawingImage(_rightImageDrawingGroup);

            //Init Timer
            VideoTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = new TimeSpan(0, 0, 0, 0, 10)

            };

            VideoTimer.Tick += Redraw;
        }

        /// <summary>
        /// Destructor for EditLiftScreenVM, 
        /// </summary>
        ~EditLiftsScreenVM()
        {
            VideoTimer.Stop();
        }
        #endregion

    }
}

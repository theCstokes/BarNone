﻿using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Drawing;
using BarNone.DataLift.UI.ViewModels.Common;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace BarNone.DataLift.UI.ViewModels
{
    #region Types
    public class PositionUpdateEventArgs : EventArgs
    {
        public TimeSpan Position { get; set; }

        public PositionUpdateEventArgs(TimeSpan position) : base()
        {
            Position = position;
        }
    }

    #endregion

    /// <summary>
    /// The view model for the edit screen of the Data Lift system.  This control is responsible for editing
    /// lifts that were recorded in a lifting session.
    /// </summary>
    public class EditLiftsScreenVM : ViewModelBase
    {
        #region Lift Data Properties
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
        
        /// <summary>
        /// The lift that is selected in the lift.  Used to bind to video player to display the selected lift.
        /// </summary>
        public string CurrentLiftTime
        {
            get
            {
                TimeSpan currentTime = new TimeSpan(0, 0, 0, 0, currentMs);
                return $"{currentTime.Minutes:00}:{currentTime.Seconds:00}.{currentTime.Milliseconds:000}";
            }
        }
        
        /// <summary>
        /// The lift that is selected in the lift.  Used to bind to video player to display the selected lift.
        /// </summary>
        public string CurrentEndTime
        {
            get
            {
                TimeSpan currentTime = new TimeSpan(0, 0, 0, 0, LiftEndTime);
                return $"{currentTime.Minutes:00}:{currentTime.Seconds:00}.{currentTime.Milliseconds:000}";
            }
        }

        public int LiftEndTime
        {
            get
            {
                if (CurrentLifts.LiftInformation.Count() != 0)
                {
                    var temp = CurrentLifts.CurrentRecordedBodyData.Select(x => x.TimeOfFrame);
                    return (int)(temp.Max(x => x.TotalMilliseconds) + 1 / 30d * 1000);
                }

                else return 0;
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

        #endregion

        #region Video Properties
        private Uri _videoUri;

        /// <summary>
        /// Video Source Link, ensure full fath is used!
        /// </summary>
        public Uri VideoUri
        {
            get => _videoUri;
            set
            {
                _videoUri = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VideoUri"));
            }
        }

        public bool HasVideo
        {
            get => _videoUri != null && File.Exists(_videoUri.AbsolutePath);
        }

        private TimeSpan _videoPosition;
        public TimeSpan VideoPosition
        {
            get => _videoPosition;
            set
            {
                _videoPosition = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VideoPosition"));
            }
        }

        public event EventHandler PlayRequested;
        public event EventHandler StopRequested;
        public event EventHandler PauseRequested;
        public event EventHandler<PositionUpdateEventArgs> UpdatePositionEvent;

        #endregion

        #region Loaded and Closed
        internal override void Loaded()
        {
            SelectedLift = CurrentLifts.LiftInformation[0];

            VideoUri = new Uri(Path.GetFullPath(CurrentLifts.ParentLiftVideoName));
            if (HasVideo)
                StopRequested.Invoke(this, EventArgs.Empty);

            ScrubberUpperThumb = LiftEndTime;

            OnPropertyChanged(new PropertyChangedEventArgs("ScrubberMaxValue"));
            OnPropertyChanged(new PropertyChangedEventArgs("ScrubberMinValue"));

            OnPropertyChanged(new PropertyChangedEventArgs("CurrentEndTime"));

            OnPropertyChanged(new PropertyChangedEventArgs("ScrubberLowerThumb"));
            OnPropertyChanged(new PropertyChangedEventArgs("ScrubberUpperThumb"));
        }

        internal override void Closed()
        {
            OnVideoPaused();
            base.Closed();
        }

        #endregion

        #region ListView properties
        /// <summary>
        /// Observable collection to be displayed in the ListView.
        /// </summary>
        public ObservableCollection<LiftItemVM> LiftIntervals
        {
            get
            {
                return CurrentLifts.LiftInformation;
            }
            set
            {
                if (CurrentLifts.LiftInformation == value) return;

                CurrentLifts.LiftInformation = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LiftIntervals"));
            }
        }
        
        /// <summary>
        /// Field representation for the <see cref="SelectedLift"/> bindable property
        /// </summary>
        private LiftItemVM _selectedLift;
        /// <summary>
        /// The lift that is selected in the lift.  Used to bind to video player to display the selected lift.
        /// </summary>
        public LiftItemVM SelectedLift
        {
            get { return _selectedLift; }

            set
            {
                if (_selectedLift == value) return;

                _selectedLift = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedLift"));
                
                try
                {
                    ScrubberLowerThumb = SelectedLift.LiftStartTime;
                }
                catch { ScrubberLowerThumb = 0d; }

                try
                {
                    //ScrubberUpperThumb = TimeSpan.ParseExact(SelectedLift.LiftEndTime, @"m\:s\.fff", CultureInfo.InvariantCulture).TotalMilliseconds;
                    ScrubberUpperThumb = SelectedLift.LiftEndTime;
                }
                catch { ScrubberUpperThumb = 0d; }

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
            if(CurrentLifts.LiftInformation.Count == 0)
            {
                return;
            }
            
            // Cast action to a LiftListVM.
            LiftItemVM selected = (LiftItemVM)action;
            
            // If action is null then return
            if (action == null) return;

            // Remove the correct lift and redo count for all the remaining lifts in ListView.
            LiftIntervals.RemoveAt(selected.Count);

            SelectedLift = CurrentLifts.LiftInformation[selected.Count];

            for (int i = 0; i < LiftIntervals.Count; i++) LiftIntervals[i].Count = i;
        }

        private RelayCommand _addRecording;
        /// <summary>
        /// Command that calls the function to delete lifts from the ListView.
        /// </summary>
        public ICommand AddRecording
        {
            get
            {
                if (_addRecording == null)
                {
                    _addRecording = new RelayCommand(action => AddRecordingCommand());
                }
                return _addRecording;
            }
        }

        private void AddRecordingCommand()
        {
            CurrentLifts.LiftInformation.Add(new LiftItemVM(CurrentLifts.CurrentUser)
            {
                LiftName = $"Lift {CurrentLifts.LiftInformation.Count}",
                LiftStartTime = 0,
                LiftEndTime = LiftEndTime,
                LiftType = "Squat"  
            });

            OnPropertyChanged(new PropertyChangedEventArgs("LiftIntervals"));
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
                    _commandPlayVideo = new RelayCommand(action => OnVideoPlayed());
                }
                return _commandPlayVideo;
            }
        }

        private void OnVideoPlayed()
        {
            GlobalTimer.Start();
            VideoTimer.IsEnabled = true;
            //Command the video to play
            PlayRequested?.Invoke(this, EventArgs.Empty);
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
                    _commandPauseVideo = new RelayCommand(action => OnVideoPaused());
                }
                return _commandPauseVideo;
            }
        }

        private void OnVideoPaused()
        {
            GlobalTimer.Stop();
            VideoTimer.IsEnabled = false;
            //Command the video to play
            PauseRequested?.Invoke(this, EventArgs.Empty);
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
        /// Video playback synchronizer timer in Ms
        /// </summary>
        private const int VideoTimerInterval = 30;
        /// <summary>
        /// Control Timer in the UI thread to handle VM to UI timed requests
        /// </summary>
        DispatcherTimer VideoTimer;

        /// <summary>
        /// Current Video frame to draw
        /// </summary>
        private int currentBodyDataFrame = 0;

        private int currentMs = 0;

        /// <summary>
        /// Redraws each image if required
        /// </summary>
        /// <param name="sender">Event object, unused</param>
        /// <param name="e">Event parameters, unused</param>
        private void Redraw(object sender, EventArgs e)
        {
            BodyDataFrame bodyFrameToDraw;

            bool drawBody = false;
            if (currentMs <= ScrubberLowerThumb)
            {
                GlobalTimer.timeOffset = 0;

                GlobalTimer.Restart();

                //Will only happen on loaded
                //colorFrameToDraw = CurrentLifts.CurrentRecordedColorData[currentColorDataFrame];
                bodyFrameToDraw = CurrentLifts.CurrentRecordedBodyData[currentBodyDataFrame];
                
                drawBody = true;
                //var ianTheCaptainLater = CurrentLifts.CurrentRecordedBodyData.Select(x => x.TimeOfFrame);

            }
            else
            {
                currentMs = (int)GlobalTimer.ElapsedMilliseconds;

                int nextBodyFrame = 0;

                for (int i = 0; i < CurrentLifts.CurrentRecordedBodyData.Count; i++)
                {
                    var frame = CurrentLifts.CurrentRecordedBodyData[i];
                    if (frame.TimeOfFrame.TotalMilliseconds > ScrubberCurrentPosition)
                    {
                        nextBodyFrame = i - 1;
                        break;
                    }
                }

                if (nextBodyFrame < 0)
                    return;
                bodyFrameToDraw = CurrentLifts.CurrentRecordedBodyData[nextBodyFrame];

                if (bodyFrameToDraw.TimeOfFrame.TotalMilliseconds < currentMs)
                {
                    drawBody = true;
                    currentBodyDataFrame = nextBodyFrame;
                }
            }

            if (drawBody)
            {
                //Console.WriteLine($"Time of body print {GlobalTimer.ElapsedMilliseconds}");

                KinectToImage.DrawFrameFrontView(bodyFrameToDraw, _leftImageDrawingGroup, 424, 424);
                KinectToImage.DrawFrameSideView(bodyFrameToDraw, _rightImageDrawingGroup, 424, 424);
            }

            if (currentMs > ScrubberUpperThumb)
            {
                currentMs = (int)ScrubberLowerThumb;

                //increment timer value
                ScrubberCurrentPosition = ScrubberLowerThumb;
                StopRequested.Invoke(this, EventArgs.Empty);
                PlayRequested.Invoke(this, EventArgs.Empty);
                UpdatePositionEvent.Invoke(this, new PositionUpdateEventArgs(new TimeSpan(0, 0, 0, 0, (int)GlobalTimer.startOffset)));
                GlobalTimer.Restart();
            }
            else
            {
                currentMs += (1/VideoTimerInterval)*1000;
                //increment timer value
                ScrubberCurrentPosition += VideoTimerInterval;

                OnPropertyChanged(new PropertyChangedEventArgs("CurrentLiftTime"));
            }

            VideoPosition = GlobalTimer.GetTimeSpanPosition();
            
        }

        #endregion

        #region Scrubber Controls

        public bool IsHeld
        {
            get => IsHeld;
            set
            {
                Console.WriteLine("JEHAHHAHHSHHAHAH");
            }
        }

        public double ScrubberCurrentPosition
        {
            get => currentMs;
            set
            {
                currentMs = (int)value;
                OnPropertyChanged(new PropertyChangedEventArgs("ScrubberCurrentPosition"));
            }
        }

        private double _scrubberUpperThumb;
        public double ScrubberUpperThumb
        {
            get => _scrubberUpperThumb;
            set
            {
                _scrubberUpperThumb = value;
                if (SelectedLift != null)
                    SelectedLift.LiftEndTime = value;

                if (ScrubberLowerThumb > value)
                {
                    ScrubberLowerThumb = value;
                    if (SelectedLift != null)
                        SelectedLift.LiftStartTime = value;
                }

                OnPropertyChanged(new PropertyChangedEventArgs("ScrubberUpperThumb"));
            }
        }

        private double _scrubberLowerThumb;
        public double ScrubberLowerThumb
        {
            get => _scrubberLowerThumb;
            set
            {
                _scrubberLowerThumb = value;
                if (SelectedLift != null)
                    SelectedLift.LiftStartTime = value;

                GlobalTimer.startOffset = (long)Math.Ceiling(value);

                if (currentMs > value)
                    GlobalTimer.timeOffset = (long)(currentMs - Math.Ceiling(value));
                else
                    GlobalTimer.timeOffset = 0;

                GlobalTimer.Restart();

                if (ScrubberUpperThumb < value)
                {
                    ScrubberUpperThumb = value;
                    if (SelectedLift != null)
                        SelectedLift.LiftEndTime = value;

                }
                OnPropertyChanged(new PropertyChangedEventArgs("ScrubberLowerThumb"));
            }
        }

        public double ScrubberMaxValue
        {
            get
            {
                if (CurrentLifts.CurrentRecordedBodyData.Count == 0)
                    return 0d;

                return CurrentLifts.CurrentRecordedBodyData.Max(x => x.TimeOfFrame.TotalMilliseconds);
            }
        }

        public double ScrubberMinValue
        {
            get
            {
                if (CurrentLifts.CurrentRecordedBodyData.Count == 0)
                    return 0d;

                return CurrentLifts.CurrentRecordedBodyData.Min(x => x.TimeOfFrame.TotalMilliseconds);
            }
        }
        #endregion

        CustomTimer GlobalTimer = new CustomTimer();

        #region Constructor(s) & Desctructor
        /// <summary>
        /// Instantiates a new EditLiftsScreenVM and sets up the draws
        /// </summary>
        public EditLiftsScreenVM()
        {
            GlobalTimer.Start();
            //Init Images
            _leftImage = new DrawingImage(_leftImageDrawingGroup);
            _rightImage = new DrawingImage(_rightImageDrawingGroup);

            //Init Timer
            VideoTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = new TimeSpan(0, 0, 0, 0, (1/VideoTimerInterval) * 1000)

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

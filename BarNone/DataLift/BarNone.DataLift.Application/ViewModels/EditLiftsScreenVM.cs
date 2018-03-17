﻿using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Drawing;
using BarNone.DataLift.UI.Models;
using BarNone.DataLift.UI.ViewModels.Common;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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

        private string _currentLiftTime;
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

        private string _currentEndTime;
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

        #region Loaded and Closed
        internal override void Loaded()
        {
            CurrentLifts.LiftInformation[0].LiftEndTime = LiftEndTime;
            SelectedLift = CurrentLifts.LiftInformation[0];

            // TODO.  Send raw data to jon then have him send us a list of stuff back.

            //Console.WriteLine($"This is how long we are shizz: {CurrentLifts.LiftInformation.Count}");
        }

        internal override void Closed()
        {
            base.Closed();
        }

        #endregion

        #region ListView properties
        /// <summary>
        /// Field representation for the <see cref="LiftIntervals"/> bindable property
        /// </summary>
        //private ObservableCollection<LiftListVM> _liftIntervals = new ObservableCollection<LiftListVM>
        //{
        //    new LiftListVM
        //    {
        //        LiftName = "Test1",
        //        LiftType = "Squat",
        //        LiftStartTime = @"00:00.000",
        //        LiftEndTime = @"00:01.000",
        //        Count = 0
        //    },
        //    new LiftListVM
        //    {
        //        LiftName = "Test2",
        //        LiftType = "Clean and Jerk",
        //        LiftStartTime = @"00:01.001",
        //        LiftEndTime = @"00:02.000",
        //        Count =  1
        //    }
        //};

        /// <summary>
        /// Observable collection to be displayed in the ListView.
        /// </summary>
        public ObservableCollection<LiftListVM> LiftIntervals
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

                //OnPropertyChanged(new PropertyChangedEventArgs("ScrubberCurrentPosition"));
                //OnPropertyChanged(new PropertyChangedEventArgs("ScrubberUpperThumb"));
                //OnPropertyChanged(new PropertyChangedEventArgs("ScrubberLowerThumb"));

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
            // Cast action to a LiftListVM.
            LiftListVM selected = (LiftListVM)action;

            // If action is null then return
            if (action == null) return;

            // Remove the correct lift and redo count for all the remaining lifts in ListView.
            LiftIntervals.RemoveAt(selected.Count);
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
            CurrentLifts.LiftInformation.Add(new LiftListVM
            {
                LiftName = $"Lift {CurrentLifts.LiftInformation.Count}",
                LiftStartTime = 0,
                LiftEndTime = LiftEndTime,
                LiftType = "Squat"  
            });
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
        private const int VideoTimerInterval = 15;
        /// <summary>
        /// Control Timer in the UI thread to handle VM to UI timed requests
        /// </summary>
        DispatcherTimer VideoTimer;

        /// <summary>
        /// Current Video frame to draw
        /// </summary>
        private int currentBodyDataFrame = 0;
        private int currentColorDataFrame = 0;

        private int currentMs = 0;
        private int LoopTime;

        /// <summary>
        /// Redraws each image if required
        /// </summary>
        /// <param name="sender">Event object, unused</param>
        /// <param name="e">Event parameters, unused</param>
        private void Redraw(object sender, EventArgs e)
        {
            BodyDataFrame bodyFrameToDraw;
            ColorImageFrame colorFrameToDraw;
            
            bool drawColor = false, drawBody = false;
            if (currentMs == 0)
            {
                GlobalTimer.Restart();

                //Will only happen on loaded
                //colorFrameToDraw = CurrentLifts.CurrentRecordedColorData[currentColorDataFrame];
                 bodyFrameToDraw = CurrentLifts.CurrentRecordedBodyData[currentBodyDataFrame];
                
                drawColor = true;
                drawBody = true;
                var ianTheCaptainLater = CurrentLifts.CurrentRecordedBodyData.Select(x => x.TimeOfFrame);
                LoopTime = (int)(ianTheCaptainLater.Max(x => x.TotalMilliseconds) + 1/30d * 1000);

                OnPropertyChanged(new PropertyChangedEventArgs("ScrubberMaxValue"));
                OnPropertyChanged(new PropertyChangedEventArgs("ScrubberMinValue"));

                OnPropertyChanged(new PropertyChangedEventArgs("CurrentEndTime")); 

                OnPropertyChanged(new PropertyChangedEventArgs("ScrubberLowerThumb"));
                OnPropertyChanged(new PropertyChangedEventArgs("ScrubberUpperThumb"));
            }
            else
            {
                if(Math.Abs(currentMs - GlobalTimer.ElapsedMilliseconds) > 25)
                {
                    Console.Out.WriteLine("The current value of CMS is {0} and Timer is {1}", currentMs, GlobalTimer.ElapsedMilliseconds);
                    GlobalTimer.Restart();
                    GlobalTimer.ElapsedMilliseconds = currentMs;
                }
                
                currentMs = (int)GlobalTimer.ElapsedMilliseconds;

                int nextBodyFrame = 0;

                for (int i = 0; i < CurrentLifts.CurrentRecordedBodyData.Count; i++)
                {
                    var frame = CurrentLifts.CurrentRecordedBodyData[i];
                    if(frame.TimeOfFrame.TotalMilliseconds > ScrubberCurrentPosition)
                    {
                        nextBodyFrame = i - 1;
                        break;
                    }
                }
                
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
            
            if ((currentMs > LoopTime) || (currentMs > ScrubberUpperThumb))
            {
                currentMs = (int)ScrubberLowerThumb;

                //increment timer value
                ScrubberCurrentPosition = ScrubberLowerThumb;

            }
            else
            {
                currentMs += VideoTimerInterval;
                //increment timer value
                ScrubberCurrentPosition += VideoTimerInterval;
                OnPropertyChanged(new PropertyChangedEventArgs("ScrubberCurrentPosition"));
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentLiftTime"));
            }
        }

        #endregion

        #region Scrubber Controls
        public double ScrubberCurrentPosition
        {
            get => currentMs;
            set
            {
                //var tempTS = new TimeSpan(0,0,0,0,(int)value);
                //GlobalTimer.Reset();
                //GlobalTimer.
                currentMs = (int)value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentScrubberPosition"));
            }
        }

        private double _scrubberUpperThumb;
        public double ScrubberUpperThumb
        {
            get
            {
                return _scrubberUpperThumb;
            }
            set
            {
                //Console.WriteLine($"This is the value of the lower thumb {value}");

               //if(SelectedLift != null) SelectedLift.LiftEndTime = value.ToString();

                if(ScrubberLowerThumb <= value)
                {
                    _scrubberUpperThumb = value;
                    SelectedLift.LiftEndTime = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("ScrubberUpperThumb"));
                }
                else
                {
                    ScrubberLowerThumb = value;
                    SelectedLift.LiftStartTime = value;

                    _scrubberUpperThumb = value;
                    SelectedLift.LiftEndTime = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ScrubberUpperThumb"));
                }

                //new PropertyChangedEventArgs("ScrubberUpperDisplayed");
            }
        }

        private double _scrubberLowerThumb;
        public double ScrubberLowerThumb
        {
            get
            {
                 return _scrubberLowerThumb;
            }

            set
            {
                //Console.WriteLine($"This is the value of the lower thumb {value}");

                //if (SelectedLift != null) SelectedLift.LiftStartTime = value.ToString();
                if(ScrubberUpperThumb >= value)
                {
                    _scrubberLowerThumb = value;
                    SelectedLift.LiftStartTime = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("ScrubberLowerThumb"));
                }
                else
                {
                    ScrubberUpperThumb = value;
                    SelectedLift.LiftEndTime = value;

                    _scrubberLowerThumb = value;
                    SelectedLift.LiftStartTime = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ScrubberLowerThumb"));
                }
            }
        }
        
        public double ScrubberMaxValue
        {
            get
            {
                if (CurrentLifts.CurrentRecordedBodyData.Count == 0) return 0d;

                //if (CurrentLifts.CurrentRecordedColorData.Count == 0 || CurrentLifts.CurrentRecordedBodyData.Count == 0)
                //    return 0d;

                //return Math.Max(CurrentLifts.CurrentRecordedColorData.Max(x => x.Time.TotalMilliseconds), CurrentLifts.CurrentRecordedBodyData.Max(x => x.TimeOfFrame.TotalMilliseconds));
                return CurrentLifts.CurrentRecordedBodyData.Max(x => x.TimeOfFrame.TotalMilliseconds);
            }
        }

        public double ScrubberMinValue
        {
            get
            {
                //if (CurrentLifts.CurrentRecordedColorData.Count == 0 || CurrentLifts.CurrentRecordedBodyData.Count == 0)
                //    return 0d;

                //return Math.Min(CurrentLifts.CurrentRecordedColorData.Min(x => x.Time.TotalMilliseconds), CurrentLifts.CurrentRecordedBodyData.Min(x => x.TimeOfFrame.TotalMilliseconds));

                if (CurrentLifts.CurrentRecordedBodyData.Count == 0) return 0d;

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
            _middleImage = new DrawingImage(_middleImageDrawingGroup);

            //Init Timer
            VideoTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = new TimeSpan(0, 0, 0, 0, VideoTimerInterval)

            };

            //CurrentLifts.LiftInformation.Add(new LiftListVM
            //{
            //    LiftStartTime = 0,
            //    LiftEndTime = 0,
            //    LiftName = String.Format($"Temp_name_{CurrentLifts.LiftInformation.Count()}"),
            //    LiftType = "Squat"
            //});

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

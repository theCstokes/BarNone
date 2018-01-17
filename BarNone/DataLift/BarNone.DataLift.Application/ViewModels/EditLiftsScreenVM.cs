﻿using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Drawing;
using BarNone.DataLift.UI.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace BarNone.DataLift.UI.ViewModels
{
    public class EditLiftsScreenVM : ViewModelBase
    {
        #region Video Control Commands
        private RelayCommand _commandPlayVideo;
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

        private RelayCommand _commandPauseVideo;
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

        private RelayCommand _commandResetVideo;
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

        private RelayCommand _commandFastForwardVideo;
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

        private RelayCommand _commandSlowMotionVideo;
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

        private CurrentLiftDataVM _currentLifts = CurrentLiftDataVMSingleton.GetInstance();
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
        /// Drawing group for rendering output
        /// </summary>
        private DrawingGroup _leftImageDrawingGroup = new DrawingGroup();
        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage _leftImage;
        public ImageSource LeftImage
        {
            get => _leftImage;
            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs("LeftImage"));
            }
        }

        /// <summary>
        /// Drawing group for rendering output
        /// </summary>
        private DrawingGroup _rightImageDrawingGroup = new DrawingGroup();
        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage _rightImage;
        public ImageSource RightImage
        {
            get => _rightImage;
            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs("RightImage"));
            }
        }




        DispatcherTimer VideoTimer;


        //TMP
        int currentFrame = 0;

        private void Redraw(object sender, EventArgs e)
        {
            KinectToImage.DrawFrameFrontView(CurrentLifts.CurrentRecordedData[0].Details.BodyData.Details.OrderedFrames[currentFrame].Details.Joints, _leftImageDrawingGroup, 424, 424);
            KinectToImage.DrawFrameSideView(CurrentLifts.CurrentRecordedData[0].Details.BodyData.Details.OrderedFrames[currentFrame].Details.Joints, _rightImageDrawingGroup, 424, 424);

            currentFrame = (currentFrame + 1) % CurrentLifts.CurrentRecordedData[0].Details.BodyData.Details.OrderedFrames.Count;
        }
        
        #region Constructor(s) & Desctructor
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

        ~EditLiftsScreenVM()
        {
            VideoTimer.Stop();
        }

        #endregion

    }
}

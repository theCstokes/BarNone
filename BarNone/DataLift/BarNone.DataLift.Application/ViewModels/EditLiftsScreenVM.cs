using BarNone.DataLift.UI.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarNone.DataLift.UI.ViewModels
{
    public class EditLiftsScreenVM : ViewModelBase
    {
        #region ListView properties

        private ObservableCollection<LiftListVM> _test = new ObservableCollection<LiftListVM>
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
        public ObservableCollection<LiftListVM> Test
        {
            get
            {
                return _test;  
            }
            set
            {
                if (_test == value) return;

                _test = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Test"));
            }
        }

        private LiftListVM _selectedLift;
        public LiftListVM SelectedLift
        {
            get { return _selectedLift;  }

            set
            {
                if (_selectedLift == value) return;

                _selectedLift = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedLift"));
            }
        }

        #endregion

        #region ListView Commands
        public RelayCommand _deleteSelectedRecording { get; private set; }
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

        private void DeleteSelectedRecordingCommand(object action)
        {
            LiftListVM selected = (LiftListVM)action;

            if (action == null) return;

            Test.RemoveAt(selected.Count);
            for (int i = 0; i < Test.Count; i++) Test[i].Count = i;

            //Console.WriteLine(selected.LiftName);
            //Console.WriteLine(selected.LiftType);
        }
        #endregion

        #region Video Control Commands
        private RelayCommand _commandPlayVideo;
        public ICommand CommandPlayVideo
        {
            get
            {
                if (_commandPlayVideo == null)
                {
                    _commandPlayVideo = new RelayCommand(action => Console.WriteLine("Not implemented Action Play"));
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
                    _commandPauseVideo = new RelayCommand(action => Console.WriteLine("Not implemented Action Pause"));
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
    }
}

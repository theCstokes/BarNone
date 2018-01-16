using BarNone.DataLift.UI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

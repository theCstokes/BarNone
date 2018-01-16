using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Nav;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BarNone.DataLift.UI.ViewModels
{
    public class ControlHolderVM : ViewModelBase
    {
        #region Public Commands
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

        /// <summary>
        /// Bound command to move wordlow forwards in XAML.
        /// </summary>
        public RelayCommand _MoveWorflowForwardCmd { get; private set; }
        public ICommand MoveWorflowForwardCmd
        {
            get
            {
                if (_MoveWorflowForwardCmd == null)
                {
                    _MoveWorflowForwardCmd = new RelayCommand(action => MoveWorflowForward());
                }
                return _MoveWorflowForwardCmd;
            }
        }

        /// <summary>
        /// Bound command to move wordlow backwards in XAML.
        /// </summary>
        public RelayCommand _MoveWorflowBackwardCmd { get; private set; }
        public ICommand MoveWorflowBackwardCmd
        {
            get
            {
                if (_MoveWorflowBackwardCmd == null)
                {
                    _MoveWorflowBackwardCmd = new RelayCommand(action => MoveWorkflowBackward());
                }
                return _MoveWorflowBackwardCmd;
            }
        }

        public ICommand LogoutCommand { get; } = new RelayCommand(action => PageManager.SwitchPage(UIPages.LoginView));

        /// <summary>
        /// Variable that controls the visibility of the Recorder Screen.
        /// </summary>
        private bool _isRecorderVisible = true;
        public bool IsRecorderVisible
        {
            get => _isRecorderVisible;
            set
            {
                _isRecorderVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsRecorderVisible"));
            }
        }

        #endregion
        
        #region Public Properties
        /// <summary>
        /// Variable that controls the visibility of the Editor Screen.
        /// </summary>
        private bool _isEditorVisible = false;
        public bool IsEditorVisible
        {
            get => _isEditorVisible;
            set
            {
                _isEditorVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsEditorVisible"));
            }
        }

        /// <summary>
        /// Variable that controls the visibility of the Saving Screen.
        /// </summary>
        private bool _isSavingVisible = false;
        public bool IsSavingVisible
        {
            get => _isSavingVisible;
            set
            {
                _isSavingVisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsSavingVisible"));
            }
        }

        private bool _isBackwardsEnabled = false;

        public bool IsBackwardsEnabled
        {
            get => _isBackwardsEnabled;
            set
            {
                _isBackwardsEnabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsBackwardsEnabled"));
            }
        }

        private bool _isStepTwoEnabledController = false;
        public bool IsStepTwoEnabledController
        {
            get => _isStepTwoEnabledController;
            set
            {
                _isStepTwoEnabledController = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsStepTwoEnabledController"));
            }
        }


        private bool _isStepThreeEnabledController = false;
        public bool IsStepThreeEnabledController
        {
            get => _isStepThreeEnabledController;
            set
            {
                _isStepThreeEnabledController = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsStepThreeEnabledController"));
            }
        }

        #endregion

        #region Private Variables

        private State _currentState = State.Recording;

        private MessageBoxResult result;

        #endregion

        #region Private Enums
        /// <summary>
        /// Enumeration defininng which state DataLift is in.
        /// </summary>
        private enum State {Recording, Editing, Saving};
        #endregion

        #region Private Functions
        private void TestStrategy1_ResetKinectSensor()
        {
            //IsVisible = !IsVisible;

            //kinectSensor.Close();
            //kinectSensor.Open();
        }
        /// <summary>
        /// Moves forward in the Data Lift Workflow.  Changes state to move the workflow along.
        /// </summary>
        private void MoveWorflowForward()
        {
            switch (_currentState)
            {
                case State.Recording:
                    GotoEditingState();
                    break;
                case State.Editing:
                    GotoSavingState();
                    break;
                case State.Saving:
                    GotoRecordingState();
                    break;
            }
        }

        /// <summary>
        /// Moves backward in the Data Lift Workflow.  Changes state to move the workflow back.
        /// </summary>        
        private void MoveWorkflowBackward()
        {
            switch (_currentState)
            {
                case State.Recording:
                    // Do nothing.  Button should be disabled.
                    break;
                case State.Editing:
                    // TODO Prompt user they may loose data. Implmementation may be wrong.
                    result = MessageBox.Show("You may loose editing data if you choose to return to the recording page.  Do you wish to continue?", "Warning:  Data may be lost", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(result == MessageBoxResult.Yes)
                    {
                        GotoRecordingState();
                    }
                    break;
                case State.Saving:
                    // TODO Prompt user they may loose data.  Implmementation may be wrong.
                    result = MessageBox.Show("You may loose lift information data if you choose to return to the editing page.  Do you wish to continue?", "Warning:  Data may be lost", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        GotoEditingState();
                    }
                    break;
            }
        }

        /// <summary>
        /// Changes state to recording state.
        /// </summary>
        private void GotoRecordingState()
        {
            _currentState = State.Recording;

            IsBackwardsEnabled = false;

            IsRecorderVisible = true;
            IsEditorVisible = false;
            IsSavingVisible = false;
        }

        /// <summary>
        /// Changes state to editing state.
        /// </summary>
        private void GotoEditingState()
        {
            _currentState = State.Editing;

            IsBackwardsEnabled = true;
            IsStepTwoEnabledController = true;
            IsStepThreeEnabledController = true;

            IsRecorderVisible = false;
            IsEditorVisible = true;
            IsSavingVisible = false;
        }

        /// <summary>
        /// Changes state to editing state.
        /// </summary>
        private void GotoSavingState()
        {
            _currentState = State.Saving;

            IsRecorderVisible = false;
            IsEditorVisible = false;
            IsSavingVisible = true;
        }

        #endregion 

    }


}

using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Nav;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Windows.Input;
using BarNone.DataLift.UI.Views;

namespace BarNone.DataLift.UI.ViewModels
{
    /// <summary>
    /// The view model for control holder.  The control holder is the screen that holds all the user controls 
    /// for Data Lift.  It is essentiall the "background" of the system.  It is responsible for dictating the 
    /// state of the system, and what screens the user can interact with in a specific state.
    /// </summary>
    public class ControlHolderVM : ViewModelBase
    {
        #region Types
        /// <summary>
        /// Private enumeration defininng which state DataLift is in.
        /// </summary>
        private enum State
        {
            /// <summary>
            /// The sub-view is the Recording Screen
            /// </summary>
            Recording,
            /// <summary>
            /// The sub-view is the Editing Screen
            /// </summary>
            Editing,
            /// <summary>
            /// The sub-view is the Save and Share Screen
            /// </summary>
            Saving
        };
        #endregion

        #region Public Commands
        /// <summary>
        /// Asynchronous function that controls the warning dialog.
        /// Shows warning telling the user that they may loose data if they go backwards in the workflow.
        /// </summary>
        /// <param name="o">Unused</param>
        private async void ExecuteRunDialog(object o)
        {
            // Create a new dialog.
            var view = new YesNoDialogScreen();

            //  Look for RootDialog in the XAML (view) and wait until the dialog has completed execution.
            var result = await DialogHost.Show(view, "RootDialog");

            // If the user wishes to take the risk and potentially loose data move backwards in the workflow.
            if (Equals(result, false))
            {
                MoveWorkflowBackward();
            }
        }

        /// <summary>
        /// Field representation for the <see cref="RunDialogCommand"/> bindable command
        /// </summary>
        private RelayCommand _runDialogCommand;
        /// <summary>
        /// Bindable command to open a new dialog box
        /// </summary>
        public ICommand RunDialogCommand
        {
            get
            {
                if (_runDialogCommand == null)
                {
                    _runDialogCommand =  new RelayCommand(ExecuteRunDialog);
                }
                return _runDialogCommand;
            }
        }

        /// <summary>
        /// Field representation for the <see cref="TestStrategy1"/> bindable command
        /// </summary>
        private RelayCommand _testStrategy1;
        /// <summary>
        /// Currently unused function to reset the Kinect.  Still bound to a button in the UI.  May be used in the future for debug.
        /// </summary>
        public ICommand TestStrategy1
        {
            get
            {
                if (_testStrategy1 == null)
                {
                    _testStrategy1 = new RelayCommand(action => TestStrategy1_ResetKinectSensor());
                }
                return _testStrategy1;
            }
        }

        /// <summary>
        /// Field representation for the <see cref="MoveWorflowForwardCmd"/> bindable command
        /// </summary>
        private RelayCommand _moveWorflowForwardCmd;
        /// <summary>
        /// Bound command to move wordlow forwards in XAML.
        /// </summary>
        public ICommand MoveWorflowForwardCmd
        {
            get
            {
                if (_moveWorflowForwardCmd == null)
                {
                    _moveWorflowForwardCmd = new RelayCommand(action => MoveWorflowForward());
                }
                return _moveWorflowForwardCmd;
            }
        }

        /// <summary>
        /// Logout command.Disposes user data and moves back to the login page.
        /// </summary>
        public ICommand LogoutCommand { get; } = new RelayCommand(action => PageManager.SwitchPage(UIPages.LoginView));
        #endregion

        #region Public Properties
        ///// <summary>
        ///// Field representation for the <see cref="IsRecorderVisible"/> bindable property
        ///// </summary>
        //private bool _isRecorderVisible = true;
        ///// <summary>
        ///// Variable that controls the visibility of the Recorder Screen.
        ///// </summary>
        //public bool IsRecorderVisible
        //{
        //    get => _isRecorderVisible;
        //    set
        //    {
        //        _isRecorderVisible = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("IsRecorderVisible"));
        //    }
        //}

        ///// <summary>
        ///// Field representation for the <see cref="IsEditorVisible"/> bindable property
        ///// </summary>
        //private bool _isEditorVisible = false;
        ///// <summary>
        ///// Variable that controls the visibility of the Editor Screen.
        ///// </summary>
        //public bool IsEditorVisible
        //{
        //    get => _isEditorVisible;
        //    set
        //    {
        //        _isEditorVisible = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("IsEditorVisible"));
        //    }
        //}

        ///// <summary>
        ///// Field representation for the <see cref="IsSavingVisible"/> bindable property
        ///// </summary>
        //private bool _isSavingVisible = false;
        ///// <summary>
        ///// Variable that controls the visibility of the Saving Screen.
        ///// </summary>
        //public bool IsSavingVisible
        //{
        //    get => _isSavingVisible;
        //    set
        //    {
        //        _isSavingVisible = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("IsSavingVisible"));
        //    }
        //}

        /// <summary>
        /// Field representation for the <see cref="IsBackwardsEnabled"/> bindable property
        /// </summary>
        private bool _isBackwardsEnabled = false;
        /// <summary>
        /// Controls whether the move backwards in workflow button can be pressed.  
        /// Because you cannot move backwards in the worflow if you are in the first step.
        /// </summary>
        public bool IsBackwardsEnabled
        {
            get => _isBackwardsEnabled;
            set
            {
                _isBackwardsEnabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsBackwardsEnabled"));
            }
        }

        /// <summary>
        /// Field representation for the <see cref="IsStepTwoEnabledController"/> bindable property
        /// </summary>
        private double _isStepTwoEnabledController = 0.5;
        /// <summary>
        /// Determines the opacity of the button for step 2 of the workflow.  0.5 is 50% opacity.
        /// </summary>
        public double IsStepTwoEnabledController
        {
            get => _isStepTwoEnabledController;
            set
            {
                _isStepTwoEnabledController = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsStepTwoEnabledController"));
            }
        }

        /// <summary>
        /// Field representation for the <see cref="IsStepThreeEnabledController"/> bindable property
        /// </summary>
        private double _isStepThreeEnabledController = 0.5;
        /// <summary>
        /// Determines the opacity of the button for step 3 of the workflow.  0.5 is 50% opacity.
        /// </summary>
        public double IsStepThreeEnabledController
        {
            get => _isStepThreeEnabledController;
            set
            {
                _isStepThreeEnabledController = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsStepThreeEnabledController"));
            }
        }
        
        //TODO story board animate the progress bars
        /// <summary>
        /// Field representation for the <see cref="StepTwoProgressController"/> bindable property
        /// </summary>
        private int _stepTwoProgressController = 0;
        /// <summary>
        /// Dictates how far the progress bar is between step 1 and 2.  0 is 0% and 100 is 100%
        /// </summary>
        public int StepTwoProgressController
        {
            get => _stepTwoProgressController;
            set
            {
                _stepTwoProgressController = value;
                OnPropertyChanged(new PropertyChangedEventArgs("StepTwoProgressController"));
            }
        }

        /// <summary>
        /// Field representation for the <see cref="StepThreeProgressController"/> bindable property
        /// </summary>
        private int _stepThreeProgressController = 0;
        /// <summary>
        /// Dictates how far the progress bar is between step 2 and 3.  0 is 0% and 100 is 100%
        /// </summary>
        public int StepThreeProgressController
        {
            get => _stepThreeProgressController;
            set
            {
                _stepThreeProgressController = value;
                OnPropertyChanged(new PropertyChangedEventArgs("StepThreeProgressController"));
            }
        }

        #endregion

        #region Private Variables
        /// <summary>
        /// Variable to track waht state of the workflow the system is in.
        /// </summary>
        private State _currentState = State.Recording;

        #endregion
        
        #region Private Functions
        /// <summary>
        /// Currently unused function.  Bound to a button in the UI.  May be used in the future.
        /// </summary>
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
                    GotoRecordingState();
                    break;
                case State.Saving:
                    GotoEditingState();
                    break;
            }
        }

        /// <summary>
        /// Changes state to recording state.
        /// </summary>
        private void GotoRecordingState()
        {
            // Change state to recording.
            _currentState = State.Recording;

            // Make sure the user cannot go further back in the workflow.
            IsBackwardsEnabled = false;
            
            // Make sure that the two buttons in the workflow have low opacity.
            IsStepTwoEnabledController = 0.5;
            IsStepThreeEnabledController = 0.5;

            // Edit the progress bar accordingly.
            StepTwoProgressController = 0;

            // Display the correct screen in the XAML.
            UserControlManager.SwitchPage(UIPages.DataRecorderView);
            
            //IsRecorderVisible = true;
            //IsEditorVisible = false;
            //IsSavingVisible = false;
        }

        /// <summary>
        /// Changes state to editing state.
        /// </summary>
        private void GotoEditingState()
        {
            // Change state to editing.
            _currentState = State.Editing;

            // Make sure the user can go backwards in the worflow.
            IsBackwardsEnabled = true;

            // Set the correct opacities for the workflow view.
            IsStepTwoEnabledController = 1;
            IsStepThreeEnabledController = 0.5;

            // Set the correct value for the progress bars.
            StepTwoProgressController = 100;
            StepThreeProgressController = 0;

            // Display the correct screen in the XAML.
            UserControlManager.SwitchPage(UIPages.EditLiftView);

            //IsRecorderVisible = false;
            //IsEditorVisible = true;
            //IsSavingVisible = false;
        }

        /// <summary>
        /// Changes state to editing state.
        /// </summary>
        private void GotoSavingState()
        {
            // Change state to saving.
            _currentState = State.Saving;

            // Set the correct value for the progress bars.
            StepThreeProgressController = 100;

            // Set the correct opacities for the workflow view.
            IsStepTwoEnabledController = 1;
            IsStepThreeEnabledController = 1;

            // Display the correct screen in the XAML.
            UserControlManager.SwitchPage(UIPages.SaveLiftView);

            //IsRecorderVisible = false;
            //IsEditorVisible = false;
            //IsSavingVisible = true;
        }

        #endregion 

    }
}

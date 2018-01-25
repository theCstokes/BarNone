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
                MoveWorflowState();
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
        /// Field representation for the <see cref="ControlStepOneCommand"/> bindable command
        /// </summary>
        private RelayCommand _controlStepOneCommand;
        /// <summary>
        /// Bindable command to move workflow to record step (1st)
        /// </summary>
        public ICommand ControlStepOneCommand
        {
            get
            {
                if (_controlStepOneCommand == null)
                {
                    _controlStepOneCommand = new RelayCommand(action => StepOnePressed());
                }
                return _controlStepOneCommand;
            }
        }

        /// <summary>
        /// Field representation for the <see cref="ControlStepTwoCommand"/> bindable command
        /// </summary>
        private RelayCommand _controlStepTwoCommand;
        /// <summary>
        /// Bindable command to move workflow to edit step (2nd)
        /// </summary>
        public ICommand ControlStepTwoCommand
        {
            get
            {
                if(_controlStepTwoCommand == null)
                {
                    _controlStepTwoCommand = new RelayCommand(action => StepTwoPressed());
                }
                return _controlStepTwoCommand;
            }
        }

        /// <summary>
        /// Field representation for the <see cref="ControlStepThreeCommand"/> bindable command
        /// </summary>
        private RelayCommand _controlStepThreeCommand;
        /// <summary>
        /// Bindable command to move workflow to send step (3rd)
        /// </summary>
        public ICommand ControlStepThreeCommand
        {
            get
            {
                if (_controlStepThreeCommand == null)
                {
                    _controlStepThreeCommand = new RelayCommand(action => StepThreePressed());
                }
                return _controlStepThreeCommand;
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
        /// Logout command.Disposes user data and moves back to the login page.
        /// </summary>
        public ICommand LogoutCommand { get; } = new RelayCommand(action => PageManager.SwitchPage(UIPages.LoginView));
        #endregion

        #region Public Properties
        /// <summary>
        /// Field representation for the <see cref="StepTwoOpacityController"/> bindable property
        /// </summary>
        private double _stepTwoOpacityController = 0.5;
        /// <summary>
        /// Determines the opacity of the button for step 2 of the workflow.  0.5 is 50% opacity.
        /// </summary>
        public double StepTwoOpacityController
        {
            get => _stepTwoOpacityController;
            set
            {
                _stepTwoOpacityController = value;
                OnPropertyChanged(new PropertyChangedEventArgs("StepTwoOpacityController"));
            }
        }

        /// <summary>
        /// Field representation for the <see cref="StepThreeOpacityController"/> bindable property
        /// </summary>
        private double _stepThreeOpacityController = 0.5;
        /// <summary>
        /// Determines the opacity of the button for step 3 of the workflow.  0.5 is 50% opacity.
        /// </summary>
        public double StepThreeOpacityController
        {
            get => _stepThreeOpacityController;
            set
            {
                _stepThreeOpacityController = value;
                OnPropertyChanged(new PropertyChangedEventArgs("StepThreeOpacityController"));
            }
        }

        /// <summary>
        /// Field representation for the <see cref="IsStepThreeEnabledController"/> bindable property
        /// </summary>
        private bool _isStepThreeEnabledController = false;
        /// <summary>
        /// Determines whether the button to go to the third step is enabled or not (so you cannot go from record to send).
        /// </summary>
        public bool IsStepThreeEnabledController
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
        /// Variable to track what state of the workflow the system is in.
        /// </summary>
        private State _currentState = State.Recording;

        /// <summary>
        /// Variable to track what state of the workflow the system wants to go to.
        /// </summary>
        private State _stateToMoveTo = State.Recording;

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
        /// Private function that handles when the goto step one button is pressed.
        /// </summary>
        private void StepOnePressed()
        {
            if(_currentState != State.Recording)
            {
                _stateToMoveTo = State.Recording;

                if (RunDialogCommand.CanExecute(null))
                {
                    RunDialogCommand.Execute(null);
                }
            }
        }

        /// <summary>
        /// Private function that handles when the goto step two button is pressed.
        /// </summary>
        private void StepTwoPressed()
        {
            if(_currentState != State.Editing)
            {
                _stateToMoveTo = State.Editing;
                switch (_currentState)
                {
                    case State.Recording:
                        MoveWorflowState();
                        break;
                    case State.Saving:
                        if(RunDialogCommand.CanExecute(null))
                        {
                            RunDialogCommand.Execute(null);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Private function that handles when the goto step three button is pressed.
        /// </summary>
        private void StepThreePressed()
        {
            if(_currentState != State.Saving)
            {
                _stateToMoveTo = State.Saving;
                MoveWorflowState();
            }
        }

        /// <summary>
        /// Moves forward in the Data Lift Workflow.  Changes state to move the workflow along.
        /// </summary>
        private void MoveWorflowState()
        {
            switch (_stateToMoveTo)
            {
                case State.Recording:
                    GotoRecordingState();
                    break;
                case State.Editing:
                    GotoEditingState();
                    break;
                case State.Saving:
                    GotoSavingState();
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
            
            // Disable the button to go to the third step
            IsStepThreeEnabledController = false;

            // Make sure that the two buttons in the workflow have low opacity.
            StepTwoOpacityController = 0.5;
            StepThreeOpacityController = 0.5;

            // Edit the progress bar accordingly.
            StepTwoProgressController = 0;
            StepThreeProgressController = 0;

            // Display the correct screen in the XAML.
            UserControlManager.SwitchPage(UIPages.DataRecorderView);
        }

        /// <summary>
        /// Changes state to editing state.
        /// </summary>
        private void GotoEditingState()
        {
            // Change state to editing.
            _currentState = State.Editing;

            // Enable the button to go to the third step
            IsStepThreeEnabledController = true;

            // Set the correct opacities for the workflow view.
            StepTwoOpacityController = 1;
            StepThreeOpacityController = 0.5;

            // Set the correct value for the progress bars.
            StepTwoProgressController = 100;
            StepThreeProgressController = 0;

            // Display the correct screen in the XAML.
            UserControlManager.SwitchPage(UIPages.EditLiftView);
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
             StepTwoOpacityController = 1;
            StepThreeOpacityController = 1;

            // Display the correct screen in the XAML.
            UserControlManager.SwitchPage(UIPages.SaveLiftView);
        }

        #endregion 

    }
}

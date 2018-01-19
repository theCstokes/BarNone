using BarNone.DataLift.APIRequest;
using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Nav;
using BarNone.Shared.DataTransfer;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarNone.DataLift.UI.ViewModels
{
    /// <summary>
    /// The view model for the Register Screen for the Data Lift application.  Controls functionality for the
    /// data lift register page.
    /// </summary>
    public class RegisterScreenVM : ViewModelBase
    {
        #region Types
        /// <summary>
        /// State of the register page.
        /// </summary>
        private enum RegisterScreenState
        {
            /// <summary>
            /// Enumeration state for no additional view elements are required
            /// </summary>
            NO_STATE,
            /// <summary>
            /// Enumeration state for a request to the server is in progress
            /// </summary>
            SPINNER,
            /// <summary>
            /// Enumeration state for a create user request returned that the username exists
            /// </summary>
            USERNAME_EXISTS_ERROR,
            /// <summary>
            /// Enumeration state for a create user request returned that the provided passwords do not match
            /// </summary>
            PASSWORDS_DO_NOT_MATCH,
            /// <summary>
            /// Enumeration state for a create user request returned that the inputed data is missing some one or more fields
            /// </summary>
            MISSING_FIELD
        }
        #endregion

        #region Bound Properties
        /// <summary>
        /// Field representation for the <see cref="Username"/> bindable property
        /// </summary>
        private static string _username = "";
        /// <summary>
        /// Two way bindable source for the username input on the register page.
        /// </summary>
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Username"));
                }
            }
        }

        /// <summary>
        /// Field representation for the <see cref="Password"/> bindable property
        /// </summary>
        private SecureString _password = new SecureString();
        /// <summary>
        /// Password the user inputs on the register page.
        /// </summary>
        public SecureString Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Password"));
                }
            }
        }

        /// <summary>
        /// Field representation for the <see cref="ConfirmPassword"/> bindable property
        /// </summary>
        private SecureString _confirmPassword = new SecureString();
        /// <summary>
        /// The confrim password (bound to the field) on the regsiter page.
        /// </summary>
        public SecureString ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ConfirmPassword"));
                }
            }
        }

        /// <summary>
        /// Field representation for the <see cref="State"/> bindable property
        /// </summary>
        private RegisterScreenState _state = RegisterScreenState.NO_STATE;
        /// <summary>
        /// The current state of the register page.
        /// </summary>
        private RegisterScreenState State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(new PropertyChangedEventArgs("State"));
                OnPropertyChanged(new PropertyChangedEventArgs("IsProgressBarVisible"));
                OnPropertyChanged(new PropertyChangedEventArgs("ShowUsernameExistMessage"));
                OnPropertyChanged(new PropertyChangedEventArgs("ShowPasswordsMatchMessage"));
                OnPropertyChanged(new PropertyChangedEventArgs("ShowMissingFieldMessage"));
            }
        }

        /// <summary>
        /// Bool dictating whether the progress bar is visible.
        /// </summary>
        public bool IsProgressBarVisible
        {
            get { return State == RegisterScreenState.SPINNER; }
        }

        /// <summary>
        /// Dictates whether the desired username is taken and display the appropriate error message.
        /// </summary>
        public bool ShowUsernameExistMessage
        {
            get { return State == RegisterScreenState.USERNAME_EXISTS_ERROR; }
        }

        /// <summary>
        /// Dictates whether the password and the confirm password the user input does not match and display the approprite error message.
        /// </summary>
        public bool ShowPasswordsMatchMessage
        {
            get { return State == RegisterScreenState.PASSWORDS_DO_NOT_MATCH; }
        }

        /// <summary>
        /// Bool that shows whether a field is missing and show the appropriate error message if it's the case.
        /// </summary>
        public bool ShowMissingFieldMessage
        {
            get { return State == RegisterScreenState.MISSING_FIELD; }
        }

        #endregion

        #region Commands
        /// <summary>
        /// Commmand that takes the user back to the login page.
        /// </summary>
        public ICommand BackCommand { get; } = new RelayCommand(action => PageManager.SwitchPage(UIPages.LoginView));

        /// <summary>
        /// Field representation for the <see cref="RegisterCommand"/> bindable field
        /// </summary>
        private RelayCommand _registerCommand;
        /// <summary>
        /// Bound command that calls the register new user function.
        /// </summary>
        public ICommand RegisterCommand
        {
            get
            {
                if (_registerCommand == null)
                {
                    _registerCommand = new RelayCommand(async action =>
                    {
                        await RegisterNewUser();
                    });
                }
                return _registerCommand;
            }
        }

        /// <summary>
        /// Function that will register a new user and store the new user information on The Rack.
        /// </summary>
        /// <returns></returns>
        public async Task RegisterNewUser()
        {
            // Make sure that there are no empty fields in the register page.  If so, throw the appropriate error.
            if (Username.Length == 0 || Password.Length == 0 || ConfirmPassword.Length == 0)
            {
                State = RegisterScreenState.MISSING_FIELD;
            }
            //  If we are good then ensure that the password and confirm password pages match.
            else if (SecureStringEqual(Password, ConfirmPassword))
            {
                // Start the progress bar.
                State = RegisterScreenState.SPINNER;
                //  Generate a new user profile object and send it to the server.  
                var g = await TokenManager.Create(new UserDTO() { UserName = Username, Password = ConvertSecure(Password) });

                // If saving on the server is sucessful
                if (g.Authorized)
                {
                    // Reset state and move into the control holder state..
                    State = RegisterScreenState.NO_STATE;
                    PageManager.SwitchPage(UIPages.ControlHolderView);
                }
                else
                {
                    // Else assume that the username already exists and dispaly the error to the user.
                    State = RegisterScreenState.USERNAME_EXISTS_ERROR;
                }
            }
            else
            {
                // Else the passwords do not match.  Throw the correct error.
                State = RegisterScreenState.PASSWORDS_DO_NOT_MATCH;
            }

        }

        #endregion

        #region Helpers
        /// <summary>
        /// Method to safely compare two Secure Strings
        /// Taken from https://stackoverflow.com/questions/4502676/c-sharp-compare-two-securestrings-for-equality
        /// </summary>
        private bool SecureStringEqual(SecureString secureString1, SecureString secureString2)
        {
            if (secureString1 == null)
            {
                throw new ArgumentNullException("s1");
            }
            if (secureString2 == null)
            {
                throw new ArgumentNullException("s2");
            }

            if (secureString1.Length != secureString2.Length)
            {
                return false;
            }

            IntPtr ss_bstr1_ptr = IntPtr.Zero;
            IntPtr ss_bstr2_ptr = IntPtr.Zero;

            try
            {
                ss_bstr1_ptr = Marshal.SecureStringToBSTR(secureString1);
                ss_bstr2_ptr = Marshal.SecureStringToBSTR(secureString2);

                String str1 = Marshal.PtrToStringBSTR(ss_bstr1_ptr);
                String str2 = Marshal.PtrToStringBSTR(ss_bstr2_ptr);

                return str1.Equals(str2);
            }
            finally
            {
                if (ss_bstr1_ptr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(ss_bstr1_ptr);
                }

                if (ss_bstr2_ptr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(ss_bstr2_ptr);
                }
            }
        }
        #endregion

    }
}

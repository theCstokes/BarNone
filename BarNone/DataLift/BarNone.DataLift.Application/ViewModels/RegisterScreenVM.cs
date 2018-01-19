using BarNone.DataLift.APIRequest;
using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Nav;
using BarNone.DataLift.UI.ViewModels.Common;
using BarNone.Shared.DataTransfer;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarNone.DataLift.UI.ViewModels
{
    public class RegisterScreenVM : ViewModelBase
    {
        #region Types
        /// <summary>
        /// State of the register page.
        /// </summary>
        private enum RegisterScreenState { NO_STATE, SPINNER, USERNAME_EXISTS_ERROR, PASSWORDS_DO_NOT_MATCH, MISSING_FIELD }
        #endregion

        #region Bound Properties
        /// <summary>
        /// The username the user inputs on the register page.
        /// </summary>
        internal static string _Username = "";
        public string Username
        {
            get => _Username;
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Username"));
                }
            }
        }

        /// <summary>
        /// Password the user inputs on the register page.
        /// </summary>
        private SecureString _Password = new SecureString();
        public SecureString Password
        {
            get => _Password;
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Password"));
                }
            }
        }

        /// <summary>
        /// The confrim password (bound to the field) on the regsiter page.
        /// </summary>
        private SecureString _ConfirmPassword = new SecureString();
        public SecureString ConfirmPassword
        {
            get => _ConfirmPassword;
            set
            {
                if (_ConfirmPassword != value)
                {
                    _ConfirmPassword = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ConfirmPassword"));
                }
            }
        }

        /// <summary>
        /// The current state of the register page.
        /// </summary>
        private RegisterScreenState _state = RegisterScreenState.NO_STATE;
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
        /// Bound command that calls the register new user function.
        /// </summary>
        public RelayCommand _RegisterCommand;

        public ICommand RegisterCommand
        {
            get
            {
                if (_RegisterCommand == null)
                {
                    _RegisterCommand = new RelayCommand(async action =>
                    {
                        await RegisterNewUser();
                    });
                }
                return _RegisterCommand;
            }
        }

        /// <summary>
        /// Function that will register a new user and store the new user information on The Rack.
        /// </summary>
        /// <returns></returns>
        public async Task RegisterNewUser()
        {
            // Make sure that there are no empty fields in the register page.  If so, throw the appropriate error.
            if(Username.Length == 0 || Password.Length == 0 || ConfirmPassword.Length == 0)
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

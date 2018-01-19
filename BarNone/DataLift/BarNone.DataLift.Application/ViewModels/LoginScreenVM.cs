using BarNone.DataLift.APIRequest;
using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Nav;
using System.ComponentModel;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarNone.DataLift.UI.ViewModels
{
    /// <summary>
    /// View Model for the Login Screen
    /// </summary>
    public class LoginScreenVM : ViewModelBase
    {
        #region Types
        /// <summary>
        /// The login screen state enumeration
        /// </summary>
        private enum LoginStates
        {
            /// <summary>
            /// The enumeration state representing no addition draws are required
            /// </summary>
            NoShow = 0,
            /// <summary>
            /// The enumeration state representing a request has been made and is processing 
            /// </summary>
            Spinner = 1,
            /// <summary>
            /// The enumeration state representing a request returned "Not successful"
            /// </summary>
            BadLogin = 2,
            /// <summary>
            /// The enumeration state representing a request returned "Successful"
            /// </summary>
            GoodLogin = 3
        }

        #endregion

        #region Bound Properties
        /// <summary>
        /// Field representation for the <see cref="Username"/> bindable property
        /// </summary>
        internal static string _username = "";
        /// <summary>
        /// Binding property for the user inputted username
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
        /// Binding property for the user inputted password
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
        /// Field representation for the <see cref="LoginState"/> bindable property
        /// </summary>
        private LoginStates _loginState;
        /// <summary>
        /// Holds the current state of the View FSM
        /// </summary>
        private LoginStates LoginState
        {
            get => _loginState;
            set
            {
                _loginState = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoginState"));
                OnPropertyChanged(new PropertyChangedEventArgs("IsBadLogin"));
                OnPropertyChanged(new PropertyChangedEventArgs("IsProgressBarVisible"));
            }
        }

        /// <summary>
        /// Oneway from source bindable property to determine if the login screen is processing a database request
        /// </summary>
        public bool IsProgressBarVisible
        {
            get { return LoginState == LoginStates.Spinner; }
        }

        /// <summary>
        /// Oneway from source bindable property to determine if the login screen is displaying that a bad login attempt was made
        /// </summary>
        public bool IsBadLogin
        {
            get { return LoginState == LoginStates.BadLogin; }
        }

        #endregion

        #region Commands
        /// <summary>
        /// Field representation for the <see cref="LoginCommand"/> bindable command
        /// </summary>
        private RelayCommand _loginCommand;
        /// <summary>
        /// Command binding to produe a login request with entered credentials
        /// </summary>
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(async action => await LoginAsync(), pred => IsLoginStateUserModifiable());
                }
                return _loginCommand;
            }
        }

        /// <summary>
        /// Field representation for the <see cref="RegisterCommand"/> bindable command
        /// </summary>
        private RelayCommand _registerCommand;
        /// <summary>
        /// Command binding to produe a login request with entered credentials
        /// </summary>
        public ICommand RegisterCommand
        {

            get
            {
                if (_registerCommand == null)
                {
                    _registerCommand = new RelayCommand(action => PageManager.SwitchPage(UIPages.RegistrationView), pred => IsLoginStateUserModifiable());
                }
                return _registerCommand;
            }
        }

        /// <summary>
        /// Task to request the auth for the inputted user credentials
        /// </summary>
        /// <returns>Void task</returns>
        private async Task LoginAsync()
        {
            LoginState = LoginStates.Spinner;
            if (await WasLoginSuccessful())
            {
                //Login server calls here to get a valid token and shift to data recorder or notify bad user pass combo
                PageManager.SwitchPage(UIPages.ControlHolderView);
                LoginState = LoginStates.NoShow;
            }
            else
            {
                LoginState = LoginStates.BadLogin;
            }
        }

        /// <summary>
        /// Task for determining if a login is successful
        /// </summary>
        /// <returns>If the login succeeded</returns>
        private async Task<bool> WasLoginSuccessful()
        {
            var r = await TokenManager.Authorize(Username, ConvertSecure(Password));
            return r.Authorized;
        }

        /// <summary>
        /// Internal method for determining if the user should be able to modify information on the Login Screen
        /// </summary>
        /// <returns>If the login screen can be modified by the user at the isntance of call</returns>
        private bool IsLoginStateUserModifiable()
        {
            return LoginState != LoginStates.Spinner;
        }
        #endregion

        #region CleanUp
        /// <summary>
        /// Destructor for the login screen, this ensure the Password is disposed and handled properly 
        /// </summary>
        ~LoginScreenVM()
        {
            Password.Dispose();
        }

        /// <summary>
        /// Method to reset the Login Screen FSM, called on screen load
        /// </summary>
        internal override void Loaded()
        {
            Username = "";
            Password.Clear();
            LoginState = LoginStates.NoShow;
        }

        #endregion
    }
}

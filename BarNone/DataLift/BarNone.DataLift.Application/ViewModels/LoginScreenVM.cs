using BarNone.DataLift.APIRequest;
using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Nav;
using System.ComponentModel;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarNone.DataLift.UI.ViewModels
{
    public class LoginScreenVM : ViewModelBase
    {
        #region Types
        private enum LoginStates
        {
            NoShow = 0, Spinner = 1, BadLogin = 2, GoodLogin = 3
        }

        #endregion

        #region Bound Properties
        internal static string _username = "";
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


        private SecureString _password = new SecureString();
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
        
        private LoginStates _loginState;
        
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

        public bool IsProgressBarVisible {
            get { return LoginState == LoginStates.Spinner; }
        }
        
        public bool IsBadLogin
        {
            get { return LoginState == LoginStates.BadLogin; }
        }

        #endregion

        #region Commands
        private RelayCommand _loginCommand;

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

        private bool IsLoginStateUserModifiable()
        {
            return LoginState != LoginStates.Spinner; 
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(action => PageManager.SwitchPage(UIPages.RegistrationView), pred => IsLoginStateUserModifiable());
            }
        }
        
        private async Task LoginAsync()
        {
            LoginState = LoginStates.Spinner;
            if (await CanLogin())
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

        private async Task<bool> CanLogin()
        {
            var r = await TokenManager.Authorize(Username, ConvertSecure(Password));
            return r.Authorized;
        }

        #endregion
        
        #region CleanUp
        //Destructor to ensure the password is removed from the system
        ~LoginScreenVM()
        {
            Password.Dispose();
        }

        internal override void Loaded()
        {
            Username = "";
            Password.Clear();
            LoginState = LoginStates.NoShow;
        }

        #endregion
    }
}

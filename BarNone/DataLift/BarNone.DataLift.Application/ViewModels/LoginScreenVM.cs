using BarNone.DataLift.APIRequest;
using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Nav;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarNone.DataLift.UI.ViewModels
{
    public class LoginScreenVM : ViewModelBase
    {
        #region Bound Properties
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


        enum LoginStates
        {
            NoShow = 0, Spinner = 1, BadLogin = 2, GoodLogin = 3
        }

        private int _LoginStateDisplayIndex = (int)LoginStates.NoShow;

        public int LoginStateDisplayIndex
        {
            get => _LoginStateDisplayIndex;
            set
            {
                if(_LoginStateDisplayIndex != value && value >= 0 && value <= 3)
                {
                    _LoginStateDisplayIndex = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LoginStateDisplayIndex"));
                } 

            }
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
            return LoginStateDisplayIndex != (int)LoginStates.Spinner; 
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
            LoginStateDisplayIndex = (int)LoginStates.NoShow;
            LoginStateDisplayIndex = (int)LoginStates.Spinner;
            if (await CanLogin())
            {
                //Login server calls here to get a valid token and shift to data recorder or notify bad user pass combo
                PageManager.SwitchPage(UIPages.DataRecorderView);
                LoginStateDisplayIndex = (int)LoginStates.NoShow;
            }
            else
            {
                LoginStateDisplayIndex = (int)LoginStates.BadLogin;
            }
        }

        private async Task<bool> CanLogin()
        {
            var r = await TokenManager.Authorize(Username, ConvertSecure(Password));
            return r.Authorized;

            //return r.Authorized;
            //return ((!string.IsNullOrWhiteSpace(Username)) && (Password?.Length > 0));
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
            //LoginStateDisplayIndex = (int)LoginStates.NoShow;

        }

        #endregion
    }
}

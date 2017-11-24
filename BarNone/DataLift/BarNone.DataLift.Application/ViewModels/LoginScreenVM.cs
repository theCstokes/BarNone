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

        #endregion

        #region Commands
        private RelayCommand _loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(async action => await LoginAsync());
                }
                return _loginCommand;
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(action => PageManager.SwitchPage(UIPages.RegistrationView));
            }
        }



        private async Task LoginAsync()
        {
            if (await CanLogin())
                //Login server calls here to get a valid token and shift to data recorder or notify bad user pass combo
                PageManager.SwitchPage(UIPages.DataRecorderView);
        }

        private async Task<bool> CanLogin()
        {
            var r = await TokenManager.Authorize(Username, ConvertSecure(Password));
            return r.Authorized;

            //return r.Authorized;
            //return ((!string.IsNullOrWhiteSpace(Username)) && (Password?.Length > 0));
        }

        #endregion

        #region Helpers
        static string ConvertSecure(SecureString value)
        {
            IntPtr bstr = Marshal.SecureStringToBSTR(value);

            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
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
            Password = new SecureString();

        }

        #endregion
    }
}

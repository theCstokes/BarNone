using BarNone.DataLift.UI.Commands;
using System.Windows.Input;
using System.Security;
using BarNone.DataLift.UI.Nav;
using System.Threading.Tasks;
using BarNone.DataLift.APIRequest;
using System.Runtime.InteropServices;
using System;

namespace BarNone.DataLift.UI.ViewModels
{
    class LoginScreenVM : ViewModelBase
    {
        public static string Username { get; set; }
        public SecureString Password { private get; set; }

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
                return new RelayCommand(action => Register());
            }
        }


        private async Task LoginAsync()
        {
            if(await CanLogin())
            //Login server calls here to get a valid token and shift to data recorder or notify bad user pass combo
                PageManager.SwitchPage(UIPages.DataRecorderView);
        }

        private void Register()
        {
            PageManager.SwitchPage(UIPages.RegistrationView);
        }
        
        private async Task<bool> CanLogin()
        {
            var r = await TokenManager.Authorize(Username, ConvertSecure(Password));
            return r.Authorized;

            //return r.Authorized;
            //return ((!string.IsNullOrWhiteSpace(Username)) && (Password?.Length > 0));
        }

        //Destructor to ensure the password is removed from the system
        ~LoginScreenVM()
        {
            Password.Dispose();
        }

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
    }
}

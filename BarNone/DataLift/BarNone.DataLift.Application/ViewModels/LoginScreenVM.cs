using BarNone.DataLift.UI.Commands;
using System.Windows.Input;
using System.Security;
using BarNone.DataLift.UI.Nav;
using BarNone.DataLift.APIRequest;
using System.Threading.Tasks;

namespace BarNone.DataLift.UI.ViewModels
{
    class LoginScreenVM : ViewModelBase
    {
        public string Username { get; set; }
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
            var r = await TokenManager.Authorize(Username, Password.ToString());
            return r.Authorized;

            //return r.Authorized;
            //return ((!string.IsNullOrWhiteSpace(Username)) && (Password?.Length > 0));
        }

        //Destructor to ensure the password is removed from the system
        ~LoginScreenVM()
        {
            Password.Dispose();
        }

    }
}

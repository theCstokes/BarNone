using DataLift.Commands;
using System.Windows.Input;
using System.Security;
using DataLift.Nav;

namespace DataLift.ViewModels
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
                    _loginCommand = new RelayCommand(action => Login(), pred => CanLogin());
                }
                return _loginCommand;
            }
        }


        private void Login()
        {
            //Login server calls here to get a valid token and shift to data recorder or notify bad user pass combo
            PageManager.SwitchPage(UIPages.DataRecorderView);
        }
        
        private bool CanLogin()
        {
            return ((!string.IsNullOrWhiteSpace(Username)) && (Password?.Length > 0));
        }

        //Destructor to ensure the password is removed from the system
        ~LoginScreenVM()
        {
            Password.Dispose();
        }

    }
}

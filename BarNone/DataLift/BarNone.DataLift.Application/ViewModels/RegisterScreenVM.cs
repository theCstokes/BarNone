using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Security;
using BarNone.DataLift.APIRequest;
using BarNone.Shared.DataTransfer;

namespace BarNone.DataLift.UI.ViewModels
{
    public class RegisterScreenVM : ViewModelBase
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


        #endregion

        #region Commands
        public ICommand BackCommand { get; } = new RelayCommand(action => PageManager.SwitchPage(UIPages.LoginView));

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

        public async Task RegisterNewUser()
        {
            var g = await TokenManager.Create(new UserDTO() { UserName = Username, Password = ConvertSecure(Password) });
            if (g.Authorized)
                PageManager.SwitchPage(UIPages.DataRecorderView);
            
        }


        #endregion

    }
}

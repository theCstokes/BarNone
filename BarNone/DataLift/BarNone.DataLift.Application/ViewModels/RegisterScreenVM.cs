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
    public class RegisterScreenVM : ViewModelBase
    {
        #region
        private enum RegisterScreenState { NO_STATE, SPINNER, USERNAME_EXISTS_ERROR, PASSWORDS_DO_NOT_MATCH, MISSING_FIELD }
        #endregion

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

        public bool IsProgressBarVisible
        {
            get { return State == RegisterScreenState.SPINNER; }
        }

        public bool ShowUsernameExistMessage
        {
            get { return State == RegisterScreenState.USERNAME_EXISTS_ERROR; }
        }

        public bool ShowPasswordsMatchMessage
        {
            get { return State == RegisterScreenState.PASSWORDS_DO_NOT_MATCH; }
        }

        public bool ShowMissingFieldMessage
        {
            get { return State == RegisterScreenState.MISSING_FIELD; }
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
            if(Username.Length == 0 || Password.Length == 0 || ConfirmPassword.Length == 0)
            {
                State = RegisterScreenState.MISSING_FIELD;
            }
            else if (SecureStringEqual(Password, ConfirmPassword))
            {
                State = RegisterScreenState.SPINNER;
                var g = await TokenManager.Create(new UserDTO() { UserName = Username, Password = ConvertSecure(Password) });
                if (g.Authorized)
                {
                    State = RegisterScreenState.NO_STATE;
                    PageManager.SwitchPage(UIPages.DataRecorderView);
                }
                else
                {
                    State = RegisterScreenState.USERNAME_EXISTS_ERROR;
                }
            }
            else
            {
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

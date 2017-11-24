using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.Nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace BarNone.DataLift.UI.ViewModels
{
    public class RegisterScreenVM : ViewModelBase
    {
        public string Username { get; set; }

        public ICommand BackCommand { get; } = new RelayCommand(action => PageManager.SwitchPage(UIPages.LoginView));

        public ICommand RegisterCommand { get; } = new RelayCommand(action => System.Diagnostics.Debug.WriteLine("Not Implemented Register Command!"));
        
    }
}

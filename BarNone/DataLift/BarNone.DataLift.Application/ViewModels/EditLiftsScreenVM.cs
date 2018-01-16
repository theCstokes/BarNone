using BarNone.DataLift.UI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarNone.DataLift.UI.ViewModels
{
    public class EditLiftsScreenVM : ViewModelBase
    {
        #region Video Control Commands
        public RelayCommand _commandPlayVideo;
        public ICommand CommandPlayVideo { get => _commandPlayVideo; set { Console.WriteLine("Play Clicked, Not Implemented"); } }
        #endregion
    }
}

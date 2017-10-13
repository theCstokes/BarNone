using DataLift.Nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataLift.Commands
{
    class SwitchPageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object page)
        {
            if(page is UIPages)
            {
                PageManager.SwitchPage((UIPages)page);
            }
            else
            {
                throw new InvalidCastException("Specified object was not a UIPages object, cannot navigate to it!");
            }
        }
    }
}

using BarNone.DataLift.UI.Nav;
using System.ComponentModel;

namespace BarNone.DataLift.UI.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


        internal virtual void Loaded()
        {
            //Do Nothing by default
        }

        internal virtual void Closed()
        {
            //Do Nothing by default
        }

    }
}

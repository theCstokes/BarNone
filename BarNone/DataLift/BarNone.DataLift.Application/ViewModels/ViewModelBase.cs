using BarNone.DataLift.UI.Nav;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

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

        #region Helpers
        protected static string ConvertSecure(SecureString value)
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

    }
}

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace BarNone.DataLift.UI.ViewModels
{
    /// <summary>
    /// Base object which each view model extents
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Event which is fired when a property changes in the view model
        /// </summary>
        /// <see cref="INotifyPropertyChanged"/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Explicit property changed event
        /// </summary>
        /// <see cref="INotifyPropertyChanged"/>
        /// <param name="e">Property changed event being fired</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Function to be called when a bound UI loads a base view model instance
        /// </summary>
        internal virtual void Loaded()
        {
            //Do Nothing by default
        }

        /// <summary>
        /// Function to be called when a bound UI is cloased or navigated away from a base view model instance
        /// </summary>
        internal virtual void Closed()
        {
            //Do Nothing by default
        }

        #region Helpers
        /// <summary>
        /// Method to translate held SecureStrings to a plain text representation
        /// </summary>
        /// <param name="value">Secure string to convert</param>
        /// <returns>Secure string as string</returns>
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

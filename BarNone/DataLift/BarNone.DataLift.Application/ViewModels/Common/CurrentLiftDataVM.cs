using BarNone.Shared.DataTransfer;
using BarNone.DataLift.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.UI.ViewModels.Common
{
    public class CurrentLiftDataVM : ViewModelBase
    {
        //If we need to get item modified ask me -Riley
        private ObservableCollection<LiftDTO> _currentRecordedData = new ObservableCollection<LiftDTO>();
        public ObservableCollection<LiftDTO> CurrentRecordedData
        {
            get => _currentRecordedData;
            set
            {
                _currentRecordedData = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentRecordedData"));
            }
        }
    }

    #region Singleton Class
    /// <summary>
    /// Creates a singleton view model for shared lift information to prevent obsurd bindings
    /// </summary>
    public static class CurrentLiftDataVMSingleton
    {
        /// <summary>
        /// CurrentLiftDataVM singleton
        /// </summary>
        private static CurrentLiftDataVM vm;

        /// <summary>
        /// Gets the singleton instance of CurrentLiftDataVM
        /// </summary>
        /// <returns>Singleton instance of CurrentLiftDataVM</returns>
        public static CurrentLiftDataVM GetInstance()
        {
            if (vm == null)
            {
                vm = new CurrentLiftDataVM();
            }
            return vm;
        }

    }
    #endregion

}

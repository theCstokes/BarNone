using BarNone.Shared.DataTransfer;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace BarNone.DataLift.UI.ViewModels.Common
{
    /// <summary>
    /// Shared View Model for all Views that require a refference to the current sessions recorded data
    /// </summary>
    public class CurrentLiftDataVM : ViewModelBase
    {
        //If we need to get item modified ask me -Riley
        /// <summary>
        /// Field representation for the <see cref="CurrentRecordedData"/> bindable property list
        /// </summary>
        private ObservableCollection<LiftDTO> _currentRecordedData = new ObservableCollection<LiftDTO>();
        /// <summary>
        /// Binding property for the currently recorded data. This data represents all recorded data from start recording to stop recorrding.
        /// </summary>
        public ObservableCollection<LiftDTO> CurrentRecordedData
        {
            get => _currentRecordedData;
            set
            {
                _currentRecordedData = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentRecordedData"));
            }
        }
#if DEBUG
        /// <summary>
        /// Constructor for CurrentLiftDataVM which initializes test data into the system for UI validation
        /// </summary>
        public CurrentLiftDataVM()
        {
            CurrentRecordedData.Add(JsonConvert.DeserializeObject<LiftDTO>(File.ReadAllText(@"C:\Users\Aamir\Documents\McMaster\Year_4\Capstone\barnone\BarNone\DataLift\BarNone.DataLift.Application\bin\Debug\SQUAT_2018_01_13.json")));
        }
    }
#endif
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

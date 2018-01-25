using BarNone.DataLift.UI.Models;
using BarNone.Shared.Core;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private ObservableCollection<BodyDataFrame> _currentRecordedData = new ObservableCollection<BodyDataFrame>();
        /// <summary>
        /// Binding property for the currently recorded data. This data represents all recorded data from start recording to stop recorrding.
        /// </summary>
        public ObservableCollection<BodyDataFrame> CurrentRecordedData
        {
            get => _currentRecordedData;
            set
            {
                _currentRecordedData = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentRecordedData"));
            }
        }

        private ObservableCollection<ColorImageFrame> _currentRecordedColorData = new ObservableCollection<ColorImageFrame>();
        public ObservableCollection<ColorImageFrame> CurrentRecordedColorData
        {
            get => _currentRecordedColorData;
            set
            {
                _currentRecordedColorData = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentRecordedColorData"));
            }
        }

        private bool isNormalized = false;
        /// <summary>
        /// Data may only be normalized once currently!
        /// </summary>
        public void NormalizeTimes()
        {
            if (isNormalized)
                return;
            if(CurrentRecordedColorData.Count > 0 && CurrentRecordedData.Count > 0)
            {
                TimeSpan bodyCandidate = CurrentRecordedData[0].TimeOfFrame, colorCandidate = CurrentRecordedColorData[0].Time;

                TimeSpan candidate = (colorCandidate < bodyCandidate) ? colorCandidate : bodyCandidate;
                
                CurrentRecordedData.ToList().ForEach(x => x.TimeOfFrame = x.TimeOfFrame.Subtract(candidate));
                CurrentRecordedColorData.ToList().ForEach(x => x.Time = x.Time.Subtract(candidate));

            }
            else
            {
                throw new Exception("Cannot Normalize, No Data!");
            }
        }

#if DEBUG
        /// <summary>
        /// Constructor for CurrentLiftDataVM which initializes test data into the system for UI validation
        /// </summary>
        public CurrentLiftDataVM()
        {
            //var lift = Converters.NewConvertion(new DebugContext()).Lift.CreateDataModel(JsonConvert.DeserializeObject<LiftDTO>(File.ReadAllText(Path.GetFullPath(@"./res/Squat_Debug.json"))));
            //lift.BodyData.BodyDataFrames.ForEach(f => f.Joints.ForEach(j => { j.JointType = new JointType((EJointType)j.JointTypeID); j.JointTrackingStateType = new JointTrackingStateType((EJointTrackingStateType)j.JointTrackingStateTypeID); }));
            //lift.BodyData.BodyDataFrames.ForEach(f => CurrentRecordedData.Add(f));
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

#if DEBUG
    /// <summary>
    /// Used to force debug code to have rack provided feilds
    /// </summary>
    public class DebugContext : IDomainContext
    {
        public int UserID { get => 0; set { return; } }
    }
#endif

}

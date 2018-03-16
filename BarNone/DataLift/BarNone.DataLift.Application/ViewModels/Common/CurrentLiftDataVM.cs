using BarNone.Shared.Core;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace BarNone.DataLift.UI.ViewModels.Common
{
    /// <summary>
    /// Shared View Model for all Views that require a refference to the current sessions recorded data
    /// </summary>
    public class CurrentLiftDataVM : ViewModelBase
    {
        //If we need to get item modified ask me -Riley
        /// <summary>
        /// Field representation for the <see cref="CurrentRecordedBodyData"/> bindable property list
        /// </summary>
        private ObservableCollection<BodyDataFrame> _currentRecordedData = new ObservableCollection<BodyDataFrame>();

        public double DataLength()
        {
            double tot = 0.0;
            for(int i = 0; i < _currentRecordedData.Count - 1; i++ )
            {
                tot += (_currentRecordedData[i+1].TimeOfFrame - _currentRecordedData[i].TimeOfFrame).TotalMilliseconds;
            }
            return tot;
        }

        /// <summary>
        /// Binding property for the currently recorded data. This data represents all recorded data from start recording to stop recorrding.
        /// </summary>
        public ObservableCollection<BodyDataFrame> CurrentRecordedBodyData
        {
            get => _currentRecordedData;
            set
            {
                _currentRecordedData = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentRecordedData"));
            }
        }

        private DateTime? _firstColorDataFrame = null;
        public DateTime? FirstColorDataFrame
        {
            get => _firstColorDataFrame;
            set
            {
                _firstColorDataFrame = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FirstColorDataFrame"));
            }
        }

        public long ColorDataOffset;

        private TimeSpan? _latencyBetweenFirstColorFrameAndFirstBodyFrame = null;
        public TimeSpan? LatencyBetweenFirstColorFrameAndFirstBodyFrame
        {
            get => _latencyBetweenFirstColorFrameAndFirstBodyFrame;
            set
            {
                _latencyBetweenFirstColorFrameAndFirstBodyFrame = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LatencyBetweenFirstColorFrameAndFirstBodyFrame"));
            }
        }

        public ObservableCollection<LiftListVM> LiftInformation = new ObservableCollection<LiftListVM>();

        private bool isNormalized = false;

        /// <summary>
        /// Data may only be normalized once currently!
        /// </summary>
        public void NormalizeTimes()
        {
            if (isNormalized || CurrentRecordedBodyData == null)
                return;
            if (CurrentRecordedBodyData.Count > 0)
            {
                //DateTime normalizatioTime = FirstColorDataFrame.Value;
                var first = CurrentRecordedBodyData[0].TimeOfFrame;
                //var m = DateTime.Now.AddMilliseconds(-Environment.TickCount + bodyCandidate.Ticks / 10000);
                // is the difference m - FirstColorDataFrame.Value;
                //TimeSpan candidate = (colorCandidate < bodyCandidate) ? colorCandidate : bodyCandidate;
                CurrentRecordedBodyData.ToList()
                    .ForEach(x => x.TimeOfFrame = x.TimeOfFrame.Add(new TimeSpan(ColorDataOffset*10000)) - first);
            }
            else
            {
                throw new ArgumentException("Data recorded cannot be normailized, missing content!");
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

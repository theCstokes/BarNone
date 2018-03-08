using System.Collections.Generic;
using System.ComponentModel;

namespace BarNone.DataLift.UI.ViewModels
{
    /// <summary>
    /// The view model for each item containted in the list of lifts in the edit screen of Data Lift.  Holds all
    /// values bound to the ListView and an ObservableCollection of these VMs will be bound to the ListView.
    /// </summary>
    public class LiftListVM : ViewModelBase
    {
        /// <summary>
        /// Corresponds to the index in the <see cref="EditLiftsScreenVM.LiftIntervals"/>
        /// </summary>
        public int Count;

        /// <summary>
        /// Field representation for the <see cref="LiftName"/> bindable property
        /// </summary>
        private string _liftName;
        /// <summary>
        /// The name of the lift.  User editable.
        /// </summary>
        public string LiftName
        {
            get { return _liftName; }

            set
            {
                if (_liftName == value) return;

                _liftName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LiftName"));
            }
        }

        /// <summary>
        /// Field representation for the <see cref="LiftType"/> bindable property list
        /// </summary>
        private string _liftType;
        /// <summary>
        /// The type  of lift; squat, clean etc.
        /// </summary>
        public string LiftType
        {
            get { return _liftType; }

            set
            {
                if (_liftType == value) return;

                _liftType = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LiftType"));
            }
        }
        
        /// <summary>
        /// Field representation for the <see cref="LiftName"/> bindable property
        /// </summary>
        private double _liftStartTime;
        /// <summary>
        /// The time the lift has started.
        /// </summary>
        public double LiftStartTime
        {
            get { return _liftStartTime; }

            set
            {
                if (_liftStartTime == value) return;

                _liftStartTime = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LiftStartTime"));
            }
        }

        /// <summary>
        /// Field representation for the <see cref="LiftEndTime"/> bindable property
        /// </summary>
        private double _liftEndTime;
        /// <summary>
        /// The time the lift has ended.
        /// </summary>
        public double LiftEndTime
        {
            get { return _liftEndTime; }

            set
            {
                if (_liftEndTime == value) return;

                _liftEndTime = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LiftEndTime"));
            }
        }

        /// <summary>
        /// Field representation for the <see cref="LiftTypeList"/> bindable property
        /// </summary>
        private readonly List<string> _liftTypeList = new List<string>() { "Squat", "Snatch", "Clean", "Clean and Jerk", "Other" };
        /// <summary>
        /// List that dictates the drop down for the list of lifts.  
        /// </summary>
        public List<string> LiftTypeList
        {
            get => _liftTypeList;
        }

        //TODO make this list reference somewhere from shared, so we only need to maintain it in one place.
    }
}

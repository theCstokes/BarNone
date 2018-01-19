using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.UI.ViewModels
{
    public class LiftListVM : ViewModelBase
    {
        // Corresponds to the index in the ObservableCollection
        public int Count;

        /// <summary>
        /// The name of the lift.  User editable.
        /// </summary>
        private string _liftName;
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
        /// The type  of lift; squat, clean etc.
        /// </summary>
        private string _liftType;
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
        /// The time the lift has started.
        /// </summary>
        private string _liftStartTime;
        public string LiftStartTime
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
        /// The time the lift has ended.
        /// </summary>
        private string _liftEndTime;
        public string LiftEndTime
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
        /// List that dictates the drop down for the list of lifts.  
        /// </summary>
        private List<string> _liftTypeList;
        public List<string> LiftTypeList
        {
            get { return new List<string>() { "Squat", "Snatch", "Clean", "Clean and Jerk", "Other" }; }

            private set
            {
                _liftTypeList = value;
            }
        }

        //TODO make this list reference somewhere from shared, so we only need to maintain it in one place.
    }
}

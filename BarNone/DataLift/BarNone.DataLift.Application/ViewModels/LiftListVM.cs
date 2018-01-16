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

        private ObservableCollection<string> _liftTypeList;
        public ObservableCollection<string> LiftTypeList
        {
            get { return new ObservableCollection<string>() { "Squat", "Snatch", "Clean", "Clean and Jerk", }; }

            private set
            {
                _liftTypeList = value;
            }
        }
    }
}

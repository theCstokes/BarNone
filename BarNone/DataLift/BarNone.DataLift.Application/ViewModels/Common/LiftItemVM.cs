using BarNone.DataLift.APIRequest;
using BarNone.DataLift.UI.ViewModels.Common;
using BarNone.Shared.DataTransfer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace BarNone.DataLift.UI.ViewModels.Common
{
    /// <summary>
    /// The view model for each item containted in the list of lifts in the edit screen of Data Lift.  Holds all
    /// values bound to the ListView and an ObservableCollection of these VMs will be bound to the ListView.
    /// </summary>
    public class LiftItemVM : ViewModelBase
    {
        /// <summary>
        /// Corresponds to the index in the <see cref="EditLiftsScreenVM.LiftIntervals"/>
        /// </summary>
        public int Count;

        public int UserID;

        public int LiftID
        {
            get { return _liftTypeDictionary[LiftType]; }
        }

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

        private string _liftFolder;
        /// <summary>
        /// The type  of lift; squat, clean etc.
        /// </summary>
        public string LiftFolder
        {
            get { return _liftFolder; }

            set
            {
                if (_liftFolder == value) return;

                _liftFolder = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LiftFolder"));
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
        /// 

        private Dictionary<string, int> _liftTypeDictionary = new Dictionary<string, int>(); // = new List<string>() { "Squat", "Snatch", "Clean", "Clean and Jerk", "Other" };
        /// <summary>
        /// List that dictates the drop down for the list of lifts.  
        /// </summary>
        public List<string> LiftTypeList
        {
            get { return new List<string>(_liftTypeDictionary.Keys); }
        }
        
        private List<string> _liftFolderList = new List<string>();

        public List<string> LiftFolderList
        {
            get { return _liftFolderList; }

            set
            {
                if (_liftFolderList == value) return;

                _liftFolderList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LiftFolderList"));
            }
        }

        public ObservableCollection<SharableUser> SharedUsers;

        public LiftItemVM(string currentUser)
        {
            List<UserDTO> allUserDTOs = new List<UserDTO>();
            List<LiftTypeDTO> liftDTOs = new List<LiftTypeDTO>();
            List<LiftFolderDTO> folderDTOs = new List<LiftFolderDTO>();

            App.Current.Dispatcher.Invoke(async () =>
            {
                allUserDTOs = await DataManager.Users.GetAll();

                SharedUsers = new ObservableCollection<SharableUser>(allUserDTOs.Select(u =>
                new SharableUser
                {
                    UserName = u.UserName,
                    IsSeletcted = false,
                    Name = u.Name,
                    ID = u.ID
                }).ToList());

                //OnPropertyChanged(new PropertyChangedEventArgs("DisplayedUsers"));

                liftDTOs = await DataManager.Types.GetAll();
                _liftTypeDictionary = liftDTOs.ToDictionary(k => k.Name, v => v.ID);

                folderDTOs = await DataManager.Folders.GetAll();
                _liftFolderList = folderDTOs.Select(u => u.Name).ToList();

                UserID = (allUserDTOs.ToDictionary(k => k.UserName, v => v.ID))[currentUser];
            });
        }

    }
}

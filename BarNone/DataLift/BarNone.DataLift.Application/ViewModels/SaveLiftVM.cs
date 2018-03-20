using BarNone.DataLift.APIRequest;
using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.ViewModels.Common;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer.Flex;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarNone.DataLift.UI.ViewModels
{
    class SaveLiftVM : ViewModelBase
    {
        #region Common Data
        public CurrentLiftDataVM CurrentLiftData = CurrentLiftDataVMSingleton.GetInstance();
        #endregion

        #region TEMP DATA FOR TESTING

        public ObservableCollection<SharableUser> Users
        {
            get
            {
                if(SelectedLift?.SharedUsers == null)
                {
                    return new ObservableCollection<SharableUser>();
                }
                else
                {
                    return SelectedLift.SharedUsers;
                }
                
            }
        }
        #endregion

        #region Lift ListView Properties

        private CurrentLiftDataVM _currentLifts = CurrentLiftDataVMSingleton.GetInstance();
        /// <summary>
        /// Shared viewmodel reference which holds currently recorded data consistently between VM's
        /// </summary>
        public CurrentLiftDataVM CurrentLifts
        {
            get => _currentLifts;
            set
            {
                _currentLifts = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentLifts"));
            }
        }

        public ObservableCollection<LiftItemVM> LiftIntervals
        {
            get
            {
                return CurrentLifts.LiftInformation;
            }
            set
            {
                if (CurrentLifts.LiftInformation == value) return;

                CurrentLifts.LiftInformation = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LiftIntervals"));
            }
        }


        /// <summary>
        /// Field representation for the <see cref="SelectedLift"/> bindable property
        /// </summary>
        private LiftItemVM _selectedLift;
        /// <summary>
        /// The lift that is selected in the lift.  Used to bind to video player to display the selected lift.
        /// </summary>
        public LiftItemVM SelectedLift
        {
            get { return _selectedLift; }

            set
            {
                if (_selectedLift == value) return;

                _selectedLift = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedLift"));
                OnPropertyChanged(new PropertyChangedEventArgs("DisplayedUsers"));
            }
        }
        #endregion

        #region Lift ListView Commands
        /// <summary>
        /// Field representation for the <see cref="DeleteSelectedRecording"/> bindable property
        /// </summary>
        private RelayCommand _deleteSelectedRecording;
        /// <summary>
        /// Command that calls the function to delete lifts from the ListView.
        /// </summary>
        public ICommand DeleteSelectedRecording
        {
            get
            {
                if (_deleteSelectedRecording == null)
                {
                    _deleteSelectedRecording = new RelayCommand(action => DeleteSelectedRecordingCommand(action));
                }
                return _deleteSelectedRecording;
            }
        }

        /// <summary>
        /// Delete function for the ListView.  Removed the selected lift from the ObservableCollection.
        /// </summary>
        /// <param name="action">The object in the ObservableCollection that called Delete.</param>
        private void DeleteSelectedRecordingCommand(object action)
        {
            // Cast action to a LiftListVM.
            LiftItemVM selected = (LiftItemVM)action;

            // If action is null then return
            if (action == null) return;

            // Remove the correct lift and redo count for all the remaining lifts in ListView.
            LiftIntervals.RemoveAt(selected.Count);
            for (int i = 0; i < LiftIntervals.Count; i++) LiftIntervals[i].Count = i;
        }
        #endregion

        #region DisplayableShareUserProperties

        private string _searchString = "";

        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SearchString"));
                OnPropertyChanged(new PropertyChangedEventArgs("DisplayedUsers"));

            }
        }

        public ObservableCollection<SharableUser> DisplayedUsers
        {
            get
            {
                if (SearchString == "")
                {
                    if (Users == null)
                    {
                        return new ObservableCollection<SharableUser>();
                    }
                    else
                    {
                        return new ObservableCollection<SharableUser>(Users.OrderBy(u => u.Code).ToList());
                    }
                }
                else
                {
                    //return new ObservableCollection<SharableUser>
                    //    (Users.Where(u => u.UserName.ToLower().Contains(SearchString.ToLower())).ToList());

                    ObservableCollection<SharableUser> toBeDisplayed = new ObservableCollection<SharableUser>();
                    //ObservableCollection<SharableUser> selectedUsers = new ObservableCollection<SharableUser>();

                    foreach (SharableUser user in Users)
                    {
                        if (user.UserName.ToLower().Contains(SearchString.ToLower()) ||
                            user.Name.ToLower().Contains(SearchString.ToLower()) || user.IsSeletcted)
                        {

                            //if(user.IsSeletcted)
                            //{
                            //    selectedUsers.Add(user);
                            //}
                            //else
                            //{
                            toBeDisplayed.Add(user);
                            //}

                        }
                    }

                    //return new ObservableCollection<SharableUser>(selectedUsers.OrderByDescending(u => u.IsSeletcted).ToList()
                    //    .Concat(toBeDisplayed.OrderBy(u => u.Code).ToList()));

                    return new ObservableCollection<SharableUser>(toBeDisplayed.OrderBy(u => u.Code).ToList());

                }
            }
            set
            {
                OnPropertyChanged(new PropertyChangedEventArgs("DisplayedUsers"));
            }
        }
        #endregion

        #region UserSharePropeties
        private ObservableCollection<User> _selectedUsers;
        public ObservableCollection<User> SelectedUsers
        {
            get
            {
                return _selectedUsers;
            }
            set
            {
                _selectedUsers = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedUsers"));
            }
        }
        #endregion

        #region Public Commands
        /// <summary>
        /// Field representation for the <see cref="StartRecording"/> bindable command
        /// </summary>
        private RelayCommand _sendLifts;
        /// <summary>
        /// Bindable command send recordings to lift.
        /// </summary>
        public ICommand SendLifts
        {
            get
            {
                if (_sendLifts == null)
                {
                    _sendLifts = new RelayCommand(action => SendLiftCommand());
                }
                return _sendLifts;
            }
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Private function that handles sending lifts to The Rack
        /// </summary>
        private async void SendLiftCommand()
        {
            //TODO producer consumer this
            var PostTasks = new List<Task>();
            var ffmpeg = new FfmpegController();
            foreach(var lift in CurrentLiftData.LiftInformation)
            {
                var firstFrame = CurrentLiftData.CurrentRecordedBodyData.FirstOrDefault(f => f.TimeOfFrame.TotalMilliseconds >= lift.LiftStartTime);
                if (firstFrame == null)
                    continue;

                PostTasks.Add(Task.Run(async () => {
                    var liftDTO = Converters.NewConvertion()
                    .Lift
                    .CreateDTO(new Lift()
                    {
                        LiftTypeID = lift.LiftID,
                        BodyData = new BodyData
                        {
                            BodyDataFrames = CurrentLiftData.CurrentRecordedBodyData
                                .Where(f => f.TimeOfFrame.TotalMilliseconds >= lift.LiftStartTime && f.TimeOfFrame.TotalMilliseconds<= lift.LiftEndTime)
                                //Clone the frame to normalize start times!
                                .Select(f => new BodyDataFrame()
                                {
                                    UserID = lift.UserID,
                                    Joints = f.Joints,
                                    TimeOfFrame = f.TimeOfFrame - firstFrame.TimeOfFrame
                                })
                                .OrderBy(f => f.TimeOfFrame.TotalMilliseconds)
                                .ToList(),
                            UserID = lift.UserID,
                            RecordDate = DateTime.Now
                        },
                        Name = lift.LiftName, // TODO. Not make this a hardcoded 0.
                        UserID = lift.UserID,
                        Video = new VideoRecord
                        {
                            Data = File.ReadAllBytes(await ffmpeg.SplitVideo(CurrentLiftData.ParentLiftVideoName, lift.LiftStartTime/1000, (lift.LiftEndTime-lift.LiftStartTime)/1000)),
                            UserID = lift.UserID
                        }
                    });

                    var temp = await DataManager.Flex.Post(new FlexDTO
                    {
                        Entities = new List<FlexEntityDTO>
                        {
                            new FlexEntityDTO
                            {
                                Resource = "LIFT",
                                Entity = liftDTO
                            }
                        }
                    });
                }));
            }

            Task.WaitAll(PostTasks.ToArray());
        }
        #endregion

        #region Loaded and Closed
        internal override void Loaded()
        {
            //TODO update this behavior
            SelectedLift = LiftIntervals[0];

        }
        #endregion

    }
}
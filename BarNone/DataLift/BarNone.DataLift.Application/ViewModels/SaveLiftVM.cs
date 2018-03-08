using BarNone.DataLift.APIRequest;
using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.ViewModels.Common;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.Flex;
using BarNone.Shared.DomainModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
        private ObservableCollection<SharableUser> _users = new ObservableCollection<SharableUser> {
            new SharableUser()
            {
                Name = "Chris Stokes",
                UserName = "TheAryan"
            },
            new SharableUser()
            {
                Name = "Aamir Mansoor",
                UserName = "Csharp70"
            },
            new SharableUser()
            {
                Name = "Jon Brown",
                UserName = "NotActuallyBrown"
            },
            new SharableUser()
            {
                Name = "Riley McGee",
                UserName = "Fuggles"
            },
            new SharableUser(){
                Name = "Vishesh Gulatee",
                UserName = "ActuallyBrown"
            }
        };
        public ObservableCollection<SharableUser> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Users"));
            }
        }
        #endregion

        #region Custom User Implementation
        public class SharableUser
        {
            public string Name
            {
                get; set;
            }
            public string UserName
            {
                get; set;
            }

            public string Code
            {
                get
                {
                    return UserName[0].ToString();
                }

                private set { }
            }

            public bool IsSeletcted
            {
                get; set;
            }

            public SharableUser()
            {
                IsSeletcted = false;
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
            var liftDTO = Converters.NewConvertion()
                .Lift
                .CreateDTO(new Lift()
                {
                    BodyData = new BodyData
                    {
                        BodyDataFrames = CurrentLiftData.CurrentRecordedBodyData.ToList(),
                        RecordDate = DateTime.Now
                    },
                    Name = CurrentLiftData.LiftInformation[0].LiftName // TODO.  Not make this a hardcoded 0.
                });

            var toSend = JsonConvert.SerializeObject(liftDTO, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );

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

            string fname = string.Format("{0}.json", liftDTO.Name);
            if (File.Exists(fname))
                File.Delete(fname);
            File.WriteAllText(fname, toSend);

            Console.WriteLine("Send Lift functionality to be implemented.");
        }
        #endregion

        #region Loaded and Closed

        // To be uncommented when I actually have the DB.
        //async internal override void Loaded()
        //{
        //    List<UserDTO> allUserDTOs = await DataManager.Users.GetAll();
        //    List<User> allUserDM = allUserDTOs.Select(x => Converters.NewConvertion().User.CreateDataModel(x)).ToList();
        //}
        #endregion
    }
}

using BarNone.DataLift.UI.Commands;
using BarNone.DataLift.UI.ViewModels.Common;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DomainModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private void SendLiftCommand()
        {
            var liftDTO = Converters.NewConvertion()
                .Lift
                .CreateDTO(
                    new Lift()
                    {
                        BodyData = new BodyData() { BodyDataFrames = CurrentLiftData.CurrentRecordedData.ToList(), RecordDate = DateTime.Now },
                        Name = "LiftName_Temp"
                    }
                );
            var toSend = JsonConvert.SerializeObject(liftDTO, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            string fname = string.Format("{0}.json", liftDTO.Name);
            if (File.Exists(fname))
                File.Delete(fname);
            File.WriteAllText(fname, toSend);

            Console.WriteLine("Send Lift functionality to be implemented.");
        }
        #endregion
    }
}

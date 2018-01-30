using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BarNone.Shared.Analysis
{

    public class SessionSplitter
    {

        private static Dictionary<string, BodyData> templateLifts;
        private static Dictionary<string, List<JointTimeSeries>> timeSeriesSets;

        static SessionSplitter()
        {
            string json = File.ReadAllText(@"\Users\jon\developer\barnone\analysis\lifts\Chris_Single_Squat_1.json");
            LiftDTO liftDTO = JsonConvert.DeserializeObject<LiftDTO>(json);
            Lift lift = Converters.NewConvertion().Lift.CreateDataModel(liftDTO);

            templateLifts = new Dictionary<string, BodyData>
            {
                { "Squat", lift.BodyData }
            };
            //templateLifts["Squat"] = lift.BodyData;

            timeSeriesSets = new Dictionary<string, List<JointTimeSeries>>
            {
                {"Squat", Utils.ConvertBodyDataToTimeSeriesSet(lift.BodyData) }
            };
        }

        /// <summary>
        /// Joint data from an unsplit ift session.
        /// </summary>
        private BodyData inputBodyData;

        private List<JointTimeSeries> inputTimeSeriesSet;

        public SessionSplitter(BodyData bodyData)
        {
            inputBodyData = bodyData;
        }

        /// <summary>
        /// Determines indices for the frame numbers that best fit as candidates for the beginning of lifts.
        /// </summary>
        /// <returns>List containing the indexes of the beginning of lifts.</returns>
        public List<int> SplitLifts()
        {
            return new List<int> { 0 };
        }
    }
}

using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using NDtw;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.Analysis
{
    public class MomentIdentifier
    {
        private static Dictionary<string, BodyData> templateLifts;
        private static Dictionary<string, List<JointTimeSeries>> templateTimeSeriesSets;

        static MomentIdentifier()
        {
            byte[] jsonBytes = Properties.Resources.Chris_Single_Squat_1;
            string json = System.Text.Encoding.UTF8.GetString(jsonBytes);
            LiftDTO liftDTO = JsonConvert.DeserializeObject<LiftDTO>(json);
            Lift lift = Converters.NewConvertion().Lift.CreateDataModel(liftDTO);
            lift.BodyData.BodyDataFrames = lift.BodyData.BodyDataFrames.Skip(10).SkipLast(3).ToList();


            templateLifts = new Dictionary<string, BodyData>
            {
                { "Squat", lift.BodyData }
            };
            //templateLifts["Squat"] = lift.BodyData;

            templateTimeSeriesSets = new Dictionary<string, List<JointTimeSeries>>
            {
                {"Squat", Utils.ConvertBodyDataToTimeSeriesSet(lift.BodyData) }
            };
        }

        /// <summary>
        /// Joint data from an unsplit ift session.
        /// </summary>
        private BodyData inputBodyData;

        public List<JointTimeSeries> inputTimeSeriesSet;

        public MomentIdentifier(BodyData bodyData)
        {
            inputBodyData = bodyData;
            inputTimeSeriesSet = Utils.ConvertBodyDataToTimeSeriesSet(inputBodyData);
        }

        public int FindBottomOfSquat()
        {
            double[] lsHeight = Array.ConvertAll<float, double>(inputTimeSeriesSet[0].Y, x => (double)x);
            double[] tlsHeight = Array.ConvertAll<float, double>(templateTimeSeriesSets["Squat"][0].Y, x => (double)x);

            float d2f = inputTimeSeriesSet[0].Y[0] - inputTimeSeriesSet[15].Y[0];
            float d2f_t = templateTimeSeriesSets["Squat"][0].Y[0] - templateTimeSeriesSets["Squat"][15].Y[0];

            lsHeight = lsHeight.Select(x => x / d2f).ToArray();
            tlsHeight = tlsHeight.Select(x => x / d2f_t).ToArray();

            Dtw dtw = new Dtw(lsHeight, tlsHeight);
            int warpedIndex = dtw.GetPath().Select(x => x.Item2).ToList().IndexOf(20);
            int unwarpedSampleIndex = dtw.GetPath()[warpedIndex].Item1;
            var path = dtw.GetPath();
            return unwarpedSampleIndex;
        }
    }
}

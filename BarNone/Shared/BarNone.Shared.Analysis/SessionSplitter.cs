using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using NDtw;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BarNone.Shared.Analysis
{

    public class SessionSplitter
    {

        private static Dictionary<string, BodyData> templateLifts;
        private static Dictionary<string, List<JointTimeSeries>> templateTimeSeriesSets;

        static SessionSplitter()
        {
            byte[] jsonBytes = Properties.Resources.Chris_Single_Squat_1;
            string json = System.Text.Encoding.UTF8.GetString(jsonBytes);
            LiftDTO liftDTO = JsonConvert.DeserializeObject<LiftDTO>(json);
            Lift lift = Converters.NewConvertion().Lift.CreateDataModel(liftDTO);

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

        private List<JointTimeSeries> inputTimeSeriesSet;

        public SessionSplitter(BodyData bodyData)
        {
            inputBodyData = bodyData;
            inputTimeSeriesSet = Utils.ConvertBodyDataToTimeSeriesSet(inputBodyData);
        }

        /// <summary>
        /// Determines indices for the frame numbers that best fit as candidates for the beginning of lifts.
        /// </summary>
        /// <returns>List containing the indexes of the beginning of lifts.</returns>
        public List<int> SplitSquats()
        {
            double[] lsHeight = Array.ConvertAll<float,double>(inputTimeSeriesSet[0].Y, x => (double)x);
            double[] tlsHeight = Array.ConvertAll<float, double>(templateTimeSeriesSets["Squat"][0].Y, x => (double)x);

            float d2f = inputTimeSeriesSet[0].Y[0] - inputTimeSeriesSet[15].Y[0];
            float d2f_t = templateTimeSeriesSets["Squat"][0].Y[0] - templateTimeSeriesSets["Squat"][15].Y[0];

            lsHeight = lsHeight.Select(x => x / d2f).ToArray();
            tlsHeight = tlsHeight.Select(x => x / d2f_t).ToArray();

            List<double> distances = new List<double> { };
            const int resolution = 1;
            for (int offset = 0; offset < lsHeight.Length - tlsHeight.Length; offset += resolution)
            {
                double[] lsHeightSubset = lsHeight.Skip(offset).Take(tlsHeight.Length).ToArray();
                distances.Add(new Dtw(lsHeightSubset, tlsHeight).GetCost());
            }



            return new List<int> { 0 };
        }
    }
}

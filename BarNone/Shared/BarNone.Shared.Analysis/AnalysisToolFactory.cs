//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace BarNone.Shared.Analysis
//{ 
//    static class AnalysisToolFactory
//    {
//        private static Dictionary<string, BodyData> templateLifts;
//        private static Dictionary<string, List<JointTimeSeries>> templateTimeSeriesSets;

//        private static SessionSplitter ss;


//        static SessionSplitter()
//        {
//            byte[] jsonBytes = Properties.Resources.Chris_Single_Squat_1;
//            string json = System.Text.Encoding.UTF8.GetString(jsonBytes);
//            LiftDTO liftDTO = JsonConvert.DeserializeObject<LiftDTO>(json);
//            Lift lift = Converters.NewConvertion().Lift.CreateDataModel(liftDTO);

//            templateLifts = new Dictionary<string, BodyData>
//            {
//                { "Squat", lift.BodyData }
//            };
//            //templateLifts["Squat"] = lift.BodyData;

//            templateTimeSeriesSets = new Dictionary<string, List<JointTimeSeries>>
//            {
//                {"Squat", Utils.ConvertBodyDataToTimeSeriesSet(lift.BodyData) }
//            };
//        }


//        public static SessonSplitter GetSessionSplitter()
//        {
//            if (ss != null)
//                return ss;
            
//            ss = new SessionSplitter()
            
//        }
//    }
//}

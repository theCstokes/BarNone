using BarNone.Shared.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataConverters;
using BarNone.Shared.Analysis;
using System.Collections.Generic;
using BarNone.Shared.Analysis.LiftAnalysisPipeline.Velocity;

namespace BarNone.Shared.AnalysisTests
{
    [TestClass]
    public class SplittingTest
    {
        [TestMethod]
        public void ChrisThreeSquats1()
        {
            string json = File.ReadAllText(@"\Users\jon\developer\barnone\analysis\lifts\Chris_Three_Squats_1.json");
            LiftDTO liftDTO = JsonConvert.DeserializeObject<LiftDTO>(json);
            Lift lift = Converters.NewConvertion().Lift.CreateDataModel(liftDTO);
            
            SessionSplitter ss = new SessionSplitter(lift.BodyData);

            Assert.AreEqual(ss.SplitSquats()[0], new List<int> { 0 }[0]);
        }


        [TestMethod]
        public void FullTest()
        {
            byte[] jsonBytes = Analysis.Properties.Resources.Aamir_Single_Squat_1;
            string json = System.Text.Encoding.UTF8.GetString(jsonBytes);
            //string json = File.ReadAllText(@"\Users\jon\developer\barnone\analysis\lifts\Chris_Three_Squats_1.json");
            LiftDTO liftDTO = JsonConvert.DeserializeObject<LiftDTO>(json);
            Lift lift = Converters.NewConvertion().Lift.CreateDataModel(liftDTO);

            MomentIdentifier mi = new MomentIdentifier(lift.BodyData);
            int frame = mi.FindBottomOfSquat();
            var d = Utils.GetSquatAnalyticsBottom(mi.inputTimeSeriesSet, frame);

        }

        [TestMethod]
        public void FindBottomTest1()
        {
            byte[] jsonBytes = Analysis.Properties.Resources.Aamir_Single_Squat_1;
            string json = System.Text.Encoding.UTF8.GetString(jsonBytes);
            //string json = File.ReadAllText(@"\Users\jon\developer\barnone\analysis\lifts\Chris_Three_Squats_1.json");
            LiftDTO liftDTO = JsonConvert.DeserializeObject<LiftDTO>(json);
            Lift lift = Converters.NewConvertion().Lift.CreateDataModel(liftDTO);

            MomentIdentifier mi = new MomentIdentifier(lift.BodyData);

            Console.WriteLine(mi.FindBottomOfSquat());

        }

        [TestMethod]
        public void UtilsChrisThreeSquats1()
        {
            string json = File.ReadAllText(@"\Users\jon\developer\barnone\analysis\lifts\Chris_Three_Squats_1.json");
            LiftDTO liftDTO = JsonConvert.DeserializeObject<LiftDTO>(json);
            Lift lift = Converters.NewConvertion().Lift.CreateDataModel(liftDTO);

            Console.WriteLine(string.Join<float>(",", Utils.ConvertBodyDataToTimeSeriesSet(lift.BodyData)[0].X));
        }

        [TestMethod]
        public void ChrisThreeSquats1Velocity()
        {
            string json = File.ReadAllText(@"\Users\jon\developer\barnone\analysis\lifts\Chris_Three_Squats_1.json");
            LiftDTO liftDTO = JsonConvert.DeserializeObject<LiftDTO>(json);
            Lift lift = Converters.NewConvertion().Lift.CreateDataModel(liftDTO);

            var request = new AR_Velocity();
            request.Joint = EJointType.SpineBase;
            request.Dimension = EDimension.Y;
            request.Type = Analysis.LiftAnalysisPipeline.Core.ELiftAnalysisType.Velocity;
            var pipe = new LAP_Velocity(request, lift);
            var r = pipe.Execute();
            Assert.IsNotNull(r);
        }
    }
}

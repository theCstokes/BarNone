using BarNone.Shared.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataConverters;
using BarNone.Shared.Analysis;
using System.Collections.Generic;

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
            byte[] jsonBytes = Analysis.Properties.Resources.Chris_Three_Squats_1;
            string json = System.Text.Encoding.UTF8.GetString(jsonBytes);
            //string json = File.ReadAllText(@"\Users\jon\developer\barnone\analysis\lifts\Chris_Three_Squats_1.json");
            LiftDTO liftDTO = JsonConvert.DeserializeObject<LiftDTO>(json);
            Lift lift = Converters.NewConvertion().Lift.CreateDataModel(liftDTO);

            SessionSplitter ss = new SessionSplitter(lift.BodyData);

            ss.SplitSquats();

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
    }
}

using BarNone.Shared.Analysis;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BarNone.Shared.AnalysisTests
{

    [TestClass]
    class UtilsTest { 
    
        [TestMethod]
        public void UtilsChrisThreeSquats1()
        {
            string json = File.ReadAllText(@"\Users\jon\developer\barnone\analysis\lifts\Chris_Three_Squats_1.json");
            LiftDTO liftDTO = JsonConvert.DeserializeObject<LiftDTO>(json);
            Lift lift = Converters.NewConvertion().Lift.CreateDataModel(liftDTO);

            Console.WriteLine(string.Join<float>(",", Utils.ConvertBodyDataToTimeSeriesSet(lift.BodyData)[0].X));
        }
    }
}

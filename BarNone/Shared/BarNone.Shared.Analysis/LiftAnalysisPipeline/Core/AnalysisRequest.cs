using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Core
{
    public class AnalysisRequest
    {
        //public AnalysisRequest()
        //{
        //    Requests = new List<RequestEntity>();
        //}

        public List<object> Requests { get; set; }
    }


    public class RequestEntity : IAnalysisRequest
    {
        public ELiftAnalysisType Type { get; set; }
    }
}

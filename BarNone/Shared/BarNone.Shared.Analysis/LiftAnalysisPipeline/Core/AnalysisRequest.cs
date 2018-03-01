using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Core
{
    public class AnalysisRequest
    {
        public AnalysisRequest()
        {
            Requests = new List<RequestEntity>();
        }

        public List<RequestEntity> Requests { get; set; }
    }

    public class RequestEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ELiftAnalysisType Type { get; set; }
    }
}

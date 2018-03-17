using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Core
{
    public interface IAnalysisRequest
    {
        [JsonConverter(typeof(StringEnumConverter))]
        ELiftAnalysisType Type { get; set; }
    }
}

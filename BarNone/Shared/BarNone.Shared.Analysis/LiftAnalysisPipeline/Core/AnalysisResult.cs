using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Core
{
    public class AnalysisResult
    {
        public AnalysisResult()
        {
            Results = new List<ResultEntity>();
        }

        public List<ResultEntity> Results { get; set; }
    }

    public class ResultEntity
    {
        public ELiftAnalysisType Type { get; set; }

        public object Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Core
{
    public class LiftAnalysisPipeline
    {
        private List<ILiftAnalysisPipe> pipes;

        public LiftAnalysisPipeline()
        {
            pipes = new List<ILiftAnalysisPipe>();
        }

        public void Register(ILiftAnalysisPipe pipe)
        {
            pipes.Add(pipe);
        }

        //public AnalysisResult Execute(AnalysisRequest analysisRequest)
        //{
        //    return analysisRequest.Requests.Aggregate(new AnalysisResult(), (result, request) =>
        //    {
        //        var pipe = pipes.FirstOrDefault(p => p.Validate());
        //        if (pipe == null) return result;
        //        result.Results.Add(pipe.Execute());
        //        return result;
        //    });
        //}

        public AnalysisResult Execute()
        {
            return pipes.Aggregate(new AnalysisResult(), (result, pipe) =>
            {
                //if (!pipe.Validate()) return result;
                result.Results.Add(pipe.Execute());
                return result;
            });
        }
    }
}

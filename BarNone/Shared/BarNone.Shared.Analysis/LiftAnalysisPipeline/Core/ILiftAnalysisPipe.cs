using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Core
{
    public interface ILiftAnalysisPipe
    {
        bool Validate();

        ResultEntity Execute();
    }
}

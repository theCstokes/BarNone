using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Core
{
    public abstract class BaseLiftAnalysisPipe<TRequest> : ILiftAnalysisPipe
        where TRequest : RequestEntity
    {
        public BaseLiftAnalysisPipe(TRequest request)
        {
            Request = request;
        }

        public TRequest Request { get; private set; }

        public abstract ResultEntity Execute();

        public abstract bool Validate();
    }
}

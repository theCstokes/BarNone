using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Angle
{
    public class LAP_Angle : BaseLiftAnalysisPipe<AR_Angle>
    {
        private Lift _lift;

        public LAP_Angle(AR_Angle request, Lift lift) : base(request)
        {
            _lift = lift;
        }

        public override ResultEntity Execute()
        {
            return new ResultEntity
            {

            };
        }

        public override bool Validate()
        {
            if (Request.Type == ELiftAnalysisType.Angle) return false;
            if (Request.Target == default(EJointType)) return false;
            if (Request.Source == default(EJointType)) return false;
            return true;
        }
    }
}

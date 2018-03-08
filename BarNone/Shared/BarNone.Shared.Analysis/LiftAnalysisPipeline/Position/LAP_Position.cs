using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Angle
{
    /// <summary>
    /// Analysis Pipe that produces the angle at vertex b of the triangle abc specified by the user.
    /// </summary>
    public class LAP_Position : BaseLiftAnalysisPipe<AR_Position>
    {
        private Lift _lift;
        private List<JointTimeSeries> _jtsList;

        public LAP_Position(AR_Position request, Lift lift) : base(request)
        {
            _lift = lift;
            _jtsList = Utils.ConvertBodyDataToTimeSeriesSet(_lift.BodyData);
        }

        public override ResultEntity Execute()
        {
            return new ResultEntity
            {
                Type = ELiftAnalysisType.Position
            };
        }

        public override bool Validate()
        {
            if (Request.Type == ELiftAnalysisType.Position) return false;
            return true;
        }

    }

}
using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Position
{
    /// <summary>
    /// Analysis Pipe that produces the velocity of a specified joint along a given dimension.
    /// </summary>
    public class LAP_Velocity : BaseLiftAnalysisPipe<AR_Position>
    {
        private Lift _lift;
        private List<JointTimeSeries> _jtsList;

        public LAP_Velocity(AR_Position request, Lift lift) : base(request)
        {
            _lift = lift;
            _jtsList = Utils.ConvertBodyDataToTimeSeriesSet(_lift.BodyData);
        }

        public override ResultEntity Execute()
        {
            //This is a little ugly, might clean it up later. We should discuss the default behaviour, currently returns Z.
            List<float> result;
            if (Request.Dimension == EDimension.X)
            {
                result = new List<float>(_jtsList[(int)Request.Joint].X);
            }
            else if (Request.Dimension == EDimension.Y)
            {
                result = new List<float>(_jtsList[(int)Request.Joint].Y);

            }
            else
            {
                result = new List<float>(_jtsList[(int)Request.Joint].Z);

            }

            List<float> time = new List<float>(_jtsList[(int)Request.Joint].t);

            return new ResultEntity
            {
                Type = ELiftAnalysisType.Position,
                Value = new Dictionary<string, List<float>>() {
                    ["time"]= time,
                    ["data"]= result
                }
            };
        }

        public override bool Validate()
        {
            if (Request.Type == ELiftAnalysisType.Position) return false;
            return true;
        }

    }

}
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

            List<AnalysisFrame> afs = new List<AnalysisFrame>();

           

            //This is a little ugly, might clean it up later. We should discuss the default behaviour, currently returns Z.
            List<float> result;
            if (Request.Dimension == EDimension.X)
            {
                _lift.BodyData.BodyDataFrames.ForEach((a) =>
                {
                    afs.Add(new AnalysisFrame
                    {
                        FrameID = a.ID,
                        Value = a.Joints[(int)Request.JointType].X
                    });
                });
            }
            else if (Request.Dimension == EDimension.Y)
            {
                _lift.BodyData.BodyDataFrames.ForEach((a) =>
                {
                    afs.Add(new AnalysisFrame
                    {
                        FrameID = a.ID,
                        Value = a.Joints[(int)Request.JointType].Y
                    });
                });

            }
            else
            {
                _lift.BodyData.BodyDataFrames.ForEach((a) =>
                {
                    afs.Add(new AnalysisFrame
                    {
                        FrameID = a.ID,
                        Value = a.Joints[(int)Request.JointType].Z
                    });
                });

            }
            return new ResultEntity
            {
                Type = ELiftAnalysisType.Position,
                Value = afs
            };
        }

        public override bool Validate()
        {
            if (Request.Type == ELiftAnalysisType.Position) return false;
            return true;
        }

    }

}
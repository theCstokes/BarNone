using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Acceleration
{
    public class LAP_Acceleration : ILiftAnalysisPipe
    {
        #region Private Field(s).
        private Lift _lift;
        private RequestEntity _request;
        #endregion

        #region Public Constructor(s).
        public LAP_Acceleration(Lift lift, RequestEntity request)
        {
            _lift = lift;
            _request = request;
        } 
        #endregion

        #region ILiftAnalysisPipe Implementation.
        public ResultEntity Execute()
        {
            var acclerationList = GetAcclerationList();

            return new ResultEntity
            {
                Type = _request.Type,
                Value = acclerationList
            };
        }

        public bool Validate()
        {
            if (_request.Type != ELiftAnalysisType.Acceleration) return false;
            return true;
        }
        #endregion

        #region Private Member(s).
        private List<double> GetAcclerationList()
        {
            List<double> accelerationList = new List<double>();

            var r = _lift.BodyData.BodyDataFrames.Aggregate((last, frame) =>
            {
                var lastCenter = GetBarCenterPosition(last);
                var currentCenter = GetBarCenterPosition(frame);

                var timeDelta = (frame.TimeOfFrame.TotalMilliseconds - last.TimeOfFrame.TotalMilliseconds) * 1000;
                if (timeDelta == 0) timeDelta = 1;  // Cannot divide by 0

                // This is already in meters.
                var distance = (
                    Math.Sqrt(Math.Pow(currentCenter.X, 2) + Math.Pow(currentCenter.Y, 2) + Math.Pow(currentCenter.Z, 2))
                    -
                    Math.Sqrt(Math.Pow(lastCenter.X, 2) + Math.Pow(lastCenter.Y, 2) + Math.Pow(lastCenter.Z, 2))
                    );

                accelerationList.Add((distance / timeDelta) / timeDelta);

                return frame;
            });

            return accelerationList;
        }

        private BarCenterPosition GetBarCenterPosition(BodyDataFrame frame)
        {
            var leftHand = frame.Joints.Find(j => j.JointType == EJointType.HandLeft);
            var rightHand = frame.Joints.Find(j => j.JointType == EJointType.HandRight);

            return new BarCenterPosition
            {
                X = (leftHand.X + rightHand.X) / 2,
                Y = (leftHand.Y + rightHand.Y) / 2,
                Z = (leftHand.Z + rightHand.Z) / 2,
            };
        }
        #endregion
    }
}

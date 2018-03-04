using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.Analysis.LiftAnalysisPipeline.Acceleration
{
    struct TrackingPosition
    {
        public float X;

        public float Y;

        public float Z;
    }

    public class LAP_Acceleration : BaseLiftAnalysisPipe<AR_Acceleration>
    {
        #region Private Field(s).
        private Lift _lift;
        private AR_Acceleration _request;
        #endregion

        #region Public Constructor(s).
        public LAP_Acceleration(AR_Acceleration request, Lift lift) : base(request)
        {
            _lift = lift;
        } 
        #endregion

        #region ILiftAnalysisPipe Implementation.
        public override ResultEntity Execute()
        {
            var acclerationList = GetAcclerationList();

            return new ResultEntity
            {
                Type = _request.Type,
                Value = acclerationList
            };
        }

        public override bool Validate()
        {
            if (_request.Type != ELiftAnalysisType.Acceleration) return false;
            if (_request.JointType == default(EJointType)) return false;
            return true;
        }
        #endregion

        #region Private Member(s).
        private List<double> GetAcclerationList()
        {
            List<double> accelerationList = new List<double>();

            var r = _lift.BodyData.BodyDataFrames.Aggregate((last, frame) =>
            {
                var lastPosition = GetTrackingPosition(last);
                var currentPosition = GetTrackingPosition(frame);

                var timeDelta = (frame.TimeOfFrame.TotalMilliseconds - last.TimeOfFrame.TotalMilliseconds) * 1000;
                if (timeDelta == 0) timeDelta = 1;  // Cannot divide by 0.

                // This is already in meters.
                var distance = (
                    Math.Sqrt(Math.Pow(currentPosition.X, 2) + Math.Pow(currentPosition.Y, 2) + Math.Pow(currentPosition.Z, 2))
                    -
                    Math.Sqrt(Math.Pow(lastPosition.X, 2) + Math.Pow(lastPosition.Y, 2) + Math.Pow(lastPosition.Z, 2))
                    );

                accelerationList.Add((distance / timeDelta) / timeDelta);

                return frame;
            });

            return accelerationList;
        }

        private TrackingPosition GetTrackingPosition(BodyDataFrame frame)
        {
            if (_request.JointType == EJointType.BarCenter)
            {
                var leftHand = frame.Joints.Find(j => j.JointType == EJointType.HandLeft);
                var rightHand = frame.Joints.Find(j => j.JointType == EJointType.HandRight);

                return new TrackingPosition
                {
                    X = (leftHand.X + rightHand.X) / 2,
                    Y = (leftHand.Y + rightHand.Y) / 2,
                    Z = (leftHand.Z + rightHand.Z) / 2,
                };

            }

            var joint = frame.Joints.Find(j => j.JointType == _request.JointType);

            return new TrackingPosition
            {
                X = (joint.X),
                Y = (joint.Y),
                Z = (joint.Z),
            };
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using K = Microsoft.Kinect;
using S = BarNone.Shared.DomainModel;

namespace BarNone.DataLift.DataConverters.KinectToDM
{
    /// <summary>
    /// Transforms Kinect Depth data into <see cref="BarNone.Shared.DomainModel"/> objects
    /// </summary>
    public static class KinectDepthFrameConverter
    {
        #region BodyData
        public static S.BodyData CreateDmBodyData()
        {
            return new S.BodyData()
            {
                RecordDate = DateTime.Now
            };
        }

        public static S.BodyData KinectBodyDataToDmBodyData(this K.Body kBody, IList<K.BodyFrame> kBodyFrames)
        {
            var sBody = CreateDmBodyData();
            sBody.BodyDataFrames = kBodyFrames.Select(kF =>
                {
                    var sF = kF.KinectBdfToDmBdf(kBody);
                    sF.BodyData = sBody;
                    return sF;
                }).ToList();
            return sBody;
        }

        public static S.BodyData KinectBodyDataToDmBodyData(IList<S.BodyDataFrame> sBodyFrames)
        {
            var sBody = CreateDmBodyData();
            sBody.BodyDataFrames = sBodyFrames.Select(f =>
            {
                f.BodyData = sBody;
                return f;
            }).ToList();
            return sBody;
        }
        #endregion

        #region Body Data Frame
        public static S.BodyDataFrame KinectBdfToDmBdf(this K.BodyFrame kData, K.Body kBody)
        {
            var sBodyDataFrame = new S.BodyDataFrame();
            sBodyDataFrame.TimeOfFrame = kData.RelativeTime;
            sBodyDataFrame.Joints =
                kBody.Joints
                .Keys
                .Select(eJ =>
                {
                    var sJoint = kBody.Joints[eJ].KinectJointToDmMJoint();
                    sJoint.BodyDataFrame = sBodyDataFrame;
                    return sJoint;

                }).ToList();
            return sBodyDataFrame;
        }

        #endregion

        #region Joint
        public static S.Joint KinectJointToDmMJoint(this K.Joint kJoint)
        {
            return new S.Joint()
            {
                X = kJoint.Position.X,
                Y = kJoint.Position.Y,
                Z = kJoint.Position.Z,
                JointTypeID = (int)kJoint.JointType+1,
                JointType = (S.EJointType)kJoint.JointType,
                JointTrackingStateType = (S.EJointTrackingStateType)kJoint.TrackingState,
                JointTrackingStateTypeID = (int)kJoint.TrackingState
            };
        }

        #endregion

    }
}

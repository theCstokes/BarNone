using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DomainModel.Body
{
    /// <summary>
    /// Model of the Human Body, from the Kinect
    /// </summary>
    public static class Skeleton
    {
        /// <summary>
        /// All bone connections forming a human skeleton
        /// </summary>
        public static List<Tuple<EJointType, EJointType>> bones = new List<Tuple<EJointType, EJointType>>() {
            new Tuple<EJointType, EJointType>(EJointType.Head, EJointType.Neck),
            new Tuple<EJointType, EJointType>(EJointType.Neck, EJointType.SpineShoulder),
            new Tuple<EJointType, EJointType>(EJointType.SpineShoulder, EJointType.SpineMid),
            new Tuple<EJointType, EJointType>(EJointType.SpineMid, EJointType.SpineBase),
            new Tuple<EJointType, EJointType>(EJointType.SpineShoulder, EJointType.ShoulderRight),
            new Tuple<EJointType, EJointType>(EJointType.SpineShoulder, EJointType.ShoulderLeft),
            new Tuple<EJointType, EJointType>(EJointType.SpineBase, EJointType.HipRight),
            new Tuple<EJointType, EJointType>(EJointType.SpineBase, EJointType.HipLeft),

            // Right Arm
            new Tuple<EJointType, EJointType>(EJointType.ShoulderRight, EJointType.ElbowRight),
            new Tuple<EJointType, EJointType>(EJointType.ElbowRight, EJointType.WristRight),
            new Tuple<EJointType, EJointType>(EJointType.WristRight, EJointType.HandRight),
            new Tuple<EJointType, EJointType>(EJointType.HandRight, EJointType.HandTipRight),
            new Tuple<EJointType, EJointType>(EJointType.WristRight, EJointType.ThumbRight),

            // Left Arm
            new Tuple<EJointType, EJointType>(EJointType.ShoulderLeft, EJointType.ElbowLeft),
            new Tuple<EJointType, EJointType>(EJointType.ElbowLeft, EJointType.WristLeft),
            new Tuple<EJointType, EJointType>(EJointType.WristLeft, EJointType.HandLeft),
            new Tuple<EJointType, EJointType>(EJointType.HandLeft, EJointType.HandTipLeft),
            new Tuple<EJointType, EJointType>(EJointType.WristLeft, EJointType.ThumbLeft),

            // Right Leg
            new Tuple<EJointType, EJointType>(EJointType.HipRight, EJointType.KneeRight),
            new Tuple<EJointType, EJointType>(EJointType.KneeRight, EJointType.AnkleRight),
            new Tuple<EJointType, EJointType>(EJointType.AnkleRight, EJointType.FootRight),

            // Left Leg
            new Tuple<EJointType, EJointType>(EJointType.HipLeft, EJointType.KneeLeft),
            new Tuple<EJointType, EJointType>(EJointType.KneeLeft, EJointType.AnkleLeft),
            new Tuple<EJointType, EJointType>(EJointType.AnkleLeft, EJointType.FootLeft)
        };
    }
}

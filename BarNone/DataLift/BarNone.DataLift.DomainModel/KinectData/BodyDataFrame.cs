using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using BarNone.DataLift.DomainModel.Core;

namespace BarNone.DataLift.DataModel.KinectData
{
    /// <summary>
    /// 
    /// </summary>
    public class BodyDataFrame : IDataModel<BodyDataFrame>
    {
        #region Public Property(s).
        /// <summary>
        /// ID of the BodyFrame
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Time of this frame
        /// </summary>
        public DateTime TimeOfFrame { get; set; }

        /// <summary>
        /// Joint types mapped to the joint object in the kinect body data
        /// </summary>
        public IDictionary<JointType, Joint> Joints { get; set; }

        #endregion
    }
}

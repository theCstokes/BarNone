using Microsoft.Kinect;
using System;
using System.Linq;
using System.Collections.Generic;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.LiftData;
using BarNone.DataLift.DomainModel.Core;

namespace BarNone.DataLift.DataModel.KinectData
{
    public class BodyDataFrame : IDataModel<BodyDataFrame>
    {
        #region Public Property(s).
        public int ID { get; set; }

        /// <summary>
        /// Time of this frame
        /// </summary>
        public DateTime TimeOfFrame { get; set; }

        public IDictionary<JointType, Joint> Joints { get; set; }
        #endregion
    }
}

using Microsoft.Kinect;
using System;
using System.Collections.Generic;

namespace BarNone.DataLift.DomainModel.KinectData
{
    class BodyDataFrame
    {
        #region Public Properties
        /// <summary>
        /// Time of this frame
        /// </summary>
        public DateTime TimeOfFrame
        {
            get; private set;
        }

        public IReadOnlyDictionary<JointType, Joint> Joints { get; }

        #endregion

        #region Constructor(s)
        /// <summary>
        /// Construct a Body Data Frame
        /// </summary>
        public BodyDataFrame(IReadOnlyDictionary<JointType, Joint> joints)
        {
            //Set the Time of the dataframe
            TimeOfFrame = DateTime.Now;
            this.Joints = joints;

        }

        #endregion

        #region API Method(s)
        /// <summary>
        /// Calculate the time between <paramref name="frame"/> and this
        /// </summary>
        /// <param name="frame">Frame used to get a time span</param>
        public TimeSpan? TimeBetweenFrames(BodyDataFrame prevFrame)
        {
            if (prevFrame != null)
            {
                return prevFrame.TimeOfFrame - TimeOfFrame;
            }
            else
            {
                return null;
            }

        }

        #endregion

    }
}

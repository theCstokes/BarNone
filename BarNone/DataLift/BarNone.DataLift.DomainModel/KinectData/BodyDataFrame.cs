﻿using Microsoft.Kinect;
using System;
using System.Linq;
using System.Collections.Generic;
using BarNone.DataLift.DomainModel.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.Types;

namespace BarNone.DataLift.DomainModel.KinectData
{
    class BodyDataFrame : BaseDomainModel<BodyDataFrameDTO, BodyDataFrameDetailDTO>
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

        public override BodyDataFrameDTO BuildDTO()
        {
            return new BodyDataFrameDTO()
            {
                TimeOfFrame = this.TimeOfFrame,
                Details = BuildDetailDTO()
            };
        }

        public override BodyDataFrameDetailDTO BuildDetailDTO()
        {
            return new BodyDataFrameDetailDTO()
            {
                Joints = Joints.Select(
                    kv => new JointDTO()
                    {
                        Details = new JointDetailDTO(),
                        PositionX = kv.Value.Position.X,
                        PositionY = kv.Value.Position.Y,
                        PositionZ = kv.Value.Position.Z,
                        TrackingState = (DTOTrackingState)kv.Key,
                        JointType = (DTOJointType)kv.Value.JointType
                    })
                    .ToList()
            };
        }

        #endregion

    }
}
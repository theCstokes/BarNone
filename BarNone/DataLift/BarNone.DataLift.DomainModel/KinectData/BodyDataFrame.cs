using Microsoft.Kinect;
using System;
using System.Linq;
using System.Collections.Generic;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.Types;
using BarNone.Shared.DomainModel.Core;

namespace BarNone.DataLift.DomainModel.KinectData
{
    public class BodyDataFrame : BaseChildDomainModel<BodyDataFrame,BodyDataFrameDTO,BodyData,BodyDataDTO>,
        IDetailDomainModel<BodyDataFrameDTO, BodyDataFrameDetailDTO>
    {
        #region Public Properties
        /// <summary>
        /// ID of this frame
        /// </summary>
        public override int ID { get; set; }
        /// <summary>
        /// Time of this frame
        /// </summary>
        public DateTime TimeOfFrame
        {
            get; set;
        }

        public IDictionary<JointType, Joint> Joints { get; set; }

        #endregion

        #region Constructor(s)
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

        public override BodyDataFrameDTO BuildDTO(BodyDataDTO parentDTO)
        {
            BodyDataFrameDTO currentFrame = new BodyDataFrameDTO()
            {
                ID = this.ID,
                TimeOfFrame = this.TimeOfFrame,
                Details = BuildDetailDTO(),
                
            };

            parentDTO.Details.OrderedFrames.Add(currentFrame);

            return currentFrame;
        }

        public override BodyDataFrameDTO BuildDTO()
        {
            return new BodyDataFrameDTO()
            {
                ID = this.ID,
                TimeOfFrame = this.TimeOfFrame,
                Details = BuildDetailDTO()
            };
        }

        public override void PopulateFromDTO(BodyDataFrameDTO dto)
        {
            ID = dto.ID;
            TimeOfFrame = dto.TimeOfFrame;
            Joints = BuildJointDict(dto.Details?.Joints);
        }

        public override void PopulateFromDTO(BodyDataFrameDTO dto, BodyData parent)
        {
            ID = dto.ID;
            TimeOfFrame = dto.TimeOfFrame;
            Joints = BuildJointDict(dto.Details.Joints);

            parent.AddNewFrame(this);
        }

        public BodyDataFrameDetailDTO BuildDetailDTO()
        {
            return new BodyDataFrameDetailDTO()
            {
                Joints = Joints?.Select(
                kv => new JointDTO()
                {
                    Details = new JointDetailDTO(),
                    PositionX = kv.Value.Position.X,
                    PositionY = kv.Value.Position.Y,
                    PositionZ = kv.Value.Position.Z,
                    TrackingState = (DTOTrackingState)kv.Value.TrackingState,
                    JointType = (DTOJointType)kv.Value.JointType
                })
                .ToDictionary(x => x.JointType, x => x)
            };  
        }

        private IDictionary<JointType,Joint> BuildJointDict(IDictionary<DTOJointType, JointDTO> JointListDTO)
        {
            return JointListDTO?.Select(
                joint => new Joint()
                {
                    JointType = (JointType)joint.Value.JointType,
                    Position = new CameraSpacePoint()
                    {
                        X = joint.Value.PositionX,
                        Y = joint.Value.PositionY,
                        Z = joint.Value.PositionZ
                    },
                    TrackingState = (TrackingState)joint.Value.TrackingState
                })
                .ToDictionary(x => x.JointType, x => x);
        }

        #endregion

    }
}

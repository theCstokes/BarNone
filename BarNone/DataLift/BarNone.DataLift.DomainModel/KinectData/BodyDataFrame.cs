using Microsoft.Kinect;
using System;
using System.Linq;
using System.Collections.Generic;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.Shared.DataTransfer.LiftData;

namespace BarNone.DataLift.DataModel.KinectData
{
    public class BodyDataFrame : BaseDataModel<BodyDataFrame, BodyDataFrameDTO, BodyDataFrameDetailDTO>
    {
        #region Public Property(s).
        /// <summary>
        /// Time of this frame
        /// </summary>
        public DateTime TimeOfFrame { get; set; }

        public IDictionary<JointType, Joint> Joints { get; set; }


        #endregion

        #region Abstract Class Impl

        protected override BodyDataFrameDetailDTO OnBuildDetailDTO(ConvertConfig config)
        {
            var jointDTOList = Joints?.Values.Select(
                kv => new JointDTO()
                {
                    PositionX = kv.Position.X,
                    PositionY = kv.Position.Y,
                    PositionZ = kv.Position.Z,
                    TrackingState = new TrackingStateDTO() { Value = Convert.ToInt32(kv.TrackingState) },
                    JointType = new JointTypeDTO() { Value = Convert.ToInt32(kv.JointType) }
                })
                .ToList();

            return new BodyDataFrameDetailDTO() { Joints = jointDTOList };
        }

        protected override BodyDataFrameDTO OnBuildDTO()
        {
            BodyDataFrameDTO currentFrame = new BodyDataFrameDTO()
            {
                TimeOfFrame = TimeOfFrame
            };

            return currentFrame;
        }


        protected override void OnPopulate(BodyDataFrameDTO dto, ConvertConfig config = null)
        {
            TimeOfFrame = dto.TimeOfFrame;
        }

        protected override void OnDetailPopulate(BodyDataFrameDetailDTO dto, ConvertConfig config = null)
        {
            Joints = dto.Joints.Select(
                joint => new Joint()
                {
                    JointType = (JointType)Enum.ToObject(typeof(JointType), joint.JointType.Value),
                    Position = new CameraSpacePoint()
                    {
                        X = joint.PositionX,
                        Y = joint.PositionY,
                        Z = joint.PositionZ
                    },
                    TrackingState = (TrackingState)Enum.ToObject(typeof(TrackingState), joint.TrackingState.Value)
                })
                .ToDictionary(x => x.JointType, x => x);
        }

        #endregion

        private IDictionary<JointType, Joint> BuildJointDict(IList<JointDTO> JointList)
        {
            return JointList?.Select(jointDTO => new Joint()
            {
                JointType = (JointType)jointDTO.JointTypeID,
                Position = new CameraSpacePoint()
                {
                    X = jointDTO.X,
                    Y = jointDTO.Y,
                    Z = jointDTO.Z
                },
                TrackingState = (TrackingState)jointDTO.JointTrackingStateTypeID
            })
                .ToDictionary(x => x.JointType, x => x);
        }


        //#region API Method(s)
        //public override BodyDataFrameDTO BuildDTO(BodyDataDTO parentDTO)
        //{
        //    BodyDataFrameDTO currentFrame = new BodyDataFrameDTO()
        //    {
        //        ID = ID,
        //        TimeOfFrame = TimeOfFrame
        //    };

        //    parentDTO.Details.OrderedFrames.Add(currentFrame);

        //    return currentFrame;
        //}

        //public override void PopulateFromDTO(BodyDataFrameDTO dto, BodyData parent)
        //{

        //    ID = dto.ID;
        //    TimeOfFrame = dto.TimeOfFrame;
        //    //Joints = BuildJointDict(dto.Details?.Joints);
        //}
        //#endregion
        ///// <summary>
        ///// Calculate the time between <paramref name="frame"/> and this
        ///// </summary>
        ///// <param name="frame">Frame used to get a time span</param>
        //public TimeSpan? TimeBetweenFrames(BodyDataFrame prevFrame)
        //{
        //    if (prevFrame != null)
        //    {
        //        return prevFrame.TimeOfFrame - TimeOfFrame;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}

        //public override BodyDataFrameDTO BuildDTO(BodyDataDTO parentDTO)
        //{
        //    BodyDataFrameDTO currentFrame = new BodyDataFrameDTO()
        //    {
        //        ID = ID,
        //        TimeOfFrame = TimeOfFrame
        //    };

        //    parentDTO.Details.OrderedFrames.Add(currentFrame);

        //    return currentFrame;
        //}

        //public override BodyDataFrameDTO BuildDTO()
        //{
        //    return new BodyDataFrameDTO()
        //    {
        //        ID = ID,
        //        TimeOfFrame = TimeOfFrame
        //    };
        //}

        //public override void PopulateFromDTO(BodyDataFrameDTO dto)
        //{
        //    ID = dto.ID;
        //    TimeOfFrame = dto.TimeOfFrame;
        //    Joints = BuildJointDict(dto.Details?.Joints);
        //}

        //public override void PopulateFromDTO(BodyDataFrameDTO dto, BodyData parent)
        //{
        //    ID = dto.ID;
        //    TimeOfFrame = dto.TimeOfFrame;
        //    Joints = BuildJointDict(dto.Details.Joints);

        //    parent.AddNewFrame(this);
        //}

        //public BodyDataFrameDetailDTO CreateDTO()
        //{
        //    return new BodyDataFrameDetailDTO()
        //    {
        //        Joints = Joints?.Select(
        //        kv => new JointDTO()
        //        {
        //            Details = new JointDetailDTO(),
        //            PositionX = kv.Value.Position.X,
        //            PositionY = kv.Value.Position.Y,
        //            PositionZ = kv.Value.Position.Z,
        //            TrackingState = (DTOTrackingState)kv.Value.TrackingState,
        //            JointType = (DTOJointType)kv.Value.JointType
        //        })
        //        .ToDictionary(x => x.JointType, x => x)
        //    };
        //}

        //private IDictionary<JointType, Joint> BuildJointDict(IDictionary<DTOJointType, JointDTO> JointListDTO)
        //{
        //    return JointListDTO?.Select(
        //        joint => new Joint()
        //        {
        //            JointType = (JointType)joint.Value.JointType,
        //            Position = new CameraSpacePoint()
        //            {
        //                X = joint.Value.PositionX,
        //                Y = joint.Value.PositionY,
        //                Z = joint.Value.PositionZ
        //            },
        //            TrackingState = (TrackingState)joint.Value.TrackingState
        //        })
        //        .ToDictionary(x => x.JointType, x => x);
        //}

        //#endregion

        //#region IDetailDomainModel Implementation.
        //dynamic IDetailDTOTransformable.CreateDTO()
        //{
        //    return CreateDTO();
        //} 
        //#endregion
    }
}

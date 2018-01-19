using BarNone.DataLift.DataModel.KinectData;
using BarNone.Shared.Core;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using Microsoft.Kinect;
using System;
using System.Linq;

namespace BarNone.DataLift.DataConverters.KinectData
{
    /// <summary>
    /// Body data Frame DM and DTO transforms
    /// </summary>
    public class BodyDataFrameConverter : BaseDetailDataConverter<BodyDataFrame, BodyDataFrameDTO, BodyDataFrameDetailDTO, Converters>
    {
        public BodyDataFrameConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        /// <summary>
        /// Translates dto to a BodyDataFrame
        /// </summary>
        /// <param name="dto">DTO being transformed</param>
        /// <returns>dto as a BodyDataFrame</returns>
        public override BodyDataFrame OnCreateDataModel(BodyDataFrameDTO dto)
        {
            return new BodyDataFrame
            {
                ID = dto.ID,
                TimeOfFrame = dto.TimeOfFrame
            };
        }

        /// <summary>
        /// Fills in data with dto info
        /// </summary>
        /// <param name="data">DM to populate</param>
        /// <param name="dto">DTO being transformed</param>
        public override void OnCreateDetailDataModel(BodyDataFrame data, BodyDataFrameDetailDTO dto)
        {
            data.Joints = dto.Joints?.Select(jointDTO => new Joint()
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

        /// <summary>
        /// Transforms data into its detail DTOs
        /// </summary>
        /// <param name="data">DM to transform</param>
        /// <returns>DTO Details representation of data</returns>
        public override BodyDataFrameDetailDTO OnCreateDetailDTO(BodyDataFrame data)
        {
            return new BodyDataFrameDetailDTO
            {
                Joints = data.Joints?.Values.Select(joint => new JointDTO()
                {
                    X = joint.Position.X,
                    Y = joint.Position.Y,
                    Z = joint.Position.Z,
                    JointTrackingStateTypeID = Convert.ToInt32(joint.TrackingState),
                    JointTypeID = Convert.ToInt32(joint.JointType)
                }).ToList()
            };
        }

        /// <summary>
        /// Transforms data into its DTO form
        /// </summary>
        /// <param name="data">DM to transform</param>
        /// <returns>DTO representation of data</returns>
        public override BodyDataFrameDTO OnCreateDTO(BodyDataFrame data)
        {
            return new BodyDataFrameDTO
            {
                ID = data.ID,
                TimeOfFrame = data.TimeOfFrame
            };
        }
    }
}

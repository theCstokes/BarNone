using BarNone.Shared.DataConverter;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    public class JointConverter :
        BaseDetailDataConverter<Joint, JointDTO, JointDetailDTO, Converters>
    {
        public JointConverter(Converters converterContext) : base(converterContext)
        {
        }

        public override Joint OnCreateDataModel(JointDTO dto)
        {
            return new Joint
            {
                ID = dto.ID,
                X = dto.X,
                Y = dto.Y,
                Z = dto.Z,
                BodyDataFrameID = dto.BodyDataFrameID,
                JointTrackingStateTypeID = dto.JointTrackingStateTypeID,
                JointTypeID = dto.JointTypeID
            };
        }

        public override void OnCreateDetailDataModel(Joint data, JointDetailDTO dto)
        {
            data.BodyDataFrame = converterContext.BodyDataFrame.CreateDataModel(dto.BodyDataFrame);
            data.JointTrackingStateType = converterContext.JointTrackingStateType.CreateDataModel(dto.JointTrackingStateType);
            data.JointType = converterContext.JointType.CreateDataModel(dto.JointType);
        }

        public override JointDetailDTO OnCreateDetailDTO(Joint data)
        {
            return new JointDetailDTO
            {
                BodyDataFrame = converterContext.BodyDataFrame.CreateDTO(data.BodyDataFrame),
                JointTrackingStateType = converterContext.JointTrackingStateType.CreateDTO(data.JointTrackingStateType),
                JointType = converterContext.JointType.CreateDTO(data.JointType)
            };
        }

        public override JointDTO OnCreateDTO(Joint data)
        {
            return new JointDTO
            {
                ID = data.ID,
                X = data.X,
                Y = data.Y,
                Z = data.Z,
                BodyDataFrameID = data.BodyDataFrameID,
                JointTrackingStateTypeID = data.JointTrackingStateTypeID,
                JointTypeID = data.JointTypeID
            };
        }
    }
}

using BarNone.Shared.Core;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    public class BodyDataFrameConverter
        : BaseDetailDataConverter<BodyDataFrame, BodyDataFrameDTO, BodyDataFrameDetailDTO, Converters>
    {
        public BodyDataFrameConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override BodyDataFrame OnCreateDataModel(BodyDataFrameDTO dto)
        {
            return new BodyDataFrame
            {
                ID = dto.ID,
                TimeOfFrame = dto.TimeOfFrame,
                BodyDataID = dto.BodyDataID,
                UserID = context.UserID
            };
        }

        public override void OnCreateDetailDataModel(BodyDataFrame data, BodyDataFrameDetailDTO dto)
        {
            data.BodyData = converterContext.BodyData.CreateDataModel(dto.BodyData);
            data.Joints = dto.Joints.Select(j => converterContext.Joint.CreateDataModel(j)).ToList();
        }

        public override BodyDataFrameDetailDTO OnCreateDetailDTO(BodyDataFrame data)
        {
            return new BodyDataFrameDetailDTO
            {
                BodyData = converterContext.BodyData.CreateDTO(data.BodyData),
                Joints = data.Joints.Select(j => converterContext.Joint.CreateDTO(j)).ToList()
            };
        }

        public override BodyDataFrameDTO OnCreateDTO(BodyDataFrame data)
        {
            return new BodyDataFrameDTO
            {
                ID = data.ID,
                BodyDataID = data.BodyDataID,
                TimeOfFrame = data.TimeOfFrame
            };
        }
    }
}

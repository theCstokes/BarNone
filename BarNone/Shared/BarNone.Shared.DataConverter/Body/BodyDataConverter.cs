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
    public class BodyDataConverter : BaseDetailDataConverter<BodyData, BodyDataDTO, BodyDataDetailDTO, Converters>
    {
        public BodyDataConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override BodyData OnCreateDataModel(BodyDataDTO dto)
        {
            return new BodyData
            {
                ID = dto.ID,
                RecordDate = dto.RecordTimeStamp,
                UserID = context.UserID
            };
        }

        public override void OnCreateDetailDataModel(BodyData data, BodyDataDetailDTO dto)
        {
            data.BodyDataFrames = 
                dto.OrderedFrames?.Select(f => converterContext.BodyDataFrame.CreateDataModel(f)).ToList();
        }

        public override BodyDataDetailDTO OnCreateDetailDTO(BodyData data)
        {
            return new BodyDataDetailDTO
            {
                OrderedFrames = data.BodyDataFrames?.Select(f => converterContext.BodyDataFrame.CreateDTO(f)).ToList()
            };
        }

        public override BodyDataDTO OnCreateDTO(BodyData data)
        {
            return new BodyDataDTO
            {
                ID = data.ID,
                RecordTimeStamp = data.RecordDate
            };
        }
    }
}

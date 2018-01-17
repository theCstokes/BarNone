using BarNone.DataLift.DataModel.KinectData;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.DataConverters.KinectData
{
    public class BodyDataConverter : BaseDetailDataConverter<BodyData, BodyDataDTO, BodyDataDetailDTO, Converters>
    {
        public BodyDataConverter(Converters converterContext) : base(converterContext)
        {
        }

        public override BodyData OnCreateDataModel(BodyDataDTO dto)
        {
            return new BodyData
            {
                ID = dto.ID,
                RecordDate = dto.RecordTimeStamp
            };
        }

        public override void OnCreateDetailDataModel(BodyData data, BodyDataDetailDTO dto)
        {
            data.DataFrames = dto.OrderedFrames.Select(f => converterContext.BodyDataFrame.CreateDataModel(f)).ToList();
        }

        public override BodyDataDetailDTO OnCreateDetailDTO(BodyData data)
        {
            return new BodyDataDetailDTO
            {
                OrderedFrames = data.DataFrames.Select(f => converterContext.BodyDataFrame.CreateDTO(f)).ToList()
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

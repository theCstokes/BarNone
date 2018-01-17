using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    public class LiftConverter : BaseDetailDataConverter<Lift, LiftDTO, LiftDetailDTO, Converters>
    {
        public LiftConverter(Converters converterContext) : base(converterContext)
        {
        }

        public override Lift OnCreateDataModel(LiftDTO dto)
        {
            return new Lift
            {
                ID = dto.ID,
                Name = dto.Name,
                ParentID = dto.ParentID,
                BodyDataID = dto.BodyDataID
            };
        }

        public override void OnCreateDetailDataModel(Lift data, LiftDetailDTO dto)
        {
            data.Parent = converterContext.LiftFolder.CreateDataModel(dto.Parent);
            data.BodyData = converterContext.BodyData.CreateDataModel(dto.BodyData);
        }

        public override LiftDetailDTO OnCreateDetailDTO(Lift data)
        {
            return new LiftDetailDTO
            {
                Parent = converterContext.LiftFolder.CreateDTO(data.Parent),
                BodyData = converterContext.BodyData.CreateDTO(data.BodyData)
            };
        }

        public override LiftDTO OnCreateDTO(Lift data)
        {
            return new LiftDTO
            {
                ID = data.ID,
                Name = data.Name,
                ParentID = data.ParentID,
                BodyDataID = data.BodyDataID
            };
        }
    }
}

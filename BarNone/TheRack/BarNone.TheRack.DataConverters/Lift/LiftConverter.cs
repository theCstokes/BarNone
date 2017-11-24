using BarNone.Shared.DataConverter;
using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DataConverters
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
                ParentID = dto.ParentID
            };
        }

        public override void OnCreateDetailDataModel(Lift data, LiftDetailDTO dto)
        {
            data.Parent = converterContext.LiftFolder.CreateDataModel(dto.Parent);
        }

        public override LiftDetailDTO OnCreateDetailDTO(Lift data)
        {
            return new LiftDetailDTO
            {
                Parent = converterContext.LiftFolder.CreateDTO(data.Parent)
            };
        }

        public override LiftDTO OnCreateDTO(Lift data)
        {
            return new LiftDTO
            {
                ID = data.ID,
                Name = data.Name,
                ParentID = data.ParentID
            };
        }
    }
}

using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.Shared.Core;

namespace BarNone.Shared.DataConverter
{
    public class LiftTypeConverter : BaseDataConverter<LiftType, LiftTypeDTO, Converters>
    {
        public LiftTypeConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override LiftType OnCreateDataModel(LiftTypeDTO dto)
        {
            return new LiftType
            {
                ID = dto.ID,
                Name = dto.Name
            };
        }

        public override LiftTypeDTO OnCreateDTO(LiftType data)
        {
            return new LiftTypeDTO
            {
                ID = data.ID,
                Name = data.Name
            };
        }
    }
}

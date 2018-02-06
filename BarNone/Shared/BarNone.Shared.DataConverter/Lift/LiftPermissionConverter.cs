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
    public class LiftPermissionConverter : BaseDataConverter<LiftPermission, LiftPermissionDTO, Converters>
    {
        public LiftPermissionConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override LiftPermission OnCreateDataModel(LiftPermissionDTO dto)
        {
            return new LiftPermission
            {
                ID = dto.ID,
                LiftID = dto.LiftID,
                UserID = dto.UserID
            };
        }

        public override LiftPermissionDTO OnCreateDTO(LiftPermission data)
        {
            return new LiftPermissionDTO
            {
                ID = data.ID,
                LiftID = data.LiftID,
                UserID = data.UserID
            };
        }
    }
}

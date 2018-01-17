using BarNone.Shared.DataConverter;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverters.Core
{
    public class BaseEnumDataConverter<TType, TTypeDTO, TEType> : BaseDataConverter<TType, TTypeDTO, Converters>
        where TType : BaseEnumDomainModel<TType, TEType>, new()
        where TTypeDTO : BaseTypeDTO<TTypeDTO>, new()
        where TEType : struct 

    {
        public BaseEnumDataConverter(Converters converterContext) : base(converterContext)
        {
        }

        public override TType OnCreateDataModel(TTypeDTO dto)
        {
            return new TType
            {
                ID = dto.ID,
                Name = dto.Name,
                Value = dto.Value
            };
        }

        public override TTypeDTO OnCreateDTO(TType data)
        {
            return new TTypeDTO
            {
                ID = data.ID,
                Name = data.Name,
                Value = data.Value
            };
        }
    }
}

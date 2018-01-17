using BarNone.Shared.Core;
using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverter.Core
{
    public abstract class BaseDetailDataConverter<TData, TDTO, TDetailDTO, TConverters> 
        : BaseDataConverter<TData, TDTO, TConverters>
        where TData : ITrackable<TData>, new()
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
        where TConverters : IConverter
    {
        public BaseDetailDataConverter(TConverters converterContext) : base(converterContext)
        {
        }

        public override TData CreateDataModel(TDTO dto)
        {
            if (dto == null) return default(TData);

            TData result = (TData)converterContext.Cache.GetDTOConvertion(GetType(), dto);
            if (result != null) return result;

            result = OnCreateDataModel(dto);
            converterContext.Cache.AddConvertion(GetType(), result, dto);

            if (dto.Details != null)
            {
                OnCreateDetailDataModel(result, dto.Details);
            }
            return result;
        }

        public override TDTO CreateDTO(TData data)
        {
            if (data == null) return default(TDTO);

            TDTO result = (TDTO)converterContext.Cache.GetDataModelConvertion(GetType(), data);
            if (result != null) return result;

            result = OnCreateDTO(data);
            converterContext.Cache.AddConvertion(GetType(), data, result);

            result.Details = OnCreateDetailDTO(data);
            return result;
        }

        public abstract void OnCreateDetailDataModel(TData data, TDetailDTO dto);
        public abstract TDetailDTO OnCreateDetailDTO(TData data);
    }
}

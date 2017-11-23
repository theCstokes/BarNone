using BarNone.Shared.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverter
{
    public interface IDataConverter
    {
        ITrackable CreateDataModel(ITrackableDTO dto);

        ITrackableDTO CreateDTO(ITrackable data);
    }

    //public interface IDataConverter<TData, TDTO> : IDataConverter
    //    where TData : ITrackable<TData>, new()
    //    where TDTO : ITrackableDTO<TDTO>, new()
    //{
    //    new TData CreateDataModel(TDTO dto);

    //    new TDTO CreateDTO(TData data);
    //}

    public abstract class BaseDataConverter<TData, TDTO, TConverters> : IDataConverter
        where TData : ITrackable<TData>, new()
        where TDTO : ITrackableDTO<TDTO>, new()
        where TConverters : IConverter
    {
        protected TConverters converterContext;

        public BaseDataConverter(TConverters converterContext)
        {
            this.converterContext = converterContext;
        }

        public virtual TData CreateDataModel(TDTO dto)
        {
            if (dto == null) return default(TData);

            TData result = (TData) converterContext.Cache.GetDTOConvertion(GetType(), dto);
            if (result != null) return result;

            result = OnCreateDataModel(dto);
            converterContext.Cache.AddConvertion(GetType(), result, dto);
            return result;
        }

        public virtual TDTO CreateDTO(TData data)
        {
            if (data == null) return default(TDTO);

            TDTO result = (TDTO)converterContext.Cache.GetDataModelConvertion(GetType(), data);
            if (result != null) return result;

            result = OnCreateDTO(data);
            converterContext.Cache.AddConvertion(GetType(), data, result);
            return result;
        }

        ITrackable IDataConverter.CreateDataModel(ITrackableDTO dto)
        {
            return CreateDataModel((TDTO)dto);
        }

        ITrackableDTO IDataConverter.CreateDTO(ITrackable data)
        {
            return (ITrackableDTO) CreateDTO((TData)data);
        }

        public abstract TData OnCreateDataModel(TDTO dto);
        public abstract TDTO OnCreateDTO(TData data);
    }
}

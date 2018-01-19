using BarNone.Shared.Core;
using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverter.Core
{
    /// <summary>
    /// Base detail data converter.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    /// <typeparam name="TDetailDTO">The type of the detail dto.</typeparam>
    /// <typeparam name="TConverters">The type of the converters.</typeparam>
    /// <seealso cref="BarNone.Shared.DataConverter.Core.BaseDataConverter{TData, TDTO, TConverters}" />
    public abstract class BaseDetailDataConverter<TData, TDTO, TDetailDTO, TConverters> 
        : BaseDataConverter<TData, TDTO, TConverters>
        where TData : ITrackable<TData>, new()
        where TDTO : BaseParentDTO<TDTO, TDetailDTO>, new()
        where TDetailDTO : BaseDetailDTO<TDetailDTO>, new()
        where TConverters : IConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDetailDataConverter{TData, TDTO, TDetailDTO, TConverters}"/> class.
        /// </summary>
        /// <param name="converterContext">The converter context.</param>
        /// <param name="context">The context.</param>
        public BaseDetailDataConverter(TConverters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        /// <summary>
        /// Creates the data model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates the dto.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates detail data model.
        /// Called when [create detail data model].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="dto">The dto.</param>
        public abstract void OnCreateDetailDataModel(TData data, TDetailDTO dto);

        /// <summary>
        /// Creates detail dto.
        /// Called when [create detail dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public abstract TDetailDTO OnCreateDetailDTO(TData data);
    }
}

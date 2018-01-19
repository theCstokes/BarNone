using BarNone.Shared.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverter.Core
{
    /// <summary>
    /// Data Converter.
    /// </summary>
    public interface IDataConverter
    {
        ITrackable CreateDataModel(ITrackableDTO dto);

        ITrackableDTO CreateDTO(ITrackable data);
    }

    /// <summary>
    /// Base Data Converter.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <typeparam name="TDTO">The type of the dto.</typeparam>
    /// <typeparam name="TConverters">The type of the converters.</typeparam>
    /// <seealso cref="BarNone.Shared.DataConverter.Core.IDataConverter" />
    public abstract class BaseDataConverter<TData, TDTO, TConverters> : IDataConverter
        where TData : ITrackable<TData>, new()
        where TDTO : ITrackableDTO<TDTO>, new()
        where TConverters : IConverter
    {
        /// <summary>
        /// The converter context.
        /// </summary>
        protected TConverters converterContext;

        /// <summary>
        /// The context
        /// </summary>
        protected IDomainContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataConverter{TData, TDTO, TConverters}"/> class.
        /// </summary>
        /// <param name="converterContext">The converter context.</param>
        /// <param name="context">The context.</param>
        public BaseDataConverter(TConverters converterContext, IDomainContext context)
        {
            this.converterContext = converterContext;
            this.context = context;
        }

        /// <summary>
        /// Creates the data model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public virtual TData CreateDataModel(TDTO dto)
        {
            if (dto == null) return default(TData);

            TData result = (TData) converterContext.Cache.GetDTOConvertion(GetType(), dto);
            if (result != null) return result;

            result = OnCreateDataModel(dto);
            converterContext.Cache.AddConvertion(GetType(), result, dto);
            return result;
        }

        /// <summary>
        /// Creates the dto.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public virtual TDTO CreateDTO(TData data)
        {
            if (data == null) return default(TDTO);

            TDTO result = (TDTO)converterContext.Cache.GetDataModelConvertion(GetType(), data);
            if (result != null) return result;

            result = OnCreateDTO(data);
            converterContext.Cache.AddConvertion(GetType(), data, result);
            return result;
        }

        /// <summary>
        /// Creates the data model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        ITrackable IDataConverter.CreateDataModel(ITrackableDTO dto)
        {
            return CreateDataModel((TDTO)dto);
        }

        /// <summary>
        /// Creates the dto.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        ITrackableDTO IDataConverter.CreateDTO(ITrackable data)
        {
            return (ITrackableDTO) CreateDTO((TData)data);
        }

        /// <summary>
        /// Creates data model.
        /// Called when [create data model].
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public abstract TData OnCreateDataModel(TDTO dto);

        /// <summary>
        /// Creates dto.
        /// Called when [create dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public abstract TDTO OnCreateDTO(TData data);
    }
}

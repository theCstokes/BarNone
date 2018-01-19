using BarNone.Shared.Core;
using BarNone.TheRack.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverter.Core
{
    /// <summary>
    /// Converter.
    /// </summary>
    public interface IConverter
    {
        ShareDataConverterCache Cache { get; }
    }

    /// <summary>
    /// Base Converter
    /// </summary>
    /// <typeparam name="TConverters">The type of the converters.</typeparam>
    /// <seealso cref="BarNone.Shared.DataConverter.Core.IConverter" />
    public abstract class BaseConverter<TConverters> : IConverter
        where TConverters : BaseConverter<TConverters>, new()
    {
        /// <summary>
        /// The data converter map
        /// </summary>
        private Dictionary<Type, IDataConverter> dataConverterMap;

        /// <summary>
        /// The dto converter map
        /// </summary>
        private Dictionary<Type, IDataConverter> dtoConverterMap;

        #region Private Constructor(s).        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseConverter{TConverters}"/> class.
        /// </summary>
        public BaseConverter()
        {
            dataConverterMap = new Dictionary<Type, IDataConverter>();
            dtoConverterMap = new Dictionary<Type, IDataConverter>();
            Cache = new ShareDataConverterCache();
            //Init(context);
        }
        #endregion

        #region Public Property(s).        
        /// <summary>
        /// Gets the cache of data converters.
        /// </summary>
        /// <value>
        /// The cache.
        /// </value>
        public ShareDataConverterCache Cache { get; private set; }
        #endregion

        #region Public Static Property(s).        
        /// <summary>
        /// Creates a new converter.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static TConverters NewConvertion(IDomainContext context = null)
        {
            var c = new TConverters();
            c.Init(context);
            return c;
        }
        #endregion

        #region Public Member(s).
        public IDataConverter GetConverterFromData(Type type)
        {
            return dataConverterMap[type];
        }

        public IDataConverter GetConverterFromDTO(Type type)
        {
            return dtoConverterMap[type];
        }
        #endregion

        #region Private Member(s).        
        /// <summary>
        /// Registers the specified converter.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <typeparam name="TDTO">The type of the dto.</typeparam>
        /// <typeparam name="TConverter">The type of the converter.</typeparam>
        /// <param name="converter">The converter.</param>
        /// <returns></returns>
        protected TConverter Register<TData, TDTO, TConverter>(TConverter converter)
            where TData : ITrackable<TData>, new()
            where TDTO : ITrackableDTO<TDTO>, new()
            where TConverter : IDataConverter
        {
            /// TODO - Dont do this. Should just us properties to construct elements.... faster.
            dataConverterMap[typeof(TData)] = converter;
            dtoConverterMap[typeof(TDTO)] = converter;

            return converter;
        }

        /// <summary>
        /// Initializes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected abstract void Init(IDomainContext context);
        #endregion
    }
}

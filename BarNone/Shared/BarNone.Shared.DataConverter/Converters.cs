using BarNone.Shared.Core;
using BarNone.TheRack.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverter
{
    public interface IConverter
    {
        ShareDataConverterCache Cache { get; }
    }

    public abstract class BaseConverter<TConverters> : IConverter
        where TConverters : BaseConverter<TConverters>, new()
    {
        private Dictionary<Type, IDataConverter> dataConverterMap;
        private Dictionary<Type, IDataConverter> dtoConverterMap;

        #region Private Constructor(s).
        public BaseConverter()
        {
            dataConverterMap = new Dictionary<Type, IDataConverter>();
            dtoConverterMap = new Dictionary<Type, IDataConverter>();
            Cache = new ShareDataConverterCache();
            Init();
        }
        #endregion

        #region Public Property(s).
        public ShareDataConverterCache Cache { get; private set; }
        #endregion

        #region Public Static Property(s).
        public static TConverters Convert
        {
            get
            {
                return new TConverters();
            }
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

        protected abstract void Init();
        #endregion
    }
}

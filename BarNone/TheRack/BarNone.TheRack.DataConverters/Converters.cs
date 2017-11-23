using BarNone.Shared.Core;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.LiftData;
using BarNone.TheRack.DataConverters.Body;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.DomainModel.Body;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DataConverters
{
    public class Converters : IConverter
    {
        private Dictionary<Type, IDataConverter> dataConverterMap;
        private Dictionary<Type, IDataConverter> dtoConverterMap;

        #region Private Constructor(s).
        private Converters()
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

        #region Converter(s).
        public UserConverter User { get; private set; }

        public LiftConverter Lift { get; private set; }

        public LiftFolderConverter LiftFolder { get; private set; } 

        public BodyDataConverter BodyData { get; private set; }

        public BodyDataFrameConverter BodyDataFrame { get; private set; }

        public JointConverter Joint { get; private set; }

        public JointTypeConverter JointType { get; private set; }

        public JointTrackingStateTypeConverter JointTrackingStateType { get; private set; }
        #endregion

        #region Public Static Property(s).
        public static Converters Convert
        {
            get
            {
                return new Converters();
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

        #region Private Member(s        
        private TConverter Register<TData, TDTO, TConverter>(TConverter converter)
            where TData : ITrackable<TData>, new()
            where TDTO : ITrackableDTO<TDTO>, new()
            where TConverter : IDataConverter
        {
            /// TODO - Dont do this. Should just us properties to construct elements.... faster.
            dataConverterMap[typeof(TData)] = converter;
            dtoConverterMap[typeof(TDTO)] = converter;

            return converter;
        }

        private void Init()
        {
            /// TODO - pls no.
            User = Register<User, UserDTO, UserConverter>(new UserConverter(this));
            Lift = Register<Lift, LiftDTO, LiftConverter>(new LiftConverter(this));
            LiftFolder = Register<LiftFolder, LiftFolderDTO, LiftFolderConverter>(new LiftFolderConverter(this));
            BodyData = Register<BodyData, BodyDataDTO, BodyDataConverter>(new BodyDataConverter(this));
            BodyDataFrame = Register<BodyDataFrame, BodyDataFrameDTO, BodyDataFrameConverter>(new BodyDataFrameConverter(this));
            Joint = Register<Joint, JointDTO, JointConverter>(new JointConverter(this));
            JointType = Register<JointType, JointTypeDTO, JointTypeConverter>(new JointTypeConverter(this));
            JointTrackingStateType = Register<JointTrackingStateType, JointTrackingStateTypeDTO, JointTrackingStateTypeConverter>(new JointTrackingStateTypeConverter(this));
            //Joint = Register<User, UserDTO, UserConverter>(new UserConverter(this));
        } 
        #endregion
    }
}

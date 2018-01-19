using BarNone.DataLift.DataConverters.KinectData;
using BarNone.DataLift.DataModel.KinectData;
using BarNone.Shared.Core;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;

namespace BarNone.DataLift.DataConverters
{
    public class Converters : BaseConverter<Converters>
    {
        #region Converter(s).
        public BodyDataConverter BodyData { get; private set; }
        public BodyDataFrameConverter BodyDataFrame { get; private set; }
        #endregion

        protected override void Init(IDomainContext context)
        {
            BodyData = Register<BodyData, BodyDataDTO, BodyDataConverter>(new BodyDataConverter(this, context));
            BodyDataFrame = Register<BodyDataFrame, BodyDataFrameDTO, BodyDataFrameConverter>(new BodyDataFrameConverter(this, context));
        }
    }
}

using BarNone.DataLift.DataConverters.KinectData;
using BarNone.DataLift.DataModel.KinectData;
using BarNone.Shared.Core;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;

namespace BarNone.DataLift.DataConverters
{
    /// <summary>
    /// Class holding DataLift required converters to transform between DataLift and Shared structures
    /// </summary>
    public class Converters : BaseConverter<Converters>
    {
        #region Converter(s).
        /// <summary>
        /// Converter for BodyData
        /// </summary>
        public BodyDataConverter BodyData { get; private set; }
        /// <summary>
        /// Converter for individual data frames
        /// </summary>
        public BodyDataFrameConverter BodyDataFrame { get; private set; }
        #endregion

        /// <summary>
        /// Registers the converters
        /// </summary>
        /// <param name="context">Context owned by Shared</param>
        protected override void Init(IDomainContext context)
        {
            BodyData = Register<BodyData, BodyDataDTO, BodyDataConverter>(new BodyDataConverter(this, context));
            BodyDataFrame = Register<BodyDataFrame, BodyDataFrameDTO, BodyDataFrameConverter>(new BodyDataFrameConverter(this, context));
        }
    }
}

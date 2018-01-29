using BarNone.DataLift.DataModel.KinectData;
using BarNone.Shared.Core;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using System.Linq;

namespace BarNone.DataLift.DataConverters.KinectData
{
    /// <summary>
    /// Body data DM and DTO transforms
    /// </summary>
    public class BodyDataConverter : BaseDetailDataConverter<BodyData, BodyDataDTO, BodyDataDetailDTO, Converters>
    {
        public BodyDataConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        /// <summary>
        /// Dto to BodyData
        /// </summary>
        /// <param name="dto">Dto to transform</param>
        /// <returns>dto as BodyData</returns>
        public override BodyData OnCreateDataModel(BodyDataDTO dto)
        {
            return new BodyData
            {
                ID = dto.ID,
                RecordDate = dto.RecordTimeStamp
            };
        }

        /// <summary>
        /// Populates details of data from dto
        /// </summary>
        /// <param name="data">BodyData being populated</param>
        /// <param name="dto">dto being transformed</param>
        public override void OnCreateDetailDataModel(BodyData data, BodyDataDetailDTO dto)
        {
            data.DataFrames = dto.OrderedFrames.Select(f => converterContext.BodyDataFrame.CreateDataModel(f)).ToList();
        }

        /// <summary>
        /// Translates data into its details DTO
        /// </summary>
        /// <param name="data">Data to transform</param>
        /// <returns>data as a BodyDataDetailDTO</returns>
        public override BodyDataDetailDTO OnCreateDetailDTO(BodyData data)
        {
            return new BodyDataDetailDTO
            {
                OrderedFrames = data.DataFrames.Select(f => converterContext.BodyDataFrame.CreateDTO(f)).ToList()
            };
        }

        /// <summary>
        /// Translates data to its dto representation
        /// </summary>
        /// <param name="data">Data to transform</param>
        /// <returns>data as a BodyDataDTO</returns>
        public override BodyDataDTO OnCreateDTO(BodyData data)
        {
            return new BodyDataDTO
            {
                ID = data.ID,
                RecordTimeStamp = data.RecordDate
            };
        }
    }
}

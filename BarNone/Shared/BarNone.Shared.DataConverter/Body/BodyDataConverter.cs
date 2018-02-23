using BarNone.Shared.Core;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    /// <summary>
    /// Body data converter.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataConverter.Core.BaseDetailDataConverter{BarNone.Shared.DomainModel.BodyData, BarNone.Shared.DataTransfer.BodyDataDTO, BarNone.Shared.DataTransfer.BodyDataDetailDTO, BarNone.Shared.DataConverters.Converters}" />
    public class BodyDataConverter : BaseDetailDataConverter<BodyData, BodyDataDTO, BodyDataDetailDTO, Converters>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyDataConverter"/> class.
        /// </summary>
        /// <param name="converterContext">The converter context.</param>
        /// <param name="context">The context.</param>
        public BodyDataConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        /// <summary>
        /// Creates data model.
        /// Called when [create data model].
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override BodyData OnCreateDataModel(BodyDataDTO dto)
        {
            return new BodyData
            {
                ID = dto.ID,
                RecordDate = dto.RecordTimeStamp,
                UserID = context != null ? context.UserID : 0
            };
        }

        /// <summary>
        /// Creates detail data model.
        /// Called when [create detail data model].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="dto">The dto.</param>
        public override void OnCreateDetailDataModel(BodyData data, BodyDataDetailDTO dto)
        {
            data.BodyDataFrames = 
                dto.OrderedFrames?.Select(f => converterContext.BodyDataFrame.CreateDataModel(f)).ToList();
        }

        /// <summary>
        /// Creates detail dto.
        /// Called when [create detail dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override BodyDataDetailDTO OnCreateDetailDTO(BodyData data)
        {
            return new BodyDataDetailDTO
            {
                OrderedFrames = data.BodyDataFrames?.Select(f => converterContext.BodyDataFrame.CreateDTO(f)).ToList()
            };
        }

        /// <summary>
        /// Creates dto.
        /// Called when [create dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
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

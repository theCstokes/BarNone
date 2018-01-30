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
    /// Body data frame converter
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataConverter.Core.BaseDetailDataConverter{BarNone.Shared.DomainModel.BodyDataFrame, BarNone.Shared.DataTransfer.BodyDataFrameDTO, BarNone.Shared.DataTransfer.BodyDataFrameDetailDTO, BarNone.Shared.DataConverters.Converters}" />
    public class BodyDataFrameConverter
        : BaseDetailDataConverter<BodyDataFrame, BodyDataFrameDTO, BodyDataFrameDetailDTO, Converters>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyDataFrameConverter"/> class.
        /// </summary>
        /// <param name="converterContext">The converter context.</param>
        /// <param name="context">The context.</param>
        public BodyDataFrameConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        /// <summary>
        /// Creates data model.
        /// Called when [create data model].
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override BodyDataFrame OnCreateDataModel(BodyDataFrameDTO dto)
        {
            return new BodyDataFrame
            {
                ID = dto.ID,
                TimeOfFrame = dto.TimeOfFrame,
                BodyDataID = dto.BodyDataID,
                UserID = context != null ? context.UserID : 0
            };
        }

        /// <summary>
        /// Creates detail data model.
        /// Called when [create detail data model].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="dto">The dto.</param>
        public override void OnCreateDetailDataModel(BodyDataFrame data, BodyDataFrameDetailDTO dto)
        {
            data.BodyData = converterContext.BodyData.CreateDataModel(dto.BodyData);
            data.Joints = dto.Joints.Select(j => converterContext.Joint.CreateDataModel(j)).ToList();
        }

        /// <summary>
        /// Creates detail dto.
        /// Called when [create detail dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override BodyDataFrameDetailDTO OnCreateDetailDTO(BodyDataFrame data)
        {
            return new BodyDataFrameDetailDTO
            {
                BodyData = converterContext.BodyData.CreateDTO(data.BodyData),
                Joints = data.Joints.Select(j => converterContext.Joint.CreateDTO(j)).ToList()
            };
        }

        /// <summary>
        /// Creates dto.
        /// Called when [create dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override BodyDataFrameDTO OnCreateDTO(BodyDataFrame data)
        {
            return new BodyDataFrameDTO
            {
                ID = data.ID,
                BodyDataID = data.BodyDataID,
                TimeOfFrame = data.TimeOfFrame
            };
        }
    }
}

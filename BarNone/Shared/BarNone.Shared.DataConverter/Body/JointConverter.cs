using BarNone.Shared.Core;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    /// <summary>
    /// Joint converter.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataConverter.Core.BaseDetailDataConverter{BarNone.Shared.DomainModel.Joint, BarNone.Shared.DataTransfer.JointDTO, BarNone.Shared.DataTransfer.JointDetailDTO, BarNone.Shared.DataConverters.Converters}" />
    public class JointConverter :
        BaseDetailDataConverter<Joint, JointDTO, JointDetailDTO, Converters>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JointConverter"/> class.
        /// </summary>
        /// <param name="converterContext">The converter context.</param>
        /// <param name="context">The context.</param>
        public JointConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        /// <summary>
        /// Creates data model.
        /// Called when [create data model].
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override Joint OnCreateDataModel(JointDTO dto)
        {
            return new Joint
            {
                ID = dto.ID,
                X = dto.X,
                Y = dto.Y,
                Z = dto.Z,
                BodyDataFrameID = dto.BodyDataFrameID,
                JointTrackingStateTypeID = dto.JointTrackingStateTypeID,
                JointTypeID = dto.JointTypeID,
                UserID = context.UserID
            };
        }

        /// <summary>
        /// Creates detail data model.
        /// Called when [create detail data model].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="dto">The dto.</param>
        public override void OnCreateDetailDataModel(Joint data, JointDetailDTO dto)
        {
            data.BodyDataFrame = converterContext.BodyDataFrame.CreateDataModel(dto.BodyDataFrame);
            data.JointTrackingStateType = converterContext.JointTrackingStateType.CreateDataModel(dto.JointTrackingStateType);
            data.JointType = converterContext.JointType.CreateDataModel(dto.JointType);
        }

        /// <summary>
        /// Creates detail dto.
        /// Called when [create detail dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override JointDetailDTO OnCreateDetailDTO(Joint data)
        {
            return new JointDetailDTO
            {
                BodyDataFrame = converterContext.BodyDataFrame.CreateDTO(data.BodyDataFrame),
                JointTrackingStateType = converterContext.JointTrackingStateType.CreateDTO(data.JointTrackingStateType),
                JointType = converterContext.JointType.CreateDTO(data.JointType)
            };
        }

        /// <summary>
        /// Creates dto.
        /// Called when [create dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override JointDTO OnCreateDTO(Joint data)
        {
            return new JointDTO
            {
                ID = data.ID,
                X = data.X,
                Y = data.Y,
                Z = data.Z,
                BodyDataFrameID = data.BodyDataFrameID,
                JointTrackingStateTypeID = data.JointTrackingStateTypeID,
                JointTypeID = data.JointTypeID
            };
        }
    }
}

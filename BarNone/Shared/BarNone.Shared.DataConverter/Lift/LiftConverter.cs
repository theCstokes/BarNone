using BarNone.Shared.Core;
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
    /// Lift converter.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataConverter.Core.BaseDetailDataConverter{BarNone.Shared.DomainModel.Lift, BarNone.Shared.DataTransfer.LiftDTO, BarNone.Shared.DataTransfer.LiftDetailDTO, BarNone.Shared.DataConverters.Converters}" />
    public class LiftConverter : BaseDetailDataConverter<Lift, LiftDTO, LiftDetailDTO, Converters>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiftConverter"/> class.
        /// </summary>
        /// <param name="converterContext">The converter context.</param>
        /// <param name="context">The context.</param>
        public LiftConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        /// <summary>
        /// Creates data model.
        /// Called when [create data model].
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override Lift OnCreateDataModel(LiftDTO dto)
        {
            return new Lift
            {
                ID = dto.ID,
                Name = dto.Name,
                ParentID = dto.ParentID,
                BodyDataID = dto.BodyDataID,
                UserID = context != null ? context.UserID : 0 ,
                LiftTypeID = dto.LiftTypeID
            };
        }

        /// <summary>
        /// Creates detail data model.
        /// Called when [create detail data model].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="dto">The dto.</param>
        public override void OnCreateDetailDataModel(Lift data, LiftDetailDTO dto)
        {
            data.Parent = converterContext.LiftFolder.CreateDataModel(dto.Parent);
            data.BodyData = converterContext.BodyData.CreateDataModel(dto.BodyData);
            data.Video = converterContext.Video.CreateDataModel(dto.Video);
            data.Permissions = dto?.Permissions?.Select(p => converterContext.LiftPermission.CreateDataModel(p)).ToList();
            data.LiftType = converterContext.LiftType.CreateDataModel(dto.LiftType);
        }

        /// <summary>
        /// Creates detail dto.
        /// Called when [create detail dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override LiftDetailDTO OnCreateDetailDTO(Lift data)
        {
            return new LiftDetailDTO
            {
                Parent = converterContext.LiftFolder.CreateDTO(data.Parent),
                BodyData = converterContext.BodyData.CreateDTO(data.BodyData),
                Video = converterContext.Video.CreateDTO(data.Video),
                Permissions = data?.Permissions?.Select(p => converterContext.LiftPermission.CreateDTO(p)).ToList(),
                LiftType = converterContext.LiftType.CreateDTO(data.LiftType)
            };
        }

        /// <summary>
        /// Creates dto.
        /// Called when [create dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override LiftDTO OnCreateDTO(Lift data)
        {
            return new LiftDTO
            {
                ID = data.ID,
                Name = data.Name,
                ParentID = data.ParentID,
                BodyDataID = data.BodyDataID,
                LiftTypeID = data.LiftTypeID
            };
        }
    }
}

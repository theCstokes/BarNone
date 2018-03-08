using BarNone.Shared.Core;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverter
{
    /// <summary>
    /// Video converter.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataConverter.Core.BaseDataConverter{BarNone.Shared.DomainModel.VideoRecord, BarNone.Shared.DataTransfer.VideoDTO, BarNone.Shared.DataConverters.Converters}" />
    public class VideoConverter : BaseDataConverter<VideoRecord, VideoDTO, Converters>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoConverter"/> class.
        /// </summary>
        /// <param name="converters">The converters.</param>
        /// <param name="context">The context.</param>
        public VideoConverter(Converters converters, IDomainContext context) : base(converters, context)
        {
        }

        /// <summary>
        /// Creates data model.
        /// Called when [create data model].
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override VideoRecord OnCreateDataModel(VideoDTO dto)
        {
            return new VideoRecord
            {
                ID = dto.ID,
                Data = dto.Data,
                UserID = context.UserID
            };
        }

        /// <summary>
        /// Creates dto.
        /// Called when [create dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override VideoDTO OnCreateDTO(VideoRecord data)
        {
            return new VideoDTO
            {
                ID = data.ID,
                Data = data.Data
            };
        }
    }
}

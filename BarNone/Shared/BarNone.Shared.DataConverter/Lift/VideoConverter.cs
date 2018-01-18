using BarNone.Shared.Core;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverter.Lift
{
    public class VideoConverter : BaseDataConverter<VideoRecord, VideoDTO, Converters>
    {
        public VideoConverter(Converters converters, IDomainContext context) : base(converters, context)
        {
        }

        public override VideoRecord OnCreateDataModel(VideoDTO dto)
        {
            return new VideoRecord
            {
                ID = dto.ID,
                Data = dto.Data,
                UserID = context.UserID
            };
        }

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

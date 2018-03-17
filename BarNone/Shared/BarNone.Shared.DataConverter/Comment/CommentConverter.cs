using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.Shared.Core;

namespace BarNone.Shared.DataConverter
{
    public class CommentConverter : BaseDetailDataConverter<Comment, CommentDTO, CommentDetailDTO, Converters>
    {
        public CommentConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override Comment OnCreateDataModel(CommentDTO dto)
        {
            return new Comment
            {
                ID = dto.ID,
                UserID = context != null ? context.UserID : 0,
                LiftID = dto.LiftID,
                TimeSent = dto.TimeSent,
                Text = dto.Text
            };
        }

        public override void OnCreateDetailDataModel(Comment data, CommentDetailDTO dto)
        {
            data.Lift = converterContext.Lift.CreateDataModel(dto.Lift);
            // Do not allow user to come in
        }

        public override CommentDetailDTO OnCreateDetailDTO(Comment data)
        {
            return new CommentDetailDTO
            {
                Lift = converterContext.Lift.CreateDTO(data.Lift),
                SentUser = converterContext.User.CreateDTO(data.User)
            };
        }

        public override CommentDTO OnCreateDTO(Comment data)
        {
            return new CommentDTO
            {
                ID = data.ID,
                LiftID = data.LiftID,
                SentUserID = data.UserID,
                TimeSent = data.TimeSent,
                Text = data.Text
            };
        }
    }
}

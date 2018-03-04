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
    public class NotificationConverter : BaseDataConverter<Notification, NotificationDTO, Converters>
    {
        public NotificationConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        public override Notification OnCreateDataModel(NotificationDTO dto)
        {
            return new Notification
            {
                ID = dto.ID,
                NotificationStatusTypeID = dto.NotificationStatusTypeID,
                UserID = context == null ? 0 : context.UserID
            };
        }

        public override NotificationDTO OnCreateDTO(Notification data)
        {
            return new NotificationDTO
            {
                ID = data.ID,
                NotificationStatusTypeID = data.NotificationStatusTypeID
            };
        }
    }
}

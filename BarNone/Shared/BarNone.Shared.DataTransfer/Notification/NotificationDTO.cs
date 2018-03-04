using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    public class NotificationDTO : BaseDTO<NotificationDTO>
    {
        public override int ID { get; set; }

        public int UserID { get; set; }

        public int NotificationStatusTypeID { get; set; }

        public string Name { get; set; }
    }
}

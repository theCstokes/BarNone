using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.Shared.DomainModel
{
    [Table("Notification", Schema = "public")]
    public class Notification : IDomainModel<Notification>, IOwnedDomainModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }
     
        public int NotificationStatusTypeID { get; set; }

        [ForeignKey("NotificationStatusTypeID")]
        public NotificationStatusType NotificationStatusType { get; set; }
    }
}

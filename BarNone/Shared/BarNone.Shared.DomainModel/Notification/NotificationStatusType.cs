using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.Shared.DomainModel
{
    [Table("NotificationStatusType", Schema = "public")]
    public class NotificationStatusType : BaseEnumDomainModel<NotificationStatusType, ENotificationStatusType>
    {
        #region Public Constructor(s).        
        public NotificationStatusType(): base()
        {
        }
        
        public NotificationStatusType(ENotificationStatusType @enum): base(@enum)
        {
        }
        #endregion

        #region Public Operator Overload(s).        
        public static implicit operator NotificationStatusType(ENotificationStatusType @enum) => new NotificationStatusType(@enum);

        public static implicit operator ENotificationStatusType(NotificationStatusType joint) => (ENotificationStatusType)joint.Value;
        #endregion
    }
}

using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.Repository
{
    public class NotificationRepository : DefaultRepository<Notification, NotificationDTO>
    {

        public NotificationRepository() : base()
        {
        }
        
        public NotificationRepository(DomainContext context) : base(context)
        {
        }

        protected override ConverterResolverDelegate<Notification, NotificationDTO> DataConverter =>
            Converters.NewConvertion(context).Notification.CreateDataModel;

        protected override SetResolverDelegate<Notification> SetResolver => (context) => context.Notifications;

        protected override EntityResolverDelegate<Notification> EntityResolver => (set) => set;

        public List<Notification> GetUnread()
        {
            return Entites
                .Where(n => n.NotificationStatusType == ENotificationStatusType.Sent)
                .ToList();
        }
    }
}

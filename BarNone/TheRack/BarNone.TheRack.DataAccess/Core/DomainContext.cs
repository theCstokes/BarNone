using BarNone.Shared.Core;
using BarNone.TheRack.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BarNone.TheRack.DataAccess
{
    /// <summary>
    /// Context for domain entites.
    /// </summary>
    /// <seealso cref="BarNone.TheRack.DataAccess.BaseDomainContext" />
    /// <seealso cref="BarNone.Shared.Core.IDomainContext" />
    public partial class DomainContext : BaseDomainContext, IDomainContext
    {
        /// <summary>
        /// Maps builder for model.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public delegate void ModelMapAction(ModelBuilder builder);

        /// <summary>
        /// Occurs when [on model map].
        /// </summary>
        private static event ModelMapAction OnModelMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainContext"/> class.
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        public DomainContext(int UserID = 0)
        {
            this.UserID = UserID;
            notificationProviders = new List<Action>();
        }

        /// <summary>
        /// Creates the model mapping.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        protected static ModelMapAction CreateModelMapping(ModelMapAction action)
        {
            DomainContext.OnModelMap += action;
            return action;
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelMap(modelBuilder);
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserID { get; set; }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();

            notificationProviders.ForEach(a => a());

            return result;
        }

        private List<Action> notificationProviders;

        public void NotifyOnSave<TDTO>(NotificationProvider<TDTO> manager, TDTO entity)
        {
            notificationProviders.Add(() => manager.Run(UserID, entity));
        }

        //public void SaveAndNotifiy()
        //{
        //    SaveChanges();
        //}
    }
}

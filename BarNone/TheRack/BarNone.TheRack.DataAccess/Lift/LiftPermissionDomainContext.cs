using BarNone.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DataAccess
{
    public partial class DomainContext
    {
        /// <summary>
        /// Lift db entities.
        /// </summary>
        /// <value>
        /// The lifts.
        /// </value>
        public DbSet<LiftPermission> LiftPermissions { get; set; }

        /// <summary>
        /// The lift parent bind
        /// </summary>
        private ModelMapAction liftPermissionLiftDetailBind = CreateModelMapping(builder => builder.Entity<LiftPermission>()
                .HasOne(r => r.Lift));

        /// <summary>
        /// The lift parent bind
        /// </summary>
        private ModelMapAction liftPermissionUserDetailBind = CreateModelMapping(builder => builder.Entity<LiftPermission>()
                .HasOne(r => r.User));
    }
}

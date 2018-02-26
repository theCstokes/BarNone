using BarNone.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
        public DbSet<Lift> Lifts { get; set; }

        /// <summary>
        /// The lift parent bind
        /// </summary>
        private ModelMapAction liftParentBind = CreateModelMapping(builder => builder.Entity<Lift>()
                .HasOne(r => r.Parent)
                .WithMany(rc => rc.Lifts));

        private ModelMapAction liftPermissionsBind = CreateModelMapping(builder => builder.Entity<Lift>()
                .HasMany(l => l.Permissions)
                .WithOne(p => p.Lift));
    }
}

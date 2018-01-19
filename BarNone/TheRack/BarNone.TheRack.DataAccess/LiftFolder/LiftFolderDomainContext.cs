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
        /// LiftFolder db entities.
        /// </summary>
        /// <value>
        /// The lift folders.
        /// </value>
        public DbSet<LiftFolder> LiftFolders { get; set; }

        /// <summary>
        /// The lift folder child bind
        /// </summary>
        private ModelMapAction liftFolderChildBind = CreateModelMapping(builder => builder.Entity<LiftFolder>()
                .HasMany(lf => lf.Lifts)
                .WithOne(l => l.Parent));

        /// <summary>
        /// The lift folder parent bind
        /// </summary>
        private ModelMapAction liftFolderParentBind = CreateModelMapping(builder => builder.Entity<LiftFolder>()
                .HasOne(lf => lf.Parent)
                .WithMany(lf => lf.SubFolders));
    }
}

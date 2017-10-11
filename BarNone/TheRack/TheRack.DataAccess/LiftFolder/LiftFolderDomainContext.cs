using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TheRack.DomainModel;

namespace TheRack.DataAccess
{
    public partial class DomainContext
    {
        public DbSet<LiftFolder> LiftFolders { get; set; }

        private ModelMapAction liftFolderChildBind = CreateModelMapping(builder => builder.Entity<LiftFolder>()
                .HasMany(lf => lf.Lifts)
                .WithOne(l => l.Parent));

        private ModelMapAction liftFolderParentBind = CreateModelMapping(builder => builder.Entity<LiftFolder>()
                .HasOne(lf => lf.Parent)
                .WithMany(lf => lf.SubFolders));
    }
}

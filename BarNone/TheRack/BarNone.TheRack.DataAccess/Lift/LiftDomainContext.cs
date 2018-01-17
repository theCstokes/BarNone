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
        public DbSet<Lift> Lifts { get; set; }

        private ModelMapAction liftParentBind = CreateModelMapping(builder => builder.Entity<Lift>()
                .HasOne(r => r.Parent)
                .WithMany(rc => rc.Lifts));
    }
}

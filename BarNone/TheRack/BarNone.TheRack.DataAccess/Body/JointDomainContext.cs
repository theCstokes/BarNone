using BarNone.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DataAccess
{
    public partial class DomainContext
    {
        public virtual DbSet<Joint> Joints { get; set; }

        private ModelMapAction Joint_BodyDataFrame_Bind
            = CreateModelMapping(builder => builder.Entity<Joint>()
                .HasOne(joint => joint.BodyDataFrame));
    }
}

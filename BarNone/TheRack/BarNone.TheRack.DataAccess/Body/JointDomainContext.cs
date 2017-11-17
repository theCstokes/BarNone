using BarNone.TheRack.DomainModel.Body;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static BarNone.TheRack.DataAccess.DomainContext;

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

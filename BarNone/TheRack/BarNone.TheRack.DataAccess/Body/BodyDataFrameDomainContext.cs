using BarNone.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static BarNone.TheRack.DataAccess.DomainContext;

namespace BarNone.TheRack.DataAccess
{
    public partial class DomainContext
    {
        public virtual DbSet<BodyDataFrame> BodyDataFrames { get; set; }

        private ModelMapAction BodyDataFrame_BodyData_Bind
            = CreateModelMapping(builder => builder.Entity<BodyDataFrame>()
                .HasOne(frame => frame.BodyData));
    }
}

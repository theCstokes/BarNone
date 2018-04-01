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
        /// Lift type db entities.
        /// </summary>
        /// <value>
        /// The lift types.
        /// </value>
        public DbSet<LiftAnalysisProfile> LiftAnalysisProfiles { get; set; }

        private ModelMapAction LiftAnalysisProfilesBind = CreateModelMapping(builder => builder.Entity<Lift>()
                .HasOne(r => r.Parent)
                .WithMany(rc => rc.Lifts));

        public DbSet<AccelerationAnalysisCriteria> AccelerationAnalysisProfiles { get; set; }
    }
}

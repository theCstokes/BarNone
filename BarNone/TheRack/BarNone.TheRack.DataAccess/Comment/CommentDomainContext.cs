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
        /// Comment db entities.
        /// </summary>
        /// <value>
        /// The lift folders.
        /// </value>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// The comment child bind
        /// </summary>
        private ModelMapAction commentChildBind = CreateModelMapping(builder => builder.Entity<Comment>()
                .HasOne(c => c.Lift));
    }
}

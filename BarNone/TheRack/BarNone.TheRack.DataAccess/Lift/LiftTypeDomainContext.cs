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
        public DbSet<LiftType> LiftTypes { get; set; }
    }
}

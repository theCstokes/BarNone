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
        /// JointType db entities.
        /// </summary>
        /// <value>
        /// The joint types.
        /// </value>
        public virtual DbSet<JointType> JointTypes { get; set; }
    }
}

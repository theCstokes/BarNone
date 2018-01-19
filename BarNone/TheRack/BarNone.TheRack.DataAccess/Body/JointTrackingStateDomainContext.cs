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
        /// JointTrackingStateType db entities.
        /// </summary>
        /// <value>
        /// The tracking states.
        /// </value>
        public virtual DbSet<JointTrackingStateType> TrackingStates { get; set; }
    }
}

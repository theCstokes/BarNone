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
        /// User db entities.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual DbSet<User> Users { get; set; }
    }
}

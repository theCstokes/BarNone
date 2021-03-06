﻿using BarNone.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DataAccess
{
    public partial class DomainContext
    {
        /// <summary>
        /// BodyData db entities.
        /// </summary>
        /// <value>
        /// The bodies.
        /// </value>
        public virtual DbSet<BodyData> Bodies { get; set; }
    }
}

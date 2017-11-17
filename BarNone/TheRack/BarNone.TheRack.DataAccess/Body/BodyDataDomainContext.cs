﻿using BarNone.TheRack.DomainModel;
using BarNone.TheRack.DomainModel.Body;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DataAccess
{
    public partial class DomainContext
    {
        public virtual DbSet<BodyData> Bodies { get; set; }
    }
}

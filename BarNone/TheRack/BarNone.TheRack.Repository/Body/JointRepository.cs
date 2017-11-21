﻿using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DomainModel.Body;
using BarNone.TheRack.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.TheRack.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BarNone.TheRack.Repository.Body
{
    public class JointRepository : DefaultDetailRepository<JointDTO, Joint>
    {
        public JointRepository() : base(
            new DomainContext(),
            c => c.Joints,
            c => c.Include(j => j.BodyDataFrame),
            c => c.Include(j => j.JointType),
            c => c.Include(j => j.JointTrackingStateType))
        {
        }

        public JointRepository(DomainContext context) : base(
            context,
            c => c.Joints,
            c => c.Include(j => j.BodyDataFrame),
            c => c.Include(j => j.JointType),
            c => c.Include(j => j.JointTrackingStateType))
        {

        }
    }
}

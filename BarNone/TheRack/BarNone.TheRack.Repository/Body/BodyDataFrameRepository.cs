﻿using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DataConverters;
using BarNone.TheRack.DomainModel.Body;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.Repository
{
    public class BodyDataFrameRepository : DefaultDetailRepository<BodyDataFrame, BodyDataFrameDTO, BodyDataFrameDetailDTO>
    {
        public BodyDataFrameRepository() : base(
            () => new ConvertConfig(),
            c => c.BodyDataFrames,
            s => s.Include(b => b.Joints),
            s => s.Include(b => b.BodyData))
        {
        }

        public BodyDataFrameRepository(DomainContext context) : base(
            context,
            () => new ConvertConfig(),
            c => c.BodyDataFrames,
            s => s.Include(b => b.Joints),
            s => s.Include(b => b.BodyData))
        {
        }

        protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.BodyDataFrame;

        protected override DetailResolver DetailDataResolver => throw new NotImplementedException();
    }
}

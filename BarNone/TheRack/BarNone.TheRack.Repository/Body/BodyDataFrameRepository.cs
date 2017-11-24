using BarNone.Shared.DataTransfer;
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
        public BodyDataFrameRepository() : base()
        {
        }

        public BodyDataFrameRepository(DomainContext context) : base(context)
        {
        }

        protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.BodyDataFrame;

        protected override DbSetResolver SetResolver => (config) => config.BodyDataFrames;

        protected override Resolver DetailDataResolver => (s) => s.Include(f => f.BodyData)
                .Include(l => l.Joints).ThenInclude(j => j.JointType)
                .Include(l => l.Joints).ThenInclude(j => j.JointTrackingStateType);
    }
}

using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.Shared.DataTransfer.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DataConverters;

namespace BarNone.TheRack.Repository
{
    public class BodyDataRepository : DefaultDetailRepository<BodyData, BodyDataDTO, BodyDataDetailDTO>
    {
        public BodyDataRepository() : base()
        {
        }

        public BodyDataRepository(DomainContext context) : base(context)
        {

        }

        protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.BodyData;

        protected override DbSetResolver SetResolver => (context) => context.Bodies;

        protected override Resolver DetailDataResolver => (s) => s
                .Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints).ThenInclude(j => j.JointType)
                .Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints).ThenInclude(j => j.JointTrackingStateType);
    }
}

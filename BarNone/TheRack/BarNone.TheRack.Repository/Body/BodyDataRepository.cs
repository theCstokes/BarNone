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
        public BodyDataRepository() : base(
            () => new ConvertConfig(),
            c => c.Bodies,
            s => s.Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints).ThenInclude(j => j.JointType))
        {
        }

        public BodyDataRepository(DomainContext context) : base(
            context,
            () => new ConvertConfig(),
            c => c.Bodies,
            s => s.Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints).ThenInclude(j => j.JointType))
        {

        }

        protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.BodyData;
    }
}

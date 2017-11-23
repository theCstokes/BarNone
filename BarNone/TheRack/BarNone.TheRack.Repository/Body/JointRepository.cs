using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DomainModel.Body;
using BarNone.TheRack.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.TheRack.DataAccess;
using Microsoft.EntityFrameworkCore;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DataConverters;

namespace BarNone.TheRack.Repository.Body
{
    public class JointRepository : DefaultDetailRepository<Joint, JointDTO, JointDetailDTO>
    {
        public JointRepository() : base(
            () => new ConvertConfig(),
            c => c.Joints,
            c => c.Include(j => j.BodyDataFrame),
            c => c.Include(j => j.JointType),
            c => c.Include(j => j.JointTrackingStateType))
        {
        }

        public JointRepository(DomainContext context) : base(
            context,
            () => new ConvertConfig(),
            c => c.Joints,
            c => c.Include(j => j.BodyDataFrame),
            c => c.Include(j => j.JointType),
            c => c.Include(j => j.JointTrackingStateType))
        {

        }

        protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.Joint;
    }
}

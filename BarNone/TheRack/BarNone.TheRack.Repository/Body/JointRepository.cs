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
        public JointRepository() : base()
        {
        }

        public JointRepository(DomainContext context) : base(context)
        {

        }

        protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.Joint;

        protected override DbSetResolver SetResolver => (context) => context.Joints;

        protected override Resolver DetailDataResolver => (s) => s.Include(j => j.BodyDataFrame)
                .Include(j => j.JointType)
                .Include(j => j.JointTrackingStateTypeID);
    }
}

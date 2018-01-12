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
using static BarNone.TheRack.Repository.Core.Resolvers;
using System.Linq;

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

        protected override ConverterResolverDelegate<Joint, JointDTO> DataConverter =>
            (dto) =>
            {
                var dm = Converters.Convert.Joint.CreateDataModel(dto);
                dm.UserID = context.UserID;
                return dm;
            };

        protected override DetailResolverDelegate<Joint> DetailEntityResolver => (s) => s.Include(j => j.BodyDataFrame)
                    .Include(j => j.JointType)
                    .Include(j => j.JointTrackingStateTypeID);

        protected override SetResolverDelegate<Joint> SetResolver => (context) => context.Joints;

        protected override EntityResolverDelegate<Joint> EntityResolver => (joints) => joints.Where(joint => joint.UserID == context.UserID);

        //    protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.Joint;

        //    protected override DbSetResolver SetResolver => (context) => context.Joints;

        //    protected override Resolver DetailDataResolver => (s) => s.Include(j => j.BodyDataFrame)
        //            .Include(j => j.JointType)
        //            .Include(j => j.JointTrackingStateTypeID);
        //}
    }
}

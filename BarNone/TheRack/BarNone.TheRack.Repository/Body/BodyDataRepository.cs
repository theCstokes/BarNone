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
using static BarNone.TheRack.Repository.Core.Resolvers;

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

        //protected override DetailConverterResolverDelegate<BodyData, BodyDataDTO, BodyDataDetailDTO, Converters> DetailDataConverterResolver =>
        //    () => Converters.Convert.BodyData;

        protected override ConverterResolverDelegate<BodyData, BodyDataDTO> DataConverter =>
            (dto) =>
            {
                var dm = Converters.Convert.BodyData.CreateDataModel(dto);
                dm.UserID = context.UserID;
                return dm;
            };

        protected override DetailResolverDelegate<BodyData> DetailEntityResolver => (s) => s
                .Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints).ThenInclude(j => j.JointType)
                .Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints).ThenInclude(j => j.JointTrackingStateType);

        protected override SetResolverDelegate<BodyData> SetResolver => (context) => context.Bodies;

        protected override EntityResolverDelegate<BodyData> EntityResolver =>
            (bodies) => bodies.Where(body => body.UserID == context.UserID);

        //protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.BodyData;

        //protected override DbSetResolver SetResolver => (context) => context.Bodies;

        //protected override Resolver DetailDataResolver => (s) => s
        //        .Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints).ThenInclude(j => j.JointType)
        //        .Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints).ThenInclude(j => j.JointTrackingStateType);
    }
}

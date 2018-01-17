using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Xml.Linq;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.Repository
{
    public class LiftRepository : DefaultDetailRepository<Lift, LiftDTO, LiftDetailDTO>
    {
        public LiftRepository() : base()
        {
        }

        public LiftRepository(DomainContext context) : base(context)
        {

        }

        //protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.Lift;

        //protected override DetailConverterResolverDelegate<Lift, LiftDTO, Converters> DetailDataConverterResolver => () => Converters.Convert.Lift;

        //protected override DetailConverterResolverDelegate<Lift, LiftDTO, LiftDetailDTO, Converters> DetailDataConverterResolver =>
        //    () => Converters.Convert.Lift;

        protected override ConverterResolverDelegate<Lift, LiftDTO> DataConverter =>
            (dto) =>
            {
                var dm = Converters.Convert.Lift.CreateDataModel(dto);
                dm.UserID = context.UserID;
                return dm;
            };

        //protected override DbSetResolver SetResolver => (context) => context.Lifts.Where(l => l.ID == context.UserID);

        protected override SetResolverDelegate<Lift> SetResolver => (context) => context.Lifts;

        //protected override Resolver DetailDataResolver => (s) => s.Include(u => u.Parent)
        //        .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints)
        //        .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints).ThenInclude(j => j.JointType)
        //        .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints).ThenInclude(j => j.JointTrackingStateType);

        protected override DetailResolverDelegate<Lift> DetailEntityResolver => (lifts) => lifts
                .Include(u => u.Parent)
                .Include(u => u.Video)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints).ThenInclude(j => j.JointType)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints).ThenInclude(j => j.JointTrackingStateType);

        protected override EntityResolverDelegate<Lift> EntityResolver => (lifts) => lifts.Where(l => l.UserID == context.UserID);

        //protected override DetailResolverDelegate<Lift> DetailEntityResolver => throw new NotImplementedException();
    }
}

using BarNone.TheRack.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Xml.Linq;
using static BarNone.Shared.DataTransfer.Core.FilterDTO;
using BarNone.Shared.DTOTransformable.Core;

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

        protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.Lift;

        protected override DbSetResolver SetResolver => (context) => context.Lifts;

        protected override Resolver DetailDataResolver => (s) => s.Include(u => u.Parent)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints).ThenInclude(j => j.JointType)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints).ThenInclude(j => j.JointTrackingStateType);
    }
}

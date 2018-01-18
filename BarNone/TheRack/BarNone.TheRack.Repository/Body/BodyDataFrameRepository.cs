using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BarNone.TheRack.Repository.Core.Resolvers;

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
        
        protected override ConverterResolverDelegate<BodyDataFrame, BodyDataFrameDTO> DataConverter => 
            Converters.NewConvertion(context).BodyDataFrame.CreateDataModel;

        protected override DetailResolverDelegate<BodyDataFrame> DetailEntityResolver =>
            (s) => s.Include(f => f.BodyData)
                .Include(l => l.Joints).ThenInclude(j => j.JointType)
                .Include(l => l.Joints).ThenInclude(j => j.JointTrackingStateType);

        protected override SetResolverDelegate<BodyDataFrame> SetResolver => 
            (context) => context.BodyDataFrames;

        protected override EntityResolverDelegate<BodyDataFrame> EntityResolver => 
            (set) => set.Where(frame => frame.UserID == context.UserID);

        //protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.BodyDataFrame;

        //protected override DbSetResolver SetResolver => (config) => config.BodyDataFrames;

        //protected override Resolver DetailDataResolver => (s) => s.Include(f => f.BodyData)
        //        .Include(l => l.Joints).ThenInclude(j => j.JointType)
        //        .Include(l => l.Joints).ThenInclude(j => j.JointTrackingStateType);
    }
}

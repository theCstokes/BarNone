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
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyDataFrameRepository"/> class.
        /// </summary>
        public BodyDataFrameRepository() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyDataFrameRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BodyDataFrameRepository(DomainContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the data converter.
        /// </summary>
        /// <value>
        /// The data converter.
        /// </value>
        protected override ConverterResolverDelegate<BodyDataFrame, BodyDataFrameDTO> DataConverter => 
            Converters.NewConvertion(context).BodyDataFrame.CreateDataModel;

        /// <summary>
        /// Gets the detail entity resolver.
        /// </summary>
        /// <value>
        /// The detail entity resolver.
        /// </value>
        protected override DetailResolverDelegate<BodyDataFrame> DetailEntityResolver =>
            (s) => s.Include(f => f.BodyData)
                .Include(l => l.Joints).ThenInclude(j => j.JointType)
                .Include(l => l.Joints).ThenInclude(j => j.JointTrackingStateType);

        /// <summary>
        /// Gets the set resolver.
        /// </summary>
        /// <value>
        /// The set resolver.
        /// </value>
        protected override SetResolverDelegate<BodyDataFrame> SetResolver => 
            (context) => context.BodyDataFrames;

        /// <summary>
        /// Gets the entity resolver.
        /// </summary>
        /// <value>
        /// The entity resolver.
        /// </value>
        protected override EntityResolverDelegate<BodyDataFrame> EntityResolver => 
            (set) => set.Where(frame => frame.UserID == context.UserID);
    }
}

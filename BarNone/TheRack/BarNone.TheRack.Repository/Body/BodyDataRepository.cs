using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.Shared.DataTransfer.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BarNone.Shared.DataConverters;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.Repository
{
    public class BodyDataRepository : DefaultDetailRepository<BodyData, BodyDataDTO, BodyDataDetailDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyDataRepository"/> class.
        /// </summary>
        public BodyDataRepository() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyDataRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BodyDataRepository(DomainContext context) : base(context)
        {

        }

        /// <summary>
        /// Gets the data converter.
        /// </summary>
        /// <value>
        /// The data converter.
        /// </value>
        protected override ConverterResolverDelegate<BodyData, BodyDataDTO> DataConverter => 
            Converters.NewConvertion(context).BodyData.CreateDataModel;

        /// <summary>
        /// Gets the detail entity resolver.
        /// </summary>
        /// <value>
        /// The detail entity resolver.
        /// </value>
        protected override DetailResolverDelegate<BodyData> DetailEntityResolver => (s) => s
                .Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints).ThenInclude(j => j.JointType)
                .Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints).ThenInclude(j => j.JointTrackingStateType);

        /// <summary>
        /// Gets the set resolver.
        /// </summary>
        /// <value>
        /// The set resolver.
        /// </value>
        protected override SetResolverDelegate<BodyData> SetResolver => (context) => context.Bodies;

        /// <summary>
        /// Gets the entity resolver.
        /// </summary>
        /// <value>
        /// The entity resolver.
        /// </value>
        protected override EntityResolverDelegate<BodyData> EntityResolver =>
            (bodies) => bodies.Where(body => body.UserID == context.UserID);
    }
}

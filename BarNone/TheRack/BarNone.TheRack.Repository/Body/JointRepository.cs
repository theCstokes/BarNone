using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.TheRack.DataAccess;
using Microsoft.EntityFrameworkCore;
using static BarNone.TheRack.Repository.Core.Resolvers;
using System.Linq;
using BarNone.Shared.DataConverters;

namespace BarNone.TheRack.Repository.Body
{
    public class JointRepository : DefaultDetailRepository<Joint, JointDTO, JointDetailDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JointRepository"/> class.
        /// </summary>
        public JointRepository() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JointRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public JointRepository(DomainContext context) : base(context)
        {

        }

        /// <summary>
        /// Gets the data converter.
        /// </summary>
        /// <value>
        /// The data converter.
        /// </value>
        protected override ConverterResolverDelegate<Joint, JointDTO> DataConverter => 
            Converters.NewConvertion(context).Joint.CreateDataModel;

        /// <summary>
        /// Gets the detail entity resolver.
        /// </summary>
        /// <value>
        /// The detail entity resolver.
        /// </value>
        protected override DetailResolverDelegate<Joint> DetailEntityResolver => (s) => s.Include(j => j.BodyDataFrame)
                    .Include(j => j.JointType)
                    .Include(j => j.JointTrackingStateTypeID);

        /// <summary>
        /// Gets the set resolver.
        /// </summary>
        /// <value>
        /// The set resolver.
        /// </value>
        protected override SetResolverDelegate<Joint> SetResolver => (context) => context.Joints;

        /// <summary>
        /// Gets the entity resolver.
        /// </summary>
        /// <value>
        /// The entity resolver.
        /// </value>
        protected override EntityResolverDelegate<Joint> EntityResolver => (joints) => joints.Where(joint => joint.UserID == context.UserID);
    }
}

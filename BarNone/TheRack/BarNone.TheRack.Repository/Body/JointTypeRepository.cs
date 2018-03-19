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
using BarNone.Shared.DataTransfer.LiftData;

namespace BarNone.TheRack.Repository.Body
{
    public class JointTypeRepository : DefaultRepository<JointType, JointTypeDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JointRepository"/> class.
        /// </summary>
        public JointTypeRepository() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JointRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public JointTypeRepository(DomainContext context) : base(context)
        {

        }

        /// <summary>
        /// Gets the data converter.
        /// </summary>
        /// <value>
        /// The data converter.
        /// </value>
        protected override ConverterResolverDelegate<JointType, JointTypeDTO> DataConverter => 
            Converters.NewConvertion(context).JointType.CreateDataModel;

        /// <summary>
        /// Gets the set resolver.
        /// </summary>
        /// <value>
        /// The set resolver.
        /// </value>
        protected override SetResolverDelegate<JointType> SetResolver => (context) => context.JointTypes;

        /// <summary>
        /// Gets the entity resolver.
        /// </summary>
        /// <value>
        /// The entity resolver.
        /// </value>
        protected override EntityResolverDelegate<JointType> EntityResolver => (joints) => joints;
    }
}

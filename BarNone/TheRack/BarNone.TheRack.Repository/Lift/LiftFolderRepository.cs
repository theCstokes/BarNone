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
    public class LiftFolderRepository : DefaultDetailRepository<LiftFolder, LiftFolderDTO, LiftFolderDetailDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiftFolderRepository"/> class.
        /// </summary>
        public LiftFolderRepository() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiftFolderRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LiftFolderRepository(DomainContext context) : base(context)
        {

        }

        /// <summary>
        /// Gets the data converter.
        /// </summary>
        /// <value>
        /// The data converter.
        /// </value>
        protected override ConverterResolverDelegate<LiftFolder, LiftFolderDTO> DataConverter => 
            Converters.NewConvertion(context).LiftFolder.CreateDataModel;

        /// <summary>
        /// Gets the detail entity resolver.
        /// </summary>
        /// <value>
        /// The detail entity resolver.
        /// </value>
        protected override DetailResolverDelegate<LiftFolder> DetailEntityResolver => (folders) => folders.Include(l => l.Parent)
                .Include(l => l.SubFolders).ThenInclude(f => f.Lifts)
                .Include(l => l.Lifts);

        /// <summary>
        /// Gets the set resolver.
        /// </summary>
        /// <value>
        /// The set resolver.
        /// </value>
        protected override SetResolverDelegate<LiftFolder> SetResolver => (context) => context.LiftFolders;

        /// <summary>
        /// Gets the entity resolver.
        /// </summary>
        /// <value>
        /// The entity resolver.
        /// </value>
        protected override EntityResolverDelegate<LiftFolder> EntityResolver => 
            (folders) => folders.Where(folder => folder.UserID == context.UserID);
    }
}

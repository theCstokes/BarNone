using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.Repository
{
    public class CommmentRepository : DefaultDetailRepository<Comment, CommentDTO, CommentDetailDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiftFolderRepository"/> class.
        /// </summary>
        public CommmentRepository() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiftFolderRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CommmentRepository(DomainContext context) : base(context)
        {

        }

        /// <summary>
        /// Gets the data converter.
        /// </summary>
        /// <value>
        /// The data converter.
        /// </value>
        protected override ConverterResolverDelegate<Comment, CommentDTO> DataConverter =>
            Converters.NewConvertion(context).Comment.CreateDataModel;

        /// <summary>
        /// Gets the detail entity resolver.
        /// </summary>
        /// <value>
        /// The detail entity resolver.
        /// </value>
        protected override DetailResolverDelegate<Comment> DetailEntityResolver => (folders) => folders.Include(l => l.Lift);
                //.Include(l => l.SubFolders).ThenInclude(f => f.Lifts)
                //.Include(l => l.Lifts);

        /// <summary>
        /// Gets the set resolver.
        /// </summary>
        /// <value>
        /// The set resolver.
        /// </value>
        protected override SetResolverDelegate<Comment> SetResolver => (context) => context.Comments;

        /// <summary>
        /// Gets the entity resolver.
        /// </summary>
        /// <value>
        /// The entity resolver.
        /// </value>
        protected override EntityResolverDelegate<Comment> EntityResolver =>
            (comments) => comments.Where(comment => comment.UserID == context.UserID);
    }
}

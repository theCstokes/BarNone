using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.Repository
{
    public class LiftPermissionRepository : DefaultRepository<LiftPermission, LiftPermissionDTO>
    {
        protected override ConverterResolverDelegate<LiftPermission, LiftPermissionDTO> DataConverter => 
            Converters.NewConvertion(context).LiftPermission.CreateDataModel;

        protected override SetResolverDelegate<LiftPermission> SetResolver => (context) => context.LiftPermissions;

        protected override EntityResolverDelegate<LiftPermission> EntityResolver => (set) => set;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiftPermissionRepository"/> class.
        /// </summary>
        public LiftPermissionRepository() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiftPermissionRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LiftPermissionRepository(DomainContext context) : base(context)
        {
        }
    }
}

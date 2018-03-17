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
    public class LiftTypeRepository : DefaultRepository<LiftType, LiftTypeDTO>
    {
        public LiftTypeRepository() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiftRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LiftTypeRepository(DomainContext context) : base(context)
        {

        }

        protected override ConverterResolverDelegate<LiftType, LiftTypeDTO> DataConverter => 
            Converters.NewConvertion().LiftType.CreateDataModel;

        protected override SetResolverDelegate<LiftType> SetResolver => (context) => context.LiftTypes;

        protected override EntityResolverDelegate<LiftType> EntityResolver => (set) => set;
    }
}

using BarNone.TheRack.DataAccess;
using System.Collections.Generic;
using BarNone.TheRack.Repository.Core;
using BarNone.TheRack.Repository;

namespace BarNone.TheRack.FlexEngine
{
    class FlexMap
    {
        public delegate IRepository IRepositoryBuilder(DomainContext context);

        public static Dictionary<string, IRepositoryBuilder> Map { get; } =
            new Dictionary<string, IRepositoryBuilder>
            {
                [FlexEntityType.LIFT] = (c) => new LiftRepository(c),
                [FlexEntityType.LIFT_FOLDER] = (c) => new LiftFolderRepository(c),
                [FlexEntityType.USER] = (c) => new UserRepository(c),
                [FlexEntityType.BODY_DATA] = (c) => new BodyDataRepository(c),
                [FlexEntityType.BODY_DATA_FRAME] = (c) => new BodyDataFrameRepository(c)
            };
    }
}

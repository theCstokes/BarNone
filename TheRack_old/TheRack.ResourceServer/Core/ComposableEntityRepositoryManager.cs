using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheRack.Repository;
using TheRack.ResourceServer.Core;

namespace TheRack.ResourceServer.Core
{
    public class ComposableEntityRepositoryManager
    {
        public delegate IRepository RepositoryBuilder();

        public static readonly Dictionary<string, RepositoryBuilder> EntityMap
            = new Dictionary<string, RepositoryBuilder>
            {
                [ComposableEntityType.USER] = () => new UserRepository()
            };
    }
}
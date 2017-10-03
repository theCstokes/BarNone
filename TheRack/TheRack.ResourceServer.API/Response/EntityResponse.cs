using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRack.ResourceServer.API.Response
{
    public class EntityResponse : IResponse
    {
        public object Entity { get; set; }

        public static EntityResponse Create(object entity)
        {
            return new EntityResponse
            {
                Entity = entity
            };
        }
    }
}

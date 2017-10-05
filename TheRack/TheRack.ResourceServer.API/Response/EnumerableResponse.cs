using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRack.ResourceServer.API.Response
{
    public class EnumerableResponse : IResponse
    {
        public int Count { get; set; }
        public IEnumerable<object> Entities { get; set; }

        public static EnumerableResponse Create(IEnumerable<object> entities)
        {
            return new EnumerableResponse
            {
                Count = entities.Count(),
                Entities = entities
            };
        }
    }
}

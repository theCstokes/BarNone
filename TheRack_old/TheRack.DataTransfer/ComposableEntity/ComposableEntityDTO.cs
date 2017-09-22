using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.DataTransfer.Core;

namespace TheRack.DataTransfer.ComposableEntity
{
    public class ComposableEntityDTO : BaseDTO
    {
        public override int ID { get; set; }

        public List<RequestEntity> Entities { get; set; }
    }

    public class RequestEntity : BaseDTO
    {
        public override int ID { get; set; }

        public string EntityType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RequestType RequestType { get; set; }
        public string Key { get; set; }
    }
}
